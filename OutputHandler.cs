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
        
        public static void WriteProgressInfo(Worker worker)
        {
            Console.CursorVisible = false;
            lock(Locker)
            {
                Console.SetCursorPosition(0, worker.Position);
                Console.WriteLine(worker.Position + " (" + worker.ManagedThreadId + ") " + worker.Progress);
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
