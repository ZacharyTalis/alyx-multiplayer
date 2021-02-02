using LiveSplit.ComponentUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace alyx_multiplayer
{
    class Core
    {
        private static Process game = Process.GetProcessesByName("hlvr")[0];

        public static void Main(string[] args)
        {
            Init();

            Console.SetCursorPosition(0, 10);
            while (true)
            {
                Update();
                Thread.Sleep(50);
                Console.SetCursorPosition(0,10);
            }

        }

        private static IntPtr _entListPtr;
        private static IntPtr _gamePathPtr;
        private static IntPtr _mapNamePtr => _gamePathPtr + 0x100;
        private static StringWatcher _mapName;

        private static MemoryWatcherList _watchers = new MemoryWatcherList();

        private const int ENT_INFO_SIZE = 120;

        private static IntPtr GetPointer(IntPtr ptr, int trgOperandOffset, int totalSize)
        {
            byte[] bytes = game.ReadBytes(ptr + trgOperandOffset, 4);
            if (bytes == null)
            {
                return IntPtr.Zero;
            }
            Array.Reverse(bytes);
            int offset = Convert.ToInt32(BitConverter.ToString(bytes).Replace("-", ""), 16);
            IntPtr actualPtr = IntPtr.Add((ptr + totalSize), offset);
            return actualPtr;
        }

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
            if (server == null || engine == null)
            {
                Thread.Sleep(1000);
                Console.WriteLine("[INIT] Modules aren't yet loaded! Waiting 1 second until next try");
                throw new Exception();
            }
            var serverScanner = new SignatureScanner(game, server.BaseAddress, server.ModuleMemorySize);
            var engineScanner = new SignatureScanner(game, engine.BaseAddress, engine.ModuleMemorySize);

            _entListPtr = serverScanner.Scan(_entListSig); SigReport("entity list", _entListPtr);
            _gamePathPtr = engineScanner.Scan(_gamePathSig); SigReport("game path / map name", _gamePathPtr);

            Console.WriteLine("gamepath " + game.ReadString(_gamePathPtr, 255) + "             ");

            _mapName = new StringWatcher(_mapNamePtr, 255);
            _watchers.Add(_mapName);
        }

        private static IntPtr GetEntPtrFromIndex(int index)
        {
            // the game splits the entity pointer list into blocks with seemingly a certain size
            // this function is taken from the game's decompiled code

            int block = 24 + (index >> 9) * 8;
            int pos = (index & 511) * ENT_INFO_SIZE;

            new DeepPointer(_entListPtr, block, 0x0).DerefOffsets(game, out IntPtr blockPtr);

            IntPtr entPtr = blockPtr + pos;
            return entPtr;
        }

        private static string GetNameFromPtr(IntPtr ptr, bool isTargetName = false)
        {
            DeepPointer nameptr = new DeepPointer(ptr, 0x10, (isTargetName) ? 0x18 : 0x20, 0x0);
            string name = "";
            nameptr.DerefString(game, 128, out name);
            return name;
        }

        private static IntPtr FindEntByName(string name, bool isTargetName = false)
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

        private static Vector3f GetEntPos(IntPtr ptr)
        {
            new DeepPointer(ptr, 0x1a0, 0x108).DerefOffsets(game, out IntPtr posPtr);
            return game.ReadValue<Vector3f>(posPtr);
        }

        private static Vector3f GetEntAngle(IntPtr ptr)
        {
            new DeepPointer(ptr, 0x1a0, 0x114).DerefOffsets(game, out IntPtr angPtr);
            return game.ReadValue<Vector3f>(angPtr);
        }

        private static void Update()
        {
            _watchers.UpdateAll(game);
            Console.WriteLine("pos " + GetEntPos(GetEntPtrFromIndex(1)) + "             ");
            Console.WriteLine("ang " + GetEntAngle(GetEntPtrFromIndex(1)) + "             ");
            Console.WriteLine("map " + _mapName.Current + "             ");
        }
        
    }
}
