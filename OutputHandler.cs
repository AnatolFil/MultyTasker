using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    //Write info in console, use synchronization object
    //to order work with console
    class OutputHandler
    {
        //Sycronization object
        public static Object Locker = new Object();
        static OutputHandler()
        {
            Console.Title = "MultyTasker";
            Console.SetWindowSize(Console.WindowWidth + 5, Console.WindowHeight);
        }

        //Write information about worker position and ManagedThreadId in console
        public static void WriteWorkerInfo(Worker worker)
        {
            Console.CursorVisible = false;
            lock(Locker)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string WorkerInfo = String.Format("{0:d2}({1:d2}) ", worker.Position, worker.ManagedThreadId);
                Console.SetCursorPosition(0, worker.Position);
                Console.Write(WorkerInfo);
            } 
        }

        //Write progress in console, if there is exception it change ForegroundColor
        //on red
        public static void WriteWorkerProgress(Worker worker, bool IsError = false)
        {
            Console.CursorVisible = false;
            lock (Locker)
            {
                Console.SetCursorPosition(6 + worker.Progress, worker.Position);
                if (IsError == false)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red; 
                Console.Write("\u2590");
                Console.ForegroundColor = ConsoleColor.Green;
                string ProgressInfo = String.Format(" {0:d2} ", worker.Progress);
                Console.Write(ProgressInfo);
                //Console.WriteLine(WorkerInfo);
            }
        }

        //Write finish position and spended time in console
        public static void WriteFinPositionAndTime(Worker worker)
        {
            Console.CursorVisible = false;
            lock (Locker)
            {
                Console.SetCursorPosition(worker.Progress + 7 , worker.Position);
                string FinishInfo = String.Format(" {0:d2} ({1:N7})", worker.FinishPosition, worker.TotalTime);
                Console.WriteLine(FinishInfo);
            }
        }

        //Write in console about unhandled error
        public static void WriteUnhadledException(Worker worker, string Msg)
        {
            Console.CursorVisible = false;
            lock (Locker)
            {
                Console.SetCursorPosition(Console.WindowWidth/2, worker.Position);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unhandled Error!");//Would be perfect to write error info in log file
            }
        }
    }
}
