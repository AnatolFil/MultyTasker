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
        struct WorkerParam
        {
            public int AmountOfWorkers;
            public int Difficulty;
            public int WorkAmount;
        }
        private static int Finishers = 0;
        private static Object Locker = new object();
        private static void CreateAndStartWorkers(object workerParam)
        {
            WorkerParam WP =(WorkerParam)workerParam;
            Thread[] Treads = new Thread[WP.AmountOfWorkers];
            for (int i = 0; i < WP.AmountOfWorkers; i++)
            {
                Worker work = new Worker(WP.WorkAmount, WP.Difficulty);
                work.Position = i;
                Thread w = new Thread(new ThreadStart(work.Work));
                w.Start();
                Treads[i] = w;
            }
            foreach(Thread thr in Treads)
            {
                thr.Join();
            }
            WriteAllDone(WP);
        }

        public static void CreateAndStartWorkersAsync(int AmountOfWorkers, int Difficulty = 1000, int WorkAmount = 100)
        {
            WorkerParam WP = new WorkerParam();
            WP.AmountOfWorkers = AmountOfWorkers;
            WP.Difficulty = Difficulty;
            WP.WorkAmount = WorkAmount;
            Thread w = new Thread(CreateAndStartWorkers);
            w.Start(WP);
        }

        public static void GetFinishPosition(ref int FinishPosition)
        {
            lock (Locker)
            { 
                Finishers++;
                FinishPosition = Finishers;
            }
        }

        private static void WriteAllDone(WorkerParam WP)
        {
            Console.SetCursorPosition(0, WP.AmountOfWorkers);
            Console.WriteLine("All done!");
        }
    }
}
