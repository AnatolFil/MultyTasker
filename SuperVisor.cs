using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    class SuperVisor
    {
        private static int Finishers = 0;
        private static Object Locker = new object();
        public static void CreateAndStartWorkers(int AmountOfWorkers, int Difficulty = 1000, int WorkAmount = 100)
        {
            for (int i = 0; i < AmountOfWorkers; i++)
            {
                Worker work = new Worker(WorkAmount, Difficulty);
                work.Position = i;
                Thread t = new Thread(new ThreadStart(work.Work));
                t.Start();
            }
        }

        public static void GetFinishPosition(ref int FinishPosition)
        {
            lock (Locker)
            { 
                Finishers++;
                FinishPosition = Finishers;
            }
        }
    }
}
