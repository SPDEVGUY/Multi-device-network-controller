using System.IO;
using System.Threading;
using System;
using System.Collections.Generic;
using NetworkController.Server.Logic;

namespace NetworkController.Server.ConsoleApp
{
    class Program
    {
        private static bool IsRunning = false;
        private static Thread WatcherThread = new Thread(BroadcastWatcherThread) { Name = "Broadcast watcher thread"};

        static void BroadcastWatcherThread()
        {
            IsRunning = true;
            using (var b = new Broadcaster())
            {
                b.Start();
                
                //TODO: Dummy code, move into config or somethin
                b.BroadcastPort = 9050;
                b.VelocityRetentionFactor = 0.97; 


                long lastPacketCont = 0;
                while (IsRunning)
                {
                    DumpExceptions(b.PopExceptions());
                    if (b.PacketsSent != lastPacketCont)
                    {
                        Console.Clear();
                        Console.WriteLine(">>> {0} / {1:0.00}MB", b.PacketsSent, b.PacketVolumeSent/1048576.0);
                        Console.WriteLine("-----------");
                        Console.WriteLine(b.LastBroadcastData.PrettifyJson());
                        lastPacketCont = b.PacketsSent;
                    }
                    Thread.Sleep(100);
                }
                b.Stop();
                Thread.Sleep(100);
                DumpExceptions(b.PopExceptions());
            }
            Console.WriteLine("Stopped.");
        }

        static void Main(string[] args)
        {
            WatcherThread.Start();
            Console.ReadLine();
            IsRunning = false;


            Console.ReadLine();
        }

        public static void DumpExceptions(List<Exception> exceptions)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            foreach (var e in exceptions)
            {
                File.AppendAllText(
                    "exceptions.txt",
                    string.Format(
                        @"

-----------------------------------
{0}
-----------------------------------

{1}

-----------------------------------

{2}
=============================================

", e.Message, e, e.InnerException
                        )
                    );
                Console.WriteLine("ERROR: \t{0}",e.Message);
                if(e.InnerException!= null) Console.WriteLine("\t\t\t{0}",e.InnerException.Message);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    public static class StringExtensions
    {
        public static string PrettifyJson(this string input)
        {
            return input //Some shitty logic to newline after stuff
                .Replace("{", "{" + Environment.NewLine)
                .Replace("[", "{" + Environment.NewLine)
                .Replace("}", Environment.NewLine + "}")
                .Replace(",\"", "," + Environment.NewLine + "\t\"")
                .Replace("]",  Environment.NewLine + "]");
        }
    }
}
