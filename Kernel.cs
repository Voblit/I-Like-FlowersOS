//******************************************************************************

// * FLOWER OS - Version 1.0 (FIRST!!1 Update)

// * Copyright (c) 2026 Voblit

// * * LICENSE: Multiple, including PolyForm Strict License 1.0.0

// * * DESCRIPTION:

// * Kernel works with multiple classic busybox/DOS commands.

// *Virtual File System initialized and usable.

// *I like flowers!! ( ^ _ ^ )

//******************************************************************************
using Cosmos.HAL;
using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.Listing;
using System.Runtime.ConstrainedExecution;

namespace ILikeFlowersOS
{

    public static class errorThingy
    {
        public static void Error(string e)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Random rnd = new Random();
            Console.WriteLine("ITS DEAD OH NO!!1 " + (rnd.Next(1, 3) == 1 ? "(> ~ <)" : "(T ~ T)"));
            Console.WriteLine("I~like~flowersOS has detected a problem and has shut down to prevent ");
            Console.WriteLine("further damage to your system... or your sanity.");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Err Code:" + e);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("***technical info 0x594F555F4152455F5354555049445F");
            //translation ^^^ means YOU_ARE_STUPID_
            Console.WriteLine("***technical info 0x4745545F4F46465F4C494E5558");
            //translation ^^^ means GET_OFF_LINUX
            Console.WriteLine("***technical info 0x474F5F544F5543485F4752415353");
            //translation ^^^ means GO_TOUCH_GRASS
            Console.WriteLine("\nIf this is the first time you've seen this Stop error screen,");

            Console.WriteLine("restart your computer. If this screen appears again, follow");
            Console.WriteLine("these steps:");

            Console.WriteLine("\nCheck to make sure any new hardware or software is properly installed.");
            Console.WriteLine("If this is a new installation, ask your hardware or software manufacturer");
            Console.WriteLine("why you are like this.");
            Console.Write(Environment.NewLine);
            while (true)
            {

                Cosmos.Core.CPU.Halt();
            }
        }
        public static void Fastfetch(string currentver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
 _______ ___    _________ _________
 |_   _| | |    |_   _| |/ /  ____|                     
   | |   | |      | | | ' /| |__                        
   | |   | |      | | |  < |  __|                       
  _| |_  | |____ _| |_| . \| |____                      
 |_____| |______|_____|_|\_\______|");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
 _________      ______          ______________   ______
 |  ____| |    / __ \ \        / /  ____|  __ \ / ____| 
 | |__  | |   | |  | \ \  /\  / /| |__  | |__) | (___   
 |  __| | |   | |  | |\ \/  \/ / |  __| |  _  / \___ \  
 | |    | |___| |__| | \  /\  /  | |____| | \ \ ____) | 
 |_|___ |______\____/   \/  \/   |______|_|  \_\_____/  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
   ____   ______
  / __ \ / ____|                                        
 | |  | | (___                                          
 | |  | |\___ \                                         
 | |__| |____) |                                        
  \____/|_____/                                         
_____________________I LIKE FLOWERS OS VERSION: " + currentver + "________________________");
            Console.ResetColor();
            Console.WriteLine("Current Date/Time: " + DateTime.Now.ToString());
            Console.WriteLine("Cpu is " + Cosmos.Core.CPU.GetCPUBrandString());
            Console.WriteLine("Uptime is " + Cosmos.Core.CPU.GetCPUUptime() / 1000000000);
        }
    }
    public class Kernel : Sys.Kernel
    {
        Random rnd = new Random();
        //i think i need to update... i think
        public string avaliblecommands = "fastfetch, help, time, cls, echo, hello, ls, reboot, shutdown, dmesg, flower, append-text, recall-text, format, rm, yes, whoami, free, beep, theme, uptime, wc, touch, base64, kill, mkdir, cd, cat";
        Canvas canvas;
        private CosmosVFS vfs;
        public string currentver = "1.0.0";

        //(^-^)
        //dont even think of removing it
        string nothing = @"
⣿⣿⣿⣿⣿⣿⡟⣏⠀⠱⠀⠀⢹⣄⠙⢿⣿⣿⣿⣿⣿⣶⣀⣤⡶⢖⣒⣂⣀⠀⠀⠀⠀⠀⠀⢀⡨⠗⠲⣄⡈⠑⢆⠈⢦⠀⠀⠀⠀
⣿⣿⣿⣿⣿⣿⣿⡞⣸⣆⠀⠀⠀⠀⠙⢗⢦⣙⢿⣿⣿⣿⣿⣿⡟⠁⡀⠀⣼⣿⣄⣀⡠⡀⣀⣀⣈⣀⡤⠂⠁⡬⠑⢄⠀⠀⠣⡀⠀⠀
⣿⣿⣿⣿⣿⣿⡏⣰⢳⣾⣷⡀⠀⠀⢐⠀⠉⠉⠉⠉⠉⠙⠛⡟⢀⣴⠁⡀⠉⠹⣿⣿⣿⣿⣷⣶⣾⣷⣌⣦⠈⣠⡤⡀⠑⢄⠀⠱⡀⠀
⣿⣿⣿⣿⣿⡟⠀⢹⡀⠻⣿⣿⣷⣄⣀⣀⡠⢤⡴⠖⠒⢒⣾⢁⣾⣿⣾⣿⣶⣤⣸⠙⠛⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣌⠀⠈⠳⡀⠱⡄
⣿⣿⣿⡿⡟⠀⠀⢸⠻⣆⠀⠙⠻⠿⣿⣦⣄⣀⣈⠙⣳⢞⣇⣾⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣶⣶⣾⣿⣿⣿⣿⣿⣿⣿⣷⣶⣶⣷⡀⠹
⣿⣿⣿⡿⡡⠀⠀⠘⡄⠈⠳⢄⡀⠀⠀⣈⠉⠛⣛⣿⠁⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀
⣿⣿⣿⠡⠁⠀⠀⠀⠘⢦⡀⠀⠉⣶⡀⠀⠙⠳⣿⠃⣸⣿⣿⣿⣿⡿⠿⣿⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧
⣿⡟⠅⠀⠀⠀⠀⠀⠀⠈⢿⠶⣤⣈⠛⠿⣿⣿⠃⢠⣿⣿⣿⣿⣿⣧⣤⣄⡁⠻⢿⣿⣿⣿⣿⣿⣿⣿⡿⠯⠿⡿⠟⢻⣿⣿⣿⣿⣿⡿
⡟⠀⠀⠀⠀⠀⠀⢀⣀⣤⣌⣷⣤⣉⠉⠒⠒⡏⠀⣾⣿⣿⣿⣿⣿⢿⣾⡈⡏⠳⣄⠙⠿⠟⠛⠉⣍⣠⣤⣤⣴⢤⣤⣼⣿⣿⣿⠻⣿⠃
⠁⠀⠀⠀⠀⠀⠀⡏⠻⣿⣿⣿⣿⣿⣿⣶⣾⡀⢸⣿⣿⣸⣿⣿⠉⠘⢿⠳⠬⠖⠚⠃⠀⠀⠀⠐⠋⠣⣀⣠⠏⣰⢻⣿⣿⣿⠏⡰⠃⠀
⠀⠀⠀⠀⠀⠀⠀⣳⡀⠀⠉⠙⠛⠛⠋⠁⣀⣈⣭⡟⣻⣿⣿⣿⠐⠤⠚⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠒⠒⠋⠀⣾⣿⣿⢏⡴⠁⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠙⣌⠛⠒⠂⠠⠤⠤⠴⠶⢶⣾⣿⣿⡿⢻⣿⡀⠀⠀⠐⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣟⡉⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠈⢳⣶⣤⣤⣤⣶⣦⣤⣴⣾⣿⣿⣿⡎⠻⣷⡤⣴⣾⣿⣷⣄⠀⠀⠠⠤⠒⠀⠀⣀⡴⢺⣿⣿⡃⢸⣿⣷⣦⣤⡄
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢁⠸⣿⣿⣿⣿⡿⠿⣛⣽⣿⣿⣿⣷⠀⠘⢿⣿⣿⠟⡳⠾⠷⢦⣀⣀⣤⣴⠾⢏⣰⣿⡿⠋⣿⣿⣿⣝⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣦⢤⣀⣀⣀⣀⣠⠤⢞⣫⣿⣿⡏⠀⠀⣼⡈⢿⣧⠱⢤⣶⠟⢹⢧⠻⣧⣉⠺⠿⡟⠀⣸⣿⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣦⣀⡠⠤⠤⣤⣶⣿⣿⣿⣿⣷⡄⠀⣷⢃⢈⣻⡿⢋⠟⢠⡇⠈⣆⠹⡙⠳⠖⠁⣰⠉⣿⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⡻⣟⣛⣛⡟⢛⡿⠟⠉⣼⣿⣿⣀⣿⣿⡛⠉⠀⠜⢀⣾⣷⡴⠛⣆⠹⣦⡀⡰⠋⡱⢹⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠘⢿⡇⣾⠋⣀⣠⣾⣿⣿⣿⣿⣿⣿⠁⠀⠀⠀⣸⢹⠀⠁⠁⠈⢦⣹⠏⠓⠚⣥⢸⡋⠟⠃⠙⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠔⠙⢮⣙⣛⣿⣿⣿⢿⣿⣿⣹⣼⣿⣿⣄⠀⠀⠀⢿⣼⠉⠳⡏⣽⠟⢻⠀⠀⢠⠃⢸⠁⠀⠀⠀⠈⣻
⣦⠒⠋⠀⠀⠀⠀⢀⠀⢀⣠⣤⢤⡾⠛⠛⠛⠛⠛⠉⠉⠉⠙⠁⣼⠻⣿⣿⡄⠀⠀⢸⡿⠐⢶⠁⠀⠀⡾⠐⢄⣨⠁⡆⠀⠀⠀⠀⣰⣿
⠁⠀⠀⠀⢀⣤⡚⣩⠟⠉⣿⣿⠋⠿⠏⠀⠀⠀⠀⠀⠀⠀⣼⢀⣿⡀⢻⣿⣷⠀⠀⠈⣷⠶⡎⠀⠀⠀⣿⠮⠘⣢⢰⠀⠀⠀⢀⣼⣿⣿
⠀⠀⡠⢞⡏⠀⢰⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣷⣬⣇⣽⣿⣿⣧⣠⣀⢹⢠⠁⠀⠀⠀⢣⡀⢙⡇⡇⠀⠀⢀⣾⣿⣿⣿
⠔⠋⠀⣾⠀⠀⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⢛⣻⣿⣿⣿⣾⣿⣮⣿⠀⠀⠀⠀⢸⠀⢫⣸⣿⣇⣠⣿⣿⣿⣿⣿
⠀⠀⡎⠉⠀⡄⠸⡄⢼⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣴⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⠈⣤⢼⣿⣿⣿⣿⣿⣿⣿⣿⣿
⠀⠀⡀⢠⠐⡀⠀⠘⢪⣄⠀⠀⠀⠀⣠⣤⣤⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠀⠀⣷⣿⣿⡿⣻⣿⣿⣿⣿⣿⣿";





        protected override void BeforeRun()
        {



                Console.WriteLine("Cosmos kernel booting...");
                
            try
            {
                this.vfs = new Cosmos.System.FileSystem.CosmosVFS();
                VFSManager.RegisterVFS(this.vfs);
            }
            catch (Exception e)
            {
                errorThingy.Error(e.ToString());
            }
            var disks = VFSManager.GetDisks();
            Console.WriteLine("Kernel sees "+ disks.Count +" disks.");

            foreach (var d in disks)
            {
                Console.WriteLine("Disk Size: " + d.Size / 1024 / 1024 +"MB | Partitions: "+d.Partitions.Count);
            }
            Cosmos.HAL.Global.PIT.Wait(2000);

            errorThingy.Fastfetch(currentver);
        }
        public string currentDirectory = @"0:\";
        public string currentDirectoryshown = @"0:\";
        protected override void Run()
        {
            try {
                Console.ForegroundColor = ConsoleColor.Green;
                if (currentDirectory == @"0:\")
                {
                    currentDirectoryshown = "i~like~flowers";
                }
                else
                {
                    currentDirectoryshown = currentDirectory;
                }
                Console.Write(currentDirectoryshown +"`# ");
                Console.ResetColor();
                var input = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(input)) return;
                var parts = input.Split(' ');
                string command = parts[0].ToLower();


                switch (command)
                {
                    case "fastfetch":
                        errorThingy.Fastfetch(currentver);
                        break;

                    case "help":
                        if (parts.Length > 1)
                        {
                            string subCommand = parts[1].ToLower();
                            switch (subCommand)
                            {
                                //this took too long :/
                                case "fastfetch": Console.WriteLine("fastfetch: just the OS banner. ALWAYS RUN IT AFTER CLS!!1"); break;
                                case "touch": Console.WriteLine("touch <file>: Creates an empty file. Do not harass the drive."); break;
                                case "append-text": Console.WriteLine("append-text <file> <text>: OVERwrites text to a file."); break;
                                case "ls": Console.WriteLine("ls: Lists files in the current directory"); break;
                                case "cls": Console.WriteLine("cls: Clears the screen."); break;
                                case "free": Console.WriteLine("free: Shows how much RAM you've gobbled up"); break;
                                case "mkdir": Console.WriteLine("Ok, now im just concerned. Nobody should be using a crappy CLI OS without knowing what mkdir is."); break;
                                case "cd": Console.WriteLine("HOW ON EARTH DO YOU NOT KNOW WHAT CD IS??? GET YOUR BUNS OFF LINUX AND GET BACK ONTO UR STINKY APPLE PC!!1"); break;
                                case "time": Console.WriteLine("time: Shows the current time... not much else."); break;
                                case "hello": Console.WriteLine("hello: You're talking to a computer. Go find a real friend."); break;
                                case "reboot": Console.WriteLine("reboot: restarts the OS in the event that your having some troubles."); break;
                                case "shutdown": Console.WriteLine("shutdown: shuts down the OS and if your in a vm closes the window."); break;
                                case "dmesg": Console.WriteLine("dmesg: doesnt do much as I dont know how to save kernel logs/too lazy to do it."); break;
                                case "flower": Console.WriteLine("flower: Displays the only beautiful thing in this entire CLI."); break;
                                case "recall-text": Console.WriteLine("recall-text <file>: Reads a file. Assuming you actually know how to read."); break;
                                case "format": Console.WriteLine("format: Deletes everything and formats your 0 drive to FAT32."); break;
                                case "rm": Console.WriteLine("rm <file>: Deletes a file. "); break;
                                case "yes": Console.WriteLine("yes: Spams 'y'. Press something to stop."); break;
                                case "whoami": Console.WriteLine("whoami: YOU ARE STUPID THATS WHAT."); break;
                                case "beep": Console.WriteLine("beep: now watch me beeeep- now watch me naynay *ok!*"); break;
                                case "theme": Console.WriteLine("theme <name>: Changes colors because you're picky. 'hacker' doesn't make you cool."); break;
                                case "uptime": Console.WriteLine("uptime: seconds from boot."); break;
                                case "wc": Console.WriteLine("wc <file>: Counts words in a file."); break;
                                case "base64": Console.WriteLine("base64 <text>: converts text into base64."); break;
                                case "kill": Console.WriteLine("kill: Doesn't do anything yet."); break;
                                case "cat": Console.WriteLine("cat <file>: Prints the contents of a file."); break;
                                case "devlist": Console.WriteLine("devlist: Lists disks."); break;
                                case "bsod": Console.WriteLine("bsod: The suicide button. Don't press it just because you're bored."); break;
                                case "suicide": Console.WriteLine("suicide: Crashes the system on purpose- just a test tool."); break;

                                default: Console.WriteLine("No help avalible for " +subCommand); break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Available Commands: " + avaliblecommands);
                            Console.WriteLine("Type 'help <command>' for more details.");
                            Console.WriteLine("GIMME ALL UR RAM -voblit");
                        }
                        break;

                    case "time":
                        Console.WriteLine("Current Date/Time: " + DateTime.Now.ToString());
                        break;

                    case "cls":
                        Console.Clear();
                        break;

                    case "echo":

                        if (parts.Length > 1)
                        {
                            string text = input.Substring(command.Length).Trim();
                            Console.WriteLine(text);
                        }
                        break;
                    case "hello":
                        Console.WriteLine("Hi! This is bob, your virtual assistant! If you see this, you clearly wanted to say hi! Thank you! More usefull stuff will be here soon, but for now, im here... waiting...");
                        break;

                    case "ls":
                        try
                        {
                            var files = VFSManager.GetDirectoryListing(currentDirectory);
                            foreach (var file in files)
                            {
                                string suffix = file.mEntryType == DirectoryEntryTypeEnum.Directory ? " <DIR>" : "      ";
                                Console.WriteLine(suffix + "  " + file.mName);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error: Could not read directory. Is the disk on fire? " + (rnd.Next(1, 3) == 1 ? "(> ~ <)" : "(T ~ T)"));
                        }
                        break;

                    case "reboot":
                        Console.WriteLine("Rebooting...");
                        Cosmos.HAL.Global.PIT.Wait(2000);
                        Sys.Power.Reboot();
                        break;

                    case "shutdown":
                        Console.WriteLine("Powering off...");
                        Cosmos.HAL.Global.PIT.Wait(2000);
                        Sys.Power.Shutdown();
                        break;

                    case "dmesg":
                        Console.WriteLine("unknown error: ?");
                        break;

                    case "flower":
                        Console.WriteLine("I Like");
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write("F");
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("l");
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write("o");
                        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("w");
                        Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("e");
                        Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("r");
                        Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("s");
                        Console.ResetColor();
                        Console.Write("!");
                        break;

                    case "append-text":
                        if (parts.Length > 2)
                        {
                            try
                            {

                                string filename = Path.Combine(currentDirectory, parts[1]);
                                string content = input.Substring(input.IndexOf(parts[2]));
                                using (var xStream = File.Create(filename))
                                {
                                    var xData = System.Text.Encoding.ASCII.GetBytes(content);
                                    xStream.Write(xData, 0, xData.Length);
                                }
                                Cosmos.HAL.Global.PIT.Wait(100);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Text saved to disk 0 at " + filename + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                                Console.ResetColor();
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usage: append-text <name.txt> <content>");
                        }
                        break;

                    case "recall-text":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                string filename = currentDirectory + parts[1];
                                if (File.Exists(filename))
                                {
                                    string data = File.ReadAllText(filename);
                                    Console.WriteLine(data);
                                }
                                else
                                {
                                    Console.WriteLine("Error: nothing at that location!!1");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                        }
                        break;

                    case "wipe":
                        try
                        {

                            foreach (var dev in Cosmos.HAL.BlockDevice.BlockDevice.Devices)
                            {
                                if (dev is Cosmos.HAL.BlockDevice.Partition part)
                                {
                                    Console.WriteLine("Found a partition. complete wipe");
                                    byte[] empty = new byte[512];


                                    part.WriteBlock(0, 1, ref empty);

                                    Console.WriteLine("Wipe successful! Rebooting...");
                                    Cosmos.HAL.Global.PIT.Wait(2000);
                                    Cosmos.System.Power.Reboot();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + ex.Message  + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                        }
                        break;

                    case "format":
                        try
                        {
                            Console.WriteLine("trying to FAT32 the drive... i think...");

                            var disks = VFSManager.GetDisks();

                            if (disks.Count > 0)
                            {
                                var disk = disks[0];
                                disk.Clear();


                                int safeSectors = (int)(disk.Size / 512) - 1024;

                                if (safeSectors <= 0)
                                {
                                    throw new Exception("Size Error");
                                }

                                disk.CreatePartition((int)(disk.Size / 512) - 128);
                                Console.WriteLine("Waiting for ATA Controller to catch up...");
                                Cosmos.HAL.Global.PIT.Wait(5000);
                                Console.WriteLine("Formatting FAT32...");
                                disk.FormatPartition(0, "FAT32", false);
                                Console.WriteLine("Format complete!!1 You can now use append-text." + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("REBOOT NOW TO MOUNT THE DISK.");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("No raw disks found. Check VM Settings!");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                            Console.ResetColor();
                        }
                        break;

                    case "rm":
                        if (parts.Length > 1)
                        {
                            string path = currentDirectory + parts[1];
                            try
                            {
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                    Console.WriteLine($"{path}" + " deleted");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)") );
                                Console.ResetColor();
                            }

                        }
                        break;
                    case "yes":
                        Console.WriteLine("press soemthing to stop it");
                        Cosmos.HAL.Global.PIT.Wait(1000);
                        while (!Console.KeyAvailable)
                        {
                            Console.WriteLine("y");
                        }
                        break;

                    case "whoami":
                        Console.WriteLine("I~Like~FlowersOS offical build version "+ currentver);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        bool isVM = false;

                        foreach (var device in Cosmos.HAL.PCI.Devices)
                        {
                            if (device.VendorID == 0x15AD || device.VendorID == 0x80EE)
                            {
                                isVM = true;
                                break;
                            }
                        }

                        if (isVM)
                        {
                            Console.WriteLine("ur in a vm do whatever you want" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                        }
                        else
                        {
                            Console.WriteLine("STATUS: BARE METAL. careful formatting...");
                        }
                        Console.ResetColor();
                        break;

                    case "free":
                        uint ramIate = Cosmos.Core.GCImplementation.GetUsedRAM() / 1024 / 1024;
                        uint ramLeftToeat = Cosmos.Core.CPU.GetAmountOfRAM();
                        Console.WriteLine("ram used: " + ramIate + "/MB out of " + ramLeftToeat + "/MB total ram");
                        break;

                    case "beep":
                        Console.WriteLine("Now watch me beeep");
                        Cosmos.System.PCSpeaker.Beep(440, 500);
                        Console.WriteLine("now watch me naynay");
                        break;

                    case "theme":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Usage: theme <name>");
                            Console.WriteLine("Themes: rose, violet, sunflower, hacker, default");
                        }
                        else
                        {
                            string choice = parts[1].ToLower();

                            switch (choice)
                            {
                                case "rose":
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                case "violet":
                                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case "sunflower":
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                                case "hacker":
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case "default":
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                default:
                                    Console.WriteLine("Theme not found! Try 'rose' or 'sunflower'.");
                                    break;
                            }

                            Console.Clear();
                            Console.WriteLine("--- Theme updated to " + choice + " ---" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                        }
                        break;

                    case "uptime":
                        Console.WriteLine("Cpu is " + Cosmos.Core.CPU.GetCPUBrandString());
                        Console.WriteLine("Uptime is " + Cosmos.Core.CPU.GetCPUUptime() / 1000000000 +" seconds.");
                        break;

                    case "wc":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                string path = currentDirectory + parts[1];
                                string content = File.ReadAllText(path);
                                Console.WriteLine(content.Split('\n').Length + "lines,"+ content.Split(' ').Length+ " words");
                            }
                            catch { Console.WriteLine("uhm its not there... :/"); }
                        }
                        break;

                    case "touch":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                string path = Path.Combine(currentDirectory, parts[1]);
                                var fileStream = File.Create(path);
                                fileStream.Close();

                                Console.WriteLine("File created successfully" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                            }
                        }
                        break;

                    case "base64":
                        if (parts.Length > 1)
                        {
                            var bytesinplaintext = System.Text.Encoding.UTF8.GetBytes(parts[1]);
                            Console.WriteLine(Convert.ToBase64String(bytesinplaintext));
                        }
                        break;


                    case "kill":
                        Console.WriteLine("nothing here yet...");
                        break;

                    case "mkdir":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                Directory.CreateDirectory(currentDirectory + parts[1]);
                                Console.WriteLine("Folder created!" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                        }
                        break;
                    case "cd":
                        if (parts.Length > 1)
                        {
                            string target = parts[1];
                            if (target == "..")
                            {
                                currentDirectory = @"0:\";
                            }
                            else
                            {
                                string NeWpAtH = Path.Combine(currentDirectory, target);

                                if (!NeWpAtH.EndsWith(@"\")) NeWpAtH += @"\";

                                if (Directory.Exists(NeWpAtH))
                                    currentDirectory = NeWpAtH;
                                else
                                    Console.WriteLine("Directory not found!"  + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                            }
                        }
                        break;


                    case "cat":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                string placewhereyouat = Path.Combine(currentDirectory, parts[1]);
                                if (File.Exists(placewhereyouat))
                                {
                                    string[] lines = File.ReadAllText(placewhereyouat).Split('\n');
                                    foreach (string line in lines)
                                    {
                                        Console.WriteLine(line);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nothing there...");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usage: cat <filename>");
                        }
                        break;

                    case "suicide":
                        Console.WriteLine("Dying shortly");
                        Cosmos.HAL.Global.PIT.Wait(1000);
                        throw new Exception("Manual_Sys_Fail_It_Works_Yay");
                        break;

                    case "bsod":
                        errorThingy.Error("User_Is_Stupid");
                        break;
                    
                    case "devlist":
                        var drivesig = VFSManager.GetDisks();
                        Console.WriteLine("Found " + drivesig.Count+ " disks.");
                         int diskno = 0;
                        foreach (var drive in drivesig)
                        {
                            diskno++;
                            Console.WriteLine("Disk: " + diskno + "| Partitions:" + drive.Partitions.Count);
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unknown input: " + input  + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                        Console.ResetColor();
                        break;

                }
            }
            catch (Exception E)
            {

                errorThingy.Error(E.ToString());
            }

        }
    }

    }
