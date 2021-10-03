using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    class OutputHandler
    {
        //Sycronization object
        public static Object Locker = new Object();
        static OutputHandler()
        {
            Console.Title = "MultyTasker";
            Console.SetWindowSize(Console.WindowWidth + 5, Console.WindowHeight);
        }
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
    }
}
