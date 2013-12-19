using System.IO;
using System.Threading;
using NetworkController.Logic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkController.Logic.Server;

namespace NetworkController.ConsoleApp
{
    class Program
    {
        private static bool IsRunning = false;
        private static Thread WatcherThread = new Thread(BroadcastWatcherThread) { Name = "Broadcast watcher thread"};
        static void BroadcastWatcherThread()
        {
            IsRunning = true;
            using (var b = new UdpBroadcaster())
            {
                b.StartBroadcasting();
                long lastPacketCont = 0;
                while (IsRunning)
                {
                    b.VelocityRetentionFactor = 0.97; //TODO: Dummy code, move into config or somethin
                    DumpExceptions(b.PopExceptions());
                    if (b.PacketsSent != lastPacketCont)
                    {
                        //Console.WriteLine(">>> {0} / {1:0.00}MB", b.PacketsSent, b.PacketVolumeSent/1048576.0);
                        Console.Write(".");
                        lastPacketCont = b.PacketsSent;
                    }
                    Thread.Sleep(100);
                }
                b.StopBroadcasting();

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
}
