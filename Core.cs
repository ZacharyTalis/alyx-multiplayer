using LiveSplit.ComponentUtil;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

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
        private const int TICK_MS = 20;

        private static UI ui;
        private static RichTextBox textBoxLog;
        private static ToolStripStatusLabel labelIP;
        public static bool isInfoOpen = false;

        private static bool isConsoleEnabled = false;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        private static extern void ShowWindow(IntPtr one, int two);
        const int CONSOLE_HIDE = 0;
        const int CONSOLE_SHOW = 5;

        private static bool hasFoundPtr = false;

        private const string defaultScriptPath = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life Alyx\game\hlvr_addons\lua_testbed\scripts\vscripts";
        public static string scriptPath;

        /// <summary>
        /// Main method. Establish the events thread, then run the UI.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            ConfigureConsole(isConsoleEnabled);

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            ui = new UI();

            scriptPath = defaultScriptPath;
            Thread eventsThread = new Thread(EventsThread.Begin);
            eventsThread.Start();

            Application.Run(ui);
        }

        /// <summary>
        /// Show or hide the console window.
        /// </summary>
        /// <param name="showWindow">If true, show the window. Else, hide the window.</param>
        private static void ConfigureConsole(bool showConsole)
        {
            var handle = GetConsoleWindow();

            if (showConsole) ShowWindow(handle, CONSOLE_SHOW);
            else ShowWindow(handle, CONSOLE_HIDE);

        }

        public static void ToggleConsole()
        {
            isConsoleEnabled = !isConsoleEnabled;
            ConfigureConsole(isConsoleEnabled);
        }
        /// <summary>
        /// We can run our events while the UI is also running.
        /// </summary>
        private class EventsThread {
            /// <summary>
            /// Run init, then update continuously.
            /// </summary>
            public static void Begin()
            {
                FindUIControls();
                DisplayIP();

                bool isGameRunning = false;
                while (!isGameRunning)
                {
                    try
                    {
                        game = Process.GetProcessesByName("hlvr")[0];
                        isGameRunning = true;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Log("[MAIN] Game not running! Checking again in five seconds.");
                        Thread.Sleep(5000);
                    }
                }
                Console.Clear();

                Init();

                Console.SetCursorPosition(0, 10);
                while (true)
                {
                    Update();
                    Thread.Sleep(TICK_MS);
                    Console.SetCursorPosition(0, 10);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class InfoThread
        {
            /// <summary>
            /// 
            /// </summary>
            public static void ShowInfo()
            {
                Application.Run(new Info());
            }
        }

        /// <summary>
        /// Find and define all the controls from the UI.
        /// </summary>
        private static void FindUIControls()
        {
            textBoxLog = ui.Controls.Find("textBoxLog", true).FirstOrDefault() as RichTextBox;
            StatusStrip statusStrip = ui.Controls.Find("statusStrip", true).FirstOrDefault() as StatusStrip;
            labelIP = statusStrip.Items.Find("labelIP", true).FirstOrDefault() as ToolStripStatusLabel;
        }

        /// <summary>
        /// Output text to the UI log.
        /// </summary>
        /// <param name="text">The text to output.</param>
        delegate void CallbackLog(string text, bool writeToConsole = true);
        public static void Log(String text, bool writeToConsole = true)
        {
            if (textBoxLog.InvokeRequired)
            {
                CallbackLog callbackLog = new CallbackLog(Log);
                textBoxLog.Invoke(callbackLog, new object[] { text, writeToConsole });
            }
            else
            {
                if (writeToConsole) Console.WriteLine(text);
                if (textBoxLog.Text.Equals("")) textBoxLog.Text = text;
                else textBoxLog.Text = textBoxLog.Text + "\n" + text;
            }
        }

        delegate void CallbackDisplayIP();
        private static void DisplayIP()
        {
            String ip = "Not found!";

            try
            {
                ip = new WebClient().DownloadString("https://api.ipify.org");
            } catch
            {
                Console.WriteLine("Error in IP fetch!");
                ip = "Error in IP fetch! Are you connected to the internet?";
            }

            Console.WriteLine("[DISPLAYIP] Client public IP is " + ip);
            labelIP.Text = ip;
        }

        /// <summary>
        /// Get a static pointer from the local pointer of an instruction.
        /// </summary>
        /// <param name="ptr">Location of the instruction.</param>
        /// <param name="trgOperandOffset">Where the pointer sits inside this instruction.</param>
        /// <param name="totalSize">Size of the instruction.</param>
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
        /// Initialize signature scanners and sigscan.
        /// </summary>
        private static void Init()
        {
            if (scriptPath.Equals(defaultScriptPath)) Log("Starting with default script path: \"" + scriptPath + "\"", false);

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
        /// <param name="isTargetName">Whether the supplied name is the entity's name or its class name.</param>
        /// <returns></returns>
        private static string GetNameFromPtr(IntPtr ptr, bool isTargetName = false)
        {
            DeepPointer nameptr = new DeepPointer(ptr, 0x10, (isTargetName) ? 0x18 : 0x20, 0x0);
            string name = "";
            nameptr.DerefString(game, 128, out name);
            return name;
        }

        /// <summary>
        /// Get the pointer of an entity using its name.
        /// </summary>
        /// <param name="name">The entity name.</param>
        /// <param name="isTargetName">If true, the name parameter is the entity's class name. Otherwise, it's the entity's actual name.</param>
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
                        if (!hasFoundPtr)
                        {
                            hasFoundPtr = true;
                            Log("Found " + name + " pointer!", false);
                        }
                        Console.WriteLine("[ENTFINDING] Successfully found " + name + "'s pointer after " + prof.ElapsedMilliseconds * 0.001f + " seconds, index #" + i);
                        return entPtr;
                    }
                    else continue;
                }
            }

            prof.Stop();
            if (hasFoundPtr)
            {
                hasFoundPtr = false;
                Log("Lost track of " + name + " pointer!", false);
            } Console.WriteLine("[ENTFINDING] Can't find " + name + "'s pointer! Time spent: " + prof.ElapsedMilliseconds * 0.001f + " seconds");
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

            // Original code for local coords fetch
            // IntPtr localPtr = GetEntPtrFromIndex(1);

            // Now we're using an empty at the local's head!
            IntPtr localPtr = GetPtrByName("localHead", true);
            Vector3f pos = GetEntPosFromPtr(localPtr);
            Vector3f ang = GetEntAngleFromPtr(localPtr);

            // Eventually this'll be for the networked avatar, but for now we'll use the local's own specs.
            LuaUtils.WriteCoordsToScript(scriptPath, pos, ang);

            Console.WriteLine("pos " + pos + "             ");
            Console.WriteLine("ang " + ang + "             ");
            Console.WriteLine("map " + _mapName.Current + "             ");
        }
        
    }
}
