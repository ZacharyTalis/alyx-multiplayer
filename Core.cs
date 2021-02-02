using LiveSplit.ComponentUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace alyx_multiplayer
{
    class Core
    {
        private static Hashtable vars = new Hashtable();
        private static Hashtable settings = new Hashtable();

        private static Process game = Process.GetProcessesByName("hlvr")[0];

        public static void Main(string[] args)
        {
            Init();
            Startup();
            Start();
            Reset();
            Split();
            IsLoading();
            GameTime();

            while (true)
            {
                Update();
                Thread.Sleep(50);
            }

        }

        private static void Init()
        {  
            IntPtr handle = game.Handle;
            ProcessModuleWow64Safe[] modules = game.ModulesWow64Safe();

            Func<IntPtr, int, int, IntPtr> GetPointerFromOpcode = (ptr, trgOperandOffset, totalSize) =>
            {
                
                UIntPtr transferred = new UIntPtr();

                byte[] bytes = game.ReadBytes(ptr + trgOperandOffset, 4);
                if (bytes == null)
                {
                    return IntPtr.Zero;
                }
                Array.Reverse(bytes);
                int offset = Convert.ToInt32(BitConverter.ToString(bytes).Replace("-", ""), 16);
                IntPtr actualPtr = IntPtr.Add((ptr + totalSize), offset);
                return actualPtr;
            };

            vars.Add("sigentList",
                new SigScanTarget(6,  "40 ?? 48 ?? ?? ?? ",
                                      "48 ?? ?? ?? ?? ?? ??", // MOV RAX,qword ptr [DAT_1814e3bc0]
                                      "8b ?? 48 ?? ?? ?? ?? ?? ?? 48 ?? ?? ff ?? ?? ?? ?? ?? 4c ?? ??")
            );
            vars.Add("sigloading",
                new SigScanTarget(18, "B2 01 C6 05 ?? ?? ?? ?? 01 48 8B 01 FF 90 ?? ?? ?? ??",
                                      "C7 05 ?? ?? ?? ?? 01 00 00 00", // MOV dword ptr [DAT_180f67f7c],0x1 
                                      "0F 28 74 24 40 48 83 C4 50 5B")
            );
            vars.Add("siginLvlTrans",
                new SigScanTarget(30, "F3 0F 11 05 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 86 ?? ?? ?? ?? 48 8D 0D ?? ?? ?? ?? 48 85 C0",
                                      "C6 05 ?? ?? ?? ?? 01") // MOV byte ptr [DAT_180e8916c],0x1
            );
            vars.Add("sigbuildNum",
                new SigScanTarget(4,  "48 83 ec ??",
                                      "8b 05 ?? ?? ?? ??", // MOV EAX,dword ptr [0x18053ef54]
                                      "33 ff 85 c0 0f ?? ?? ?? ?? 00 48 89 5c 24 30 8b df 48 89 74 24 38")
            );
            vars.Add("sigmapTime",
                new SigScanTarget(11, "F3 0F 58 ?? 48 8B 05 ?? ?? ?? ??",
                                      "F3 0F 11 ?? ?? ?? ?? ??", // this
                                      "48 85 C0 74 ?? 80 38 00 74 ??")
            );
            vars.Add("sigmapTimenoVr",
                new SigScanTarget(7,  "48 8B 97 ?? ?? ?? ??",
                                      "48 8D 0D ?? ?? ?? ??", // LEA RCX,[0x180544a00]
                                      "48 8B 5C 24 ??")
            );
            vars.Add("sigmapName",
                new SigScanTarget(7, "48 8B 97 ?? ?? ?? ??",
                                      "48 8D 0D ?? ?? ?? ??", // LEA RCX,[0x180544a00]
                                      "48 8B 5C 24 ??")
            );
            vars.Add("signoVr",
                new SigScanTarget(0,  "48 8B 0D ?? ?? ?? ??", // MOV RCX,qword ptr [0x180e5e928]
                                      "48 8B DA 48 85 C9 0F 84 ?? ?? ?? ?? 48 8B 01")
            );

            ProcessModuleWow64Safe client = modules.FirstOrDefault(x => x.ModuleName.ToLower() == "client.dll");
            ProcessModuleWow64Safe server = modules.FirstOrDefault(x => x.ModuleName.ToLower() == "server.dll");
            ProcessModuleWow64Safe engine = modules.FirstOrDefault(x => x.ModuleName.ToLower() == "engine2.dll");
            while (client == null || engine == null || server == null)
            {
                Thread.Sleep(1000);
                Console.WriteLine("[SIGSCANNING] All modules aren't yet loaded! Waiting 1 second until next try");
                // no need to throw an exception here, just wait it out
                // throw new Exception();
            }
            var clientScanner = new SignatureScanner(game, client.BaseAddress, client.ModuleMemorySize);
            var engineScanner = new SignatureScanner(game, engine.BaseAddress, engine.ModuleMemorySize);
            var serverScanner = new SignatureScanner(game, server.BaseAddress, server.ModuleMemorySize);

            IntPtr ptrnoVr = GetPointerFromOpcode(clientScanner.Scan((SigScanTarget)vars["signoVr"]), 3, 7);
            // this pointer doesn't seem to be initialized whenever the game is in novr
            bool isnoVr = new DeepPointer(ptrnoVr).Deref<IntPtr>(game) == IntPtr.Zero;

            IntPtr ptrentList = GetPointerFromOpcode(serverScanner.Scan((SigScanTarget)vars["sigentList"]), 3, 7);
            IntPtr ptrloading = GetPointerFromOpcode(clientScanner.Scan((SigScanTarget)vars["sigloading"]), 2, 10);
            IntPtr ptrinLvlTrans = GetPointerFromOpcode(clientScanner.Scan((SigScanTarget)vars["siginLvlTrans"]), 2, 7);
            IntPtr ptrbuildNum = GetPointerFromOpcode(engineScanner.Scan((SigScanTarget)vars["sigbuildNum"]), 2, 6);
            IntPtr ptrmapTime = (isnoVr) ? GetPointerFromOpcode(serverScanner.Scan((SigScanTarget)vars["sigmapTimenoVr"]), 3, 7) : GetPointerFromOpcode(clientScanner.Scan((SigScanTarget)vars["sigmapTime"]), 4, 8);
            IntPtr ptrmapName = GetPointerFromOpcode(engineScanner.Scan((SigScanTarget)vars["sigmapName"]), 3, 7) + 0x100;

            Console.WriteLine("[SIGSCANNING] entList PTR is " + ptrentList.ToString("X"));
            Console.WriteLine("[SIGSCANNING] inLvlTrans PTR is " + ptrinLvlTrans.ToString("X"));
            Console.WriteLine("[SIGSCANNING] buildNum PTR is " + ptrbuildNum.ToString("X"));
            Console.WriteLine("[SIGSCANNING] loading PTR is " + ptrloading.ToString("X"));
            Console.WriteLine("[SIGSCANNING] mapTime PTR is " + ptrmapTime.ToString("X"));
            Console.WriteLine("[SIGSCANNING] mapName PTR is " + ptrmapName.ToString("X"));
            Console.WriteLine("[SIGSCANNING] noVr PTR is " + ptrnoVr.ToString("X"));

            int buildnum = game.ReadValue<int>(ptrbuildNum);
            Console.WriteLine("[GAME INFO] Game is build number " + buildnum);

            if (isnoVr)
            {
                Console.WriteLine("[GAME INFO] Game is running in No VR mode");
            }

            // SETTING UP WATCHLIST
            vars.Add("loading", new MemoryWatcher<int>(ptrloading));
            vars.Add("mapTime", (isnoVr) ? new MemoryWatcher<float>(new DeepPointer(ptrmapTime, 0x0)) : new MemoryWatcher<float>(ptrmapTime));
            vars.Add("inLvlTrans", new MemoryWatcher<byte>(ptrinLvlTrans));
            vars.Add("entList", new MemoryWatcher<IntPtr>(new DeepPointer(ptrentList)));
            vars.Add("moveFlag", new MemoryWatcher<byte>(new DeepPointer(ptrentList, 0x18, 0x78, 0x2e9c)));
            vars.Add("map", new StringWatcher(ptrmapName, 120));

            vars.Add("watchIt", new MemoryWatcherList()
                {
                    (MemoryWatcher)vars["loading"],
                    (MemoryWatcher)vars["mapTime"],
                    (MemoryWatcher)vars["inLvlTrans"],
                    (MemoryWatcher)vars["entList"],
                    (MemoryWatcher)vars["moveFlag"],
                    (MemoryWatcher)vars["map"]
                }
            );

            // ENTITY LIST FUNCTIONS

            int entInfoSize = 120;

            Func<int, IntPtr> GetEntPtrFromIndex = (index) =>
            {
                // the game splits the entity pointer list into blocks with seemingly a certain size
                // this function is taken from the game's decompiled code

                int block = 24 + (index >> 9) * 8;
                int pos = (index & 511) * entInfoSize;

                DeepPointer DPentPtr = new DeepPointer((((MemoryWatcher<IntPtr>)vars["entList"]).Current) + block, 0x0);
                IntPtr blockPtr = IntPtr.Zero;
                DPentPtr.DerefOffsets(game, out blockPtr);

                IntPtr entPtr = blockPtr + pos;
                return entPtr;
            };

            Func<IntPtr, bool, string> GetNameFromPtr = (entPtr, isTargetName) =>
            {
                DeepPointer nameptr = new DeepPointer(entPtr, 0x10, (isTargetName) ? 0x18 : 0x20, 0x0);
                string name = "";
                nameptr.DerefString(game, 128, out name);
                return name;
            };

            // 2838: EXTREMELY expensive, do NOT call frequently!!!!
            Func<string, bool, IntPtr> GetEntFromName = (name, isTargetName) =>
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
                        else
                        {
                            continue;
                        }
                    }
                    prof.Stop();
                    Console.WriteLine("ENTFINDING] Can't find " + name + "'s pointer! Time spent: " + prof.ElapsedMilliseconds * 0.001f + " seconds");
                }

                return IntPtr.Zero;
            };

            // 2838: not a necessary function but imma just put this here in case someone finds a use for it
            Func<IntPtr, string> PrintPosFromPtr = (entPtr) =>
            {
                DeepPointer posDP = new DeepPointer(entPtr, 0x1a0, 0x108);
                IntPtr posPtr;
                posDP.DerefOffsets(game, out posPtr);
                float xPos; game.ReadValue<float>(posPtr, out xPos);
                float yPos; game.ReadValue<float>(posPtr + 0x4, out yPos);
                float zPos; game.ReadValue<float>(posPtr + 0x8, out zPos);

                string pos = xPos + " " + yPos + " " + zPos;

                return pos;
            };

            vars["GetEntFromName"] = GetEntFromName;
            vars["GetEntPtrFromIndex"] = GetEntPtrFromIndex;
            vars["GetNameFromPtr"] = GetNameFromPtr;
            vars["PrintPosFromPtr"] = PrintPosFromPtr;
        }

        private static void Startup()
        {
            //SETTINGS
            // Split on Chapter Transitions Instead of Per-Map
            settings.Add("chapters", false);

            // Only use when running ILs. Starts automatically on any map selected
            settings.Add("il", false);

            //MAP DATA
            vars.Add("latestMap", "a1_intro_world");

            vars.Add("maps", new Dictionary<string, Tuple<int, int>>() { 
            //   MAP NAME                                             ID          CHAPTER
                {"a1_intro_world"               , new Tuple<int, int>(0         , 0         )},
                {"a1_intro_world_2"             , new Tuple<int, int>(1         , 0         )},

                {"a2_quarantine_entrance"       , new Tuple<int, int>(2         , 1         )},
                {"a2_pistol"                    , new Tuple<int, int>(3         , 1         )},
                {"a2_hideout"                   , new Tuple<int, int>(4         , 1         )},

                {"a2_headcrabs_tunnel"          , new Tuple<int, int>(5         , 2         )},
                {"a2_drainage"                  , new Tuple<int, int>(6         , 2         )},
                {"a2_train_yard"                , new Tuple<int, int>(7         , 2         )},

                {"a3_station_street"            , new Tuple<int, int>(8         , 3         )},

                {"a3_hotel_lobby_basement"      , new Tuple<int, int>(9         , 4         )},
                {"a3_hotel_underground_pit"     , new Tuple<int, int>(10        , 4         )},
                {"a3_hotel_interior_rooftop"    , new Tuple<int, int>(11        , 4         )},
                {"a3_hotel_street"              , new Tuple<int, int>(12        , 4         )},

                {"a3_c17_processing_plant"      , new Tuple<int, int>(13        , 5         )},

                {"a3_distillery"                , new Tuple<int, int>(14        , 6         )},

                {"a4_c17_zoo"                   , new Tuple<int, int>(15        , 7         )},

                {"a4_c17_tanker_yard"           , new Tuple<int, int>(16        , 8         )},

                {"a4_c17_water_tower"           , new Tuple<int, int>(17        , 9         )},
                {"a4_c17_parking_garage"        , new Tuple<int, int>(18        , 9         )},

                {"a5_vault"                     , new Tuple<int, int>(19        , 10        )},
                {"a5_ending"                    , new Tuple<int, int>(20        , 10        )},

                {"startup"                      , new Tuple<int, int>(-10       , -10       )}
            });

            vars.Add("waitForLoading", false);

            //TIMER
            vars.Add("currentTime", 0.0f);

            //END STUFF 
            vars.Add("autoGripDP1", new MemoryWatcher<byte>(IntPtr.Zero));
            vars.Add("autoGripDP2", new MemoryWatcher<byte>(IntPtr.Zero));
            vars.Add("endCheck", true);
        }

        private static void Update()
        {
            ((MemoryWatcherList)vars["watchIt"]).UpdateAll(game);

            Console.WriteLine(((Func<IntPtr, string>)vars["PrintPosFromPtr"])(((Func<int, IntPtr>)vars["GetEntPtrFromIndex"])(1)));

            // 2838: Code for ending the run, messy but it works well enough
            if (((StringWatcher)vars["map"]).Current == "a5_ending")
            {
                // first check: if the player started livesplit and is in the final map already
                // second check: only search for the entity when game has just finished loading

                if ((bool)vars["endCheck"] || (((MemoryWatcher<int>)vars["loading"]).Current != 1 && ((MemoryWatcher<int>)vars["loading"]).Old == 1))
                {
                    vars["endCheck"] = false;
                    // do both hands to really make sure we don't miss
                    vars["autoGripDP1"] = new MemoryWatcher<byte>(new DeepPointer(((Func<string, bool, IntPtr>)vars["GetEntFromName"])("g_release_hand1", true), 0x878, 0xb4));
                    vars["autoGripDP2"] = new MemoryWatcher<byte>(new DeepPointer(((Func<string, bool, IntPtr>)vars["GetEntFromName"])("g_release_hand2", true), 0x878, 0xb4));
                }
            }

            ((MemoryWatcher<byte>)vars["autoGripDP1"]).Update(game);
            ((MemoryWatcher<byte>)vars["autoGripDP2"]).Update(game);

            //Set Current Time

            // 2838: 
            // the game has 2 states of loading: waiting for map load (state 1) and waiting for the player to press the trigger (state 2)
            // we'll only need to exclude state 1 as state 2 is when the game has finished loading in

            float delta = ((MemoryWatcher<float>)vars["mapTime"]).Current - ((MemoryWatcher<float>)vars["mapTime"]).Old;

            if (delta > 0.0f && (((MemoryWatcher<int>)vars["loading"]).Current != 1 && ((MemoryWatcher<byte>)vars["inLvlTrans"]).Current == 0))
            {
                vars["currentTime"] = (float)vars["currentTime"] + delta; 
            }
        }

        private static bool Start()
        {
            vars["currentTime"] = 0.0f;
            vars["autoGripDP1"] = new MemoryWatcher<byte>(IntPtr.Zero);
            vars["autoGripDP2"] = new MemoryWatcher<byte>(IntPtr.Zero);

            //Normal Start Condition

            if ((String)((MemoryWatcher)vars["map"]).Current == "a1_intro_world")
            {
                if ((int)((MemoryWatcher)vars["moveFlag"]).Current == 0 && (int)((MemoryWatcher)vars["moveFlag"]).Old == 1)
                {
                    vars["latestMap"] = "a1_intro_world";
                    return true;
                }
                return false;
            }
            else if ((bool)settings["il"])
            {
                //IL Starts on any level
                bool ilstart = ((MemoryWatcher<int>)vars["loading"]).Old == 1 && ((MemoryWatcher<int>)vars["loading"]).Current == 0;
                if (ilstart)
                { //latestmap has to be set or end split won't work
                    vars["latestMap"] = ((MemoryWatcher)vars["map"]).Current;
                }
                return ilstart;
            }

            return false;
        }
        private static bool Reset()
        {
            if (!(bool)settings["il"])
            {
                if ((string)((MemoryWatcher)vars["map"]).Current == "a1_intro_world")
                {
                    if ((int)((MemoryWatcher)vars["moveFlag"]).Old == 0 && (int)((MemoryWatcher)vars["moveFlag"]).Current == 1)
                    {
                        vars["latestMap"] = "a1_intro_world";
                        vars["currentTime"] = 0.0f;
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }
        private static bool Split()
        {
            //Thanks to my partner for writing this in a debug sesh!
            var mapsDict = (Dictionary < String, Tuple<int, int> > )vars["maps"];
            var mapMemWatcher = (MemoryWatcher)vars["map"];
            var currentMap = (string)mapMemWatcher.Current;
            var oldMap = (string)mapMemWatcher.Old;

            if (currentMap == null || oldMap == null) return false;

            var currentTuple = mapsDict[currentMap];
            var oldTuple = mapsDict[oldMap];

            //Only split if map is increasing
            if (currentTuple.Item1 == oldTuple.Item1 + 1)
            {
                if ((bool)settings["chapters"])
                {
                    vars["latestMap"] = ((MemoryWatcher)vars["map"]).Current;
                    return (currentTuple.Item2 == oldTuple.Item2 + 1);
                }

                return true;
            }

            //Ending Conditional
            if ((string)((MemoryWatcher)vars["map"]).Current == "a5_ending")
            {
                bool check = (((MemoryWatcher<byte>)vars["autoGripDP1"]).Current == 0 && ((MemoryWatcher<byte>)vars["autoGripDP1"]).Old == 1) ||
                (((MemoryWatcher<byte>)vars["autoGripDP2"]).Current == 0 && ((MemoryWatcher<byte>)vars["autoGripDP2"]).Old == 1);
                return check;
            }

            return false;
        }

        private static bool IsLoading()
        {
            return true;
        }

        private static int GameTime()
        {
            return 0;
        }
    }
}
