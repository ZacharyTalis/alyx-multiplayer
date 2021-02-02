using LiveSplit.ComponentUtil;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace alyx_multiplayer
{
    class Core
    {
        private static Process game;

        private static IntPtr _entListPtr;
        private static IntPtr _gamePathPtr;
        private static IntPtr _mapNamePtr => _gamePathPtr + 0x100;
        private static StringWatcher _mapName;

        private static MemoryWatcherList _watchers = new MemoryWatcherList();

        private const int ENT_INFO_SIZE = 120;

        private const string scriptPath = @"C:\Users\Zachary\Desktop\";

        /// <summary>
        /// Main method. Run init, then update continuously.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {

            bool isGameRunning = false;
            while (!isGameRunning)
            {
                try {
                    game = Process.GetProcessesByName("hlvr")[0];
                    isGameRunning = true;
                }
                catch (IndexOutOfRangeException) {
                    Console.WriteLine("[MAIN] Game not running! Checking again in five seconds.");
                    Thread.Sleep(5000);
                }
            } Console.Clear();

            Init();

            Console.SetCursorPosition(0, 10);
            while (true)
            {
                Update();
                Thread.Sleep(50);
                Console.SetCursorPosition(0,10);
            }
        }

        /// <summary>
        /// Find a base address pointer for signature scanning .
        /// </summary>
        /// <param name="ptr">The initial pointer to use.</param>
        /// <param name="trgOperandOffset">An offset from the initial pointer. Used when first reading bytes from this pointer.</param>
        /// <param name="totalSize">The expected total size of the base address pointer.</param>
        /// <returns></returns>
        private static IntPtr GetPointer(IntPtr ptr, int trgOperandOffset, int totalSize)
        {
            byte[] bytes = game.ReadBytes(ptr + trgOperandOffset, 4);
            if (bytes == null)
            {
                return IntPtr.Zero;
            }
            Array.Reverse(bytes);
            int offset = Convert.ToInt32(BitConverter.ToString(bytes).Replace("-", ""), 16);
            IntPtr actualPtr = IntPtr.Add(ptr + totalSize, offset);
            return actualPtr;
        }

        /// <summary>
        /// Initialize signature scanners for the server and engine.
        /// </summary>
        private static void Init()
        {
            Action<string, IntPtr> SigReport = (name, ptr) =>
            {
                Console.WriteLine("[INIT] " + name + (ptr == IntPtr.Zero ? " WAS NOT FOUND" : " is 0x" + ptr.ToString("X")));
            };

            SigScanTarget _entListSig = new SigScanTarget(6,
                "40 ?? 48 ?? ?? ??",
                "48 ?? ?? ?? ?? ?? ??", // MOV RAX,qword ptr [DAT_1814e3bc0]
                "8b ?? 48 ?? ?? ?? ?? ?? ?? 48 ?? ?? ff ?? ?? ?? ?? ?? 4c ?? ??");
            _entListSig.OnFound = (proc, scanner, ptr) => GetPointer(ptr, 3, 7);

            SigScanTarget _gamePathSig = new SigScanTarget(7,
                "48 8B 97 ?? ?? ?? ??",
                "48 8D 0D ?? ?? ?? ??", // LEA RCX,[mapname]
                "48 8B 5C 24 ??");
            _gamePathSig.OnFound = (proc, scanner, ptr) => GetPointer(ptr, 3, 7);

            ProcessModuleWow64Safe[] modules = game.ModulesWow64Safe();

            ProcessModuleWow64Safe server = modules.FirstOrDefault(x => x.ModuleName.ToLower() == "server.dll");
            ProcessModuleWow64Safe engine = modules.FirstOrDefault(x => x.ModuleName.ToLower() == "engine2.dll");
            while (server == null || engine == null)
            {
                Console.WriteLine("[INIT] Modules aren't yet loaded! Waiting 1 second until next try");
                Thread.Sleep(1000);
            }
            var serverScanner = new SignatureScanner(game, server.BaseAddress, server.ModuleMemorySize);
            var engineScanner = new SignatureScanner(game, engine.BaseAddress, engine.ModuleMemorySize);

            _entListPtr = serverScanner.Scan(_entListSig); SigReport("entity list", _entListPtr);
            _gamePathPtr = engineScanner.Scan(_gamePathSig); SigReport("game path / map name", _gamePathPtr);

            Console.WriteLine("gamepath " + game.ReadString(_gamePathPtr, 255) + "             ");

            _mapName = new StringWatcher(_mapNamePtr, 255);
            _watchers.Add(_mapName);
        }

        /// <summary>
        /// Get the pointer of an entity using its index.
        /// </summary>
        private static IntPtr GetEntPtrFromIndex(int index)
        {
            // The game splits the entity pointer list into blocks with seemingly a certain size.
            // This function is taken from the game's decompiled code!

            int block = 24 + (index >> 9) * 8;
            int pos = (index & 511) * ENT_INFO_SIZE;

            new DeepPointer(_entListPtr, block, 0x0).DerefOffsets(game, out IntPtr blockPtr);

            IntPtr entPtr = blockPtr + pos;
            return entPtr;
        }

        /// <summary>
        /// Get the name of an entity using a pointer.
        /// </summary>
        /// <param name="ptr">The pointer.</param>
        /// <param name="isTargetName">Whether or not the DeepPointer declaration should use target name offsets.</param>
        /// <returns></returns>
        private static string GetNameFromPtr(IntPtr ptr, bool isTargetName = false)
        {
            DeepPointer nameptr = new DeepPointer(ptr, 0x10, (isTargetName) ? 0x18 : 0x20, 0x0);
            string name = "";
            nameptr.DerefString(game, 128, out name);
            return name;
        }

        /// <summary>
        /// Get the pointer of an entity using its name. Currently unused.
        /// </summary>
        /// <param name="name">The entity name.</param>
        /// <param name="isTargetName">Whether or not the DeepPointer declaration should use target name offsets.</param>
        /// <returns></returns>
        private static IntPtr GetPtrByName(string name, bool isTargetName = false)
        {
            var prof = Stopwatch.StartNew();
            // 2838: theorectically the index can go all the way up to 32768 but it never does even on the biggest of maps
            for (int i = 0; i <= 20000; i++)
            {
                IntPtr entPtr = GetEntPtrFromIndex(i);
                if (entPtr != IntPtr.Zero)
                {
                    if (GetNameFromPtr(entPtr, isTargetName) == name)
                    {
                        prof.Stop();
                        Console.WriteLine("[ENTFINDING] Successfully found " + name + "'s pointer after " + prof.ElapsedMilliseconds * 0.001f + " seconds, index #" + i);
                        return entPtr;
                    }
                    else continue;
                }
            }

            prof.Stop();
            Console.WriteLine("[ENTFINDING] Can't find " + name + "'s pointer! Time spent: " + prof.ElapsedMilliseconds * 0.001f + " seconds");
            return IntPtr.Zero;
        }

        /// <summary>
        /// Get the position of an entity using a pointer.
        /// </summary>
        /// <param name="ptr">The pointer.</param>
        /// <returns></returns>
        private static Vector3f GetEntPosFromPtr(IntPtr ptr)
        {
            new DeepPointer(ptr, 0x1a0, 0x108).DerefOffsets(game, out IntPtr posPtr);
            return game.ReadValue<Vector3f>(posPtr);
        }

        /// <summary>
        /// Get the angles of an entity using a pointer.
        /// </summary>
        /// <param name="ptr">The pointer.</param>
        /// <returns></returns>
        private static Vector3f GetEntAngleFromPtr(IntPtr ptr)
        {
            new DeepPointer(ptr, 0x1a0, 0x114).DerefOffsets(game, out IntPtr angPtr);
            return game.ReadValue<Vector3f>(angPtr);
        }

        /// <summary>
        /// Update the console with current position, angles, and mapname.
        /// </summary>
        private static void Update()
        {
            _watchers.UpdateAll(game);

            Vector3f pos = GetEntPosFromPtr(GetEntPtrFromIndex(1));
            Vector3f ang = GetEntAngleFromPtr(GetEntPtrFromIndex(1));

            // Eventually this'll be for the networked avatar, but for now we'll use the player's own specs.
            LuaUtils.WriteCoordsToScript(scriptPath, pos, ang);

            Console.WriteLine("pos " + pos + "             ");
            Console.WriteLine("ang " + ang + "             ");
            Console.WriteLine("map " + _mapName.Current + "             ");
        }
        
    }
}
