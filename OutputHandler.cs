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

        public static void WriteWorkerProgress(Worker worker)
        {
            Console.CursorVisible = false;
            lock (Locker)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(6+worker.Progress, worker.Position);
                string ProgressInfo = String.Format("\u2597 {0:d2} ", worker.Progress);
                Console.Write(ProgressInfo);
                //Console.WriteLine(WorkerInfo);
            }
        }

        public static void WriteFinPositionAndTime(Worker worker)
        {
            Console.CursorVisible = false;
            lock (Locker)
            {
                Console.SetCursorPosition(0, worker.Position);
                Console.WriteLine(worker.Position + " (" + worker.ManagedThreadId + ") " + worker.Progress + " " + worker.FinishPosition + " " + worker.TotalTime);
            }
        }
    }
}
