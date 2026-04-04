//******************************************************************************

// * FLOWER OS - Version 1.2.fix (MV  and CP command Update, oh and also funneling :P)

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
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.Listing;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;

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
            Kernel.LineWrite("ITS DEAD OH NO!!1 " + (rnd.Next(1, 3) == 1 ? "(> ~ <)" : "(T ~ T)"));
            Kernel.LineWrite("I~like~flowersOS has detected a problem and has shut down to prevent ");
            Kernel.LineWrite("further damage to your system... or ur sanity... hehe");
            Kernel.LineWrite("---------------------------------------------");
            Kernel.LineWrite("Err Code:" + e);
            Kernel.LineWrite("---------------------------------------------");
            Kernel.LineWrite("***technical info 0x594F555F4152455F5354555049445F");
            //translation ^^^ means YOU_ARE_STUPID_
            Kernel.LineWrite("***technical info 0x4745545F4F46465F4C494E5558");
            //translation ^^^ means GET_OFF_LINUX
            Kernel.LineWrite("***technical info 0x474F5F544F5543485F4752415353");
            //translation ^^^ means GO_TOUCH_GRASS
            Kernel.LineWrite("\nIf this is the first time you've seen this Stop error screen,");

            Kernel.LineWrite("restart your computer. If this screen appears again, follow");
            Kernel.LineWrite("these steps:");

            Kernel.LineWrite("\nCheck to make sure any new hardware or software is properly installed.");
            Kernel.LineWrite("If this is a new installation, ask your hardware or software manufacturer");
            Kernel.LineWrite("why you are like this.");
            Console.Write(Environment.NewLine);
            while (true)
            {

                Cosmos.Core.CPU.Halt();
            }
        }
        public static void NeoFetch(string currentver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Kernel.LineWrite($@"
                     .-~~~-
                .-~~~_._~~~\   
                /~-~~   ~.  `._ I like flowers OS
               /    \     \  | ~~-_ I like flowers OS
       __     |      |     | |  /~\|   I like flowers OS
   _-~~  ~~-..|       ______||/__..-~~/
    ~-.___     \     /~\_________.-~~
         \~~--._\   |             / Current version {currentver}
          ^-_    ~\  \          /^
             ^~---|~~~~-.___.-~^
               /~^| | | |^~\ Uptime: {Cosmos.Core.CPU.GetCPUUptime() / 1000000000}
              //~^`/ /_/ ^~\\ Ram: IDK.
              /   //~||      \
                 ~   || Your CPU: {Cosmos.Core.CPU.GetCPUBrandString()}
          ___      -(||      __ ___ _
         |\|  \       ||_.-~~ /|\-  \~-._
         | -\| |      ||/   /  | |\- | |\ \
          \__-\|______ ||  |    \___\|  \_\|
    _____ _.-~/|\     \\||  \  |  /       ~-.
  /'  --/|  / /|  \    \||    \ /          |\~-
 ' ---/| | |   |\  |     ||                 \__|
| --/| | ;  \ /|  /    -(|| The time is {DateTime.Now.ToString()}
`./  |  /     \|/        ||)-
  `~^~^                  ||
");
            Console.ResetColor();
        }
        public static void Fastfetch(string currentver)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Kernel.LineWrite(@"
 _______ ___    _________ _________
 |_   _| | |    |_   _| |/ /  ____|                     
   | |   | |      | | | ' /| |__                        
   | |   | |      | | |  < |  __|                       
  _| |_  | |____ _| |_| . \| |____                      
 |_____| |______|_____|_|\_\______|");
            Console.ForegroundColor = ConsoleColor.Blue;
            Kernel.LineWrite(@"
 _________      ______          ______________   ______
 |  ____| |    / __ \ \        / /  ____|  __ \ / ____| 
 | |__  | |   | |  | \ \  /\  / /| |__  | |__) | (___   
 |  __| | |   | |  | |\ \/  \/ / |  __| |  _  / \___ \  
 | |    | |___| |__| | \  /\  /  | |____| | \ \ ____) | 
 |_|___ |______\____/   \/  \/   |______|_|  \_\_____/  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Kernel.LineWrite(@"
   ____   ______
  / __ \ / ____|                                        
 | |  | | (___                                          
 | |  | |\___ \                                         
 | |__| |____) |                                        
  \____/|_____/                                         
_____________________I LIKE FLOWERS OS VERSION: " + currentver + "________________________");
            Console.ResetColor();
            Kernel.LineWrite("Current Date/Time: " + DateTime.Now.ToString());
            Kernel.LineWrite("Cpu is " + Cosmos.Core.CPU.GetCPUBrandString());
            Kernel.LineWrite("Uptime is " + Cosmos.Core.CPU.GetCPUUptime() / 1000000000);
        }
    }
    public class Kernel : Sys.Kernel
    {
        public static string whateveristheretofunnel = "";
        public static bool amIfunneling = false;
        public static string funnelingto = "";

        public static void LineWrite(string text)
        {
            if (amIfunneling)
            {
                whateveristheretofunnel += text + "\n";
            }
            else
            {
                Console.WriteLine(text);
            }
        }
        Random rnd = new Random();
        //i think i need to update... i think
        public string avaliblecommands = "neofetch, banner, help, time, cls, echo, hello, ls, reboot, shutdown, dmesg, flower, append-text, recall-text, format, rm, yes, whoami, free, beep, theme, uptime, wc, touch, base64, kill, mkdir, cd, cat, devlist, df, funnel, mv, cp";
        Canvas canvas;
        private CosmosVFS vfs;
        public string currentver = "1.2.fix";

        //(^-^)
        //dont even think of removing it
        //HOURS WASTED TRYING TO MAKE THIS SHOW UP IN THE OS AND THEN IT JUST DOES ?????????: 5
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



            LineWrite("Cosmos kernel booting...");

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
            LineWrite("Kernel sees " + disks.Count + " disks.");

            foreach (var d in disks)
            {
                LineWrite("Disk Size: " + d.Size / 1024 / 1024 + "MB | Partitions: " + d.Partitions.Count);
            }
            Cosmos.HAL.Global.PIT.Wait(2000);

            errorThingy.Fastfetch(currentver);
        }
        public string currentDirectory = @"0:\";
        public string currentDirectoryshown = @"0:\";
        protected override void Run()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (currentDirectory == @"0:\")
                {
                    currentDirectoryshown = "i~like~flowers";
                }
                else
                {
                    currentDirectoryshown = currentDirectory;
                }
                Console.Write($"{currentDirectoryshown}`# ");
                Console.ResetColor();

                var input = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(input)) return;

            starthere: 

                var parts = input.Split(' ');
                string command = parts[0].ToLower();


                switch (command)
                {
                    case "banner":
                        errorThingy.Fastfetch(currentver);
                        break;

                    case "neofetch":
                        errorThingy.NeoFetch(currentver);
                        break;

                    case "help":
                        if (parts.Length > 1)
                        {
                            string subCommand = parts[1].ToLower();
                            switch (subCommand)
                            {
                                //this took too long :/
                                case "df": LineWrite("Check how much used in your current folder, go to root to see total drive size."); break;
                                case "funnel": LineWrite("funnel <command> <output.txt> funnels command's output and puts into the designated file"); break;
                                case "neofetch": LineWrite("neofetch: OS logo and stats. ALWAYS RUN IT AFTER CLS!!1"); break;
                                case "banner": LineWrite("This is the OG OS banner left just because its multicolor :)"); break;
                                case "touch": LineWrite("touch <file>: Creates an empty file. Do not harass the drive."); break;
                                case "append-text": LineWrite("append-text <file> <text>: OVERwrites text to a file."); break;
                                case "ls": LineWrite("ls: Lists files in the current directory"); break;
                                case "cls": LineWrite("cls: Clears the screen."); break;
                                case "free": LineWrite("free: Shows how much RAM you've gobbled up"); break;
                                case "mkdir": LineWrite("Ok, now im just concerned. Nobody should be using a crappy CLI OS without knowing what mkdir is."); break;
                                case "cd": LineWrite("HOW ON EARTH DO YOU NOT KNOW WHAT CD IS??? GET YOUR BUNS OFF LINUX AND GET BACK ONTO UR STINKY APPLE PC!!1"); break;
                                case "time": LineWrite("time: Shows the current time... not much else."); break;
                                case "hello": LineWrite("hello: You're talking to a computer. Go find a real friend."); break;
                                case "reboot": LineWrite("reboot: restarts the OS in the event that your having some troubles."); break;
                                case "shutdown": LineWrite("shutdown: shuts down the OS and if your in a vm closes the window."); break;
                                case "dmesg": LineWrite("dmesg: doesnt do much as I dont know how to save kernel logs/too lazy to do it."); break;
                                case "flower": LineWrite("flower: Displays the only beautiful thing in this entire CLI."); break;
                                case "recall-text": LineWrite("recall-text <file>: Reads a file. Assuming you actually know how to read."); break;
                                case "format": LineWrite("format: Deletes everything and formats your 0 drive to FAT32."); break;
                                case "rm": LineWrite("rm <file>: Deletes a file. "); break;
                                case "yes": LineWrite("yes: Spams 'y'. Press something to stop."); break;
                                case "whoami": LineWrite("whoami: YOU ARE STUPID THATS WHAT."); break;
                                case "beep": LineWrite("beep: now watch me beeeep- now watch me naynay *ok!*"); break;
                                case "theme": LineWrite("theme <name>: Changes colors because you're picky. 'hacker' doesn't make you cool."); break;
                                case "uptime": LineWrite("uptime: seconds from boot."); break;
                                case "wc": LineWrite("wc <file>: Counts words in a file."); break;
                                case "base64": LineWrite("base64 <text>: converts text into base64."); break;
                                case "kill": LineWrite("kill: Doesn't do anything yet."); break;
                                case "cat": LineWrite("cat <file>: Prints the contents of a file."); break;
                                case "devlist": LineWrite("devlist: Lists disks."); break;
                                case "bsod": LineWrite("bsod: The suicide button. Don't press it just because you're bored."); break;
                                case "suicide": LineWrite("suicide: Crashes the system on purpose- just a test tool."); break;
                                case "mv": LineWrite("mv <source> <destination> moves the file from the source to the destination."); break;
                                case "cp": LineWrite("cp <source> <destination> copies a file with a set new name, NOT THAT you freaky degenerate!"); break;

                                default: LineWrite("No help avalible for " + subCommand); break;
                            }
                        }
                        else
                        {
                            LineWrite("Available Commands: " + avaliblecommands);
                            LineWrite("Type 'help <command>' for more details.");
                            LineWrite("GIMME ALL UR RAM -voblit");
                        }
                        break;

                    case "time":
                        LineWrite("Current Date/Time: " + DateTime.Now.ToString());
                        break;

                    case "cls":
                        Console.Clear();
                        break;

                    case "echo":

                        if (parts.Length > 1)
                        {
                            string text = input.Substring(command.Length).Trim();
                            LineWrite(text);
                        }
                        break;
                    case "hello":
                        LineWrite("Hi! This is bob, your virtual assistant! If you see this, you clearly wanted to say hi! Thank you! More usefull stuff will be here soon, but for now, im here... waiting...");
                        break;

                    case "ls":
                        try
                        {
                            var files = VFSManager.GetDirectoryListing(currentDirectory);
                            foreach (var file in files)
                            {
                                string suffix = file.mEntryType == DirectoryEntryTypeEnum.Directory ? " <DIR>" : "      ";
                                LineWrite(suffix + "  " + file.mName);
                            }
                        }
                        catch
                        {
                            LineWrite("Error: Could not read directory. Is the disk on fire? " + (rnd.Next(1, 3) == 1 ? "(> ~ <)" : "(T ~ T)"));
                        }
                        break;

                    case "reboot":
                        LineWrite("Rebooting...");
                        Cosmos.HAL.Global.PIT.Wait(2000);
                        Sys.Power.Reboot();
                        break;

                    case "cp":
                        if (parts.Length > 2)
                        {
                            string srctocp = Path.Combine(currentDirectory, parts[1].Trim());
                            string desttocp = Path.Combine(currentDirectory, parts[2].Trim());
                            try
                            {
                                File.Copy(srctocp, desttocp);
                                LineWrite("File successfully copied! " + desttocp + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)")); 
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                            
                        }
                        else {
                            LineWrite("Usage: cp <source file> <destination file>");
                        }
                        break;

                    case "mv":
                        if (parts.Length > 2)
                        {
                            string srctomv = Path.Combine(currentDirectory, parts[1].Trim());
                            string desttomv = Path.Combine(currentDirectory, parts[2].Trim());
                            try
                            {
                            //emergancy move fix
                                byte[] fileData = File.ReadAllBytes(srctomv);
                                File.WriteAllBytes(desttomv, fileData);
                                File.Delete(srctomv);
                                LineWrite("File successfully moved! " + desttomv + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                            }
                            catch (Exception e)
                            {
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                            }
                            
                        }
                        else
                        {
                            LineWrite("Usage: mv <source file> <destination file.");
                        }
                        break;

                    case "shutdown":
                        LineWrite("Powering off...");
                        Cosmos.HAL.Global.PIT.Wait(2000);
                        Sys.Power.Shutdown();
                        break;

                    case "dmesg":
                        LineWrite("unknown error: ?");
                        break;

                    case "flower":
                        LineWrite("I Like");
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write("F");
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("l");
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write("o");
                        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("w");
                        Console.ForegroundColor = ConsoleColor.Magenta; LineWrite("e");
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
                                LineWrite("Text saved to disk 0 at " + filename + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                                Console.ResetColor();
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            LineWrite("Usage: append-text <name.txt> <content>");
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
                                    LineWrite(data);
                                }
                                else
                                {
                                    LineWrite("Error: nothing at that location!!1");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
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
                                    LineWrite("Found a partition. complete wipe");
                                    byte[] empty = new byte[512];


                                    part.WriteBlock(0, 1, ref empty);

                                    LineWrite("Wipe successful! Rebooting...");
                                    Cosmos.HAL.Global.PIT.Wait(2000);
                                    Cosmos.System.Power.Reboot();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LineWrite("An error occured and execution of the command failed. ERR_CODE: " + ex.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                        }
                        break;

                    case "format":
                        try
                        {
                            LineWrite("trying to FAT32 the drive... i think...");

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
                                LineWrite("Waiting for ATA Controller to catch up...");
                                Cosmos.HAL.Global.PIT.Wait(5000);
                                LineWrite("Formatting FAT32...");
                                disk.FormatPartition(0, "FAT32", false);
                                LineWrite("Format complete!!1 You can now use append-text." + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("REBOOT NOW TO MOUNT THE DISK.");
                                Console.ResetColor();
                            }
                            else
                            {
                                LineWrite("No raw disks found. Check VM Settings!");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
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
                                    LineWrite($"{path}" + " deleted");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }

                        }
                        break;
                    case "yes":
                        LineWrite("press soemthing to stop it");
                        Cosmos.HAL.Global.PIT.Wait(1000);
                        while (!Console.KeyAvailable)
                        {
                            LineWrite("y");
                        }
                        break;

                    case "whoami":
                        LineWrite("I~Like~FlowersOS offical build version " + currentver);
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
                            LineWrite("ur in a vm do whatever you want" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                        }
                        else
                        {
                            LineWrite("STATUS: BARE METAL. careful formatting...");
                        }
                        Console.ResetColor();
                        break;

                    case "free":
                        uint ramIate = Cosmos.Core.GCImplementation.GetUsedRAM() / 1024 / 1024;
                        uint ramLeftToeat = Cosmos.Core.CPU.GetAmountOfRAM();
                        LineWrite("ram used: " + ramIate + "/MB out of " + ramLeftToeat + "/MB total ram");
                        break;

                    case "beep":
                        LineWrite("Now watch me beeep");
                        for (int i = 0; i < 3; i++)
                        {
                            Cosmos.System.PCSpeaker.Beep(2000, 50);
                            Cosmos.System.PCSpeaker.Beep(2500, 50);
                        }
                        LineWrite("now watch me naynay");
                        break;

                    case "theme":
                        if (parts.Length < 2)
                        {
                            LineWrite("Usage: theme <name>");
                            LineWrite("Themes: rose, violet, sunflower, hacker, default");
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
                                    LineWrite("Theme not found! Try 'rose' or 'sunflower'.");
                                    break;
                            }

                            Console.Clear();
                            LineWrite("--- Theme updated to " + choice + " ---" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                        }
                        break;

                    case "uptime":
                        LineWrite("Cpu is " + Cosmos.Core.CPU.GetCPUBrandString());
                        LineWrite("Uptime is " + Cosmos.Core.CPU.GetCPUUptime() / 1000000000 + " seconds.");
                        break;

                    case "wc":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                string path = currentDirectory + parts[1];
                                string content = File.ReadAllText(path);
                                LineWrite(content.Split('\n').Length + "lines," + content.Split(' ').Length + " words");
                            }
                            catch { LineWrite("uhm its not there... :/"); }
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

                                LineWrite("File created successfully" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                            }
                            catch (Exception e)
                            {
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                            }
                        }
                        break;

                    case "funnel":
                        if (amIfunneling) break;
                        if (parts.Length >= 3)
                        {
                            funnelingto = parts[parts.Length - 1];
                            whateveristheretofunnel = "";
                            amIfunneling = true;
                            int startofthecommand = 7;
                            int grabbyLength = input.Length - startofthecommand - funnelingto.Length;
                            string theactuallcommandyourrunning = input.Substring(startofthecommand, grabbyLength).Trim();
                            input = theactuallcommandyourrunning;
                            goto starthere;
                        }
                        else
                        {
                            LineWrite("Usage: funnel <command> <file.txt>");
                        }
                        break;

                    case "base64":
                        if (parts.Length > 1)
                        {
                            var bytesinplaintext = System.Text.Encoding.UTF8.GetBytes(parts[1]);
                            LineWrite(Convert.ToBase64String(bytesinplaintext));
                        }
                        break;


                    case "kill":
                        LineWrite("nothing here yet...");
                        break;

                    case "mkdir":
                        if (parts.Length > 1)
                        {
                            try
                            {
                                Directory.CreateDirectory(currentDirectory + parts[1]);
                                LineWrite("Folder created!" + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
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
                                    LineWrite("Directory not found!" + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
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
                                        LineWrite(line);
                                    }
                                }
                                else
                                {
                                    LineWrite("Nothing there...");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            LineWrite("Usage: cat <filename>");
                        }
                        break;

                    case "suicide":
                        LineWrite("Dying shortly");
                        Cosmos.HAL.Global.PIT.Wait(1000);
                        throw new Exception("Manual_Sys_Fail_It_Works_Yay");
                        break;

                    case "df":
                        try
                        {
                            var volumes = VFSManager.GetAvailableFreeSpace(currentDirectory);
                            LineWrite("you have " + (volumes / (1024 * 1024)).ToString() + " space left");
                        }
                        catch (Exception e)
                        {
                            LineWrite("An error occured and execution of the command failed. ERR_CODE: " + e.Message + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                        }
                        break;

                    case "bsod":
                        errorThingy.Error("User_Is_Stupid");
                        break;

                    case "devlist":
                        var drivesig = VFSManager.GetDisks();
                        LineWrite("Found " + drivesig.Count + " disks.");
                        int diskno = 0;
                        foreach (var drive in drivesig)
                        {
                            diskno++;
                            LineWrite("Disk: " + diskno + "| Partitions:" + drive.Partitions.Count);
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        LineWrite("Unknown input: " + input + (rnd.Next(1, 3) == 1 ? " (> ~ <)" : " (T ~ T)"));
                        Console.ResetColor();
                        break;

                }
            }
            catch (Exception E)
            {

                errorThingy.Error(E.ToString());
            }
            if (amIfunneling)
            {
                amIfunneling = false;
                try
                {
                    string fullPath = Path.Combine(currentDirectory, funnelingto);
                    File.WriteAllText(fullPath, whateveristheretofunnel);
                    Console.WriteLine("funneling worked!!1 " + funnelingto + (rnd.Next(1, 3) == 1 ? " (^ - ^)" : " (^ v ^)"));
                }
                catch { /* Stay silent if save fails */ }
            }
        }
    }

}
