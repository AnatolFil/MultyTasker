using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    //Look after workers, give them info about their finish position
    //Wait when all workers finish work and say All done in console 
    class SuperVisor
    {
        //Struct to send it in thread as param
        struct WorkerParam
        {
            public int AmountOfWorkers;
            public int Difficulty;
            public int WorkAmount;
        }

        //Counter of finishers
        //It will be increase when some worker finish his work
        //and ask info about his finish position
        private static int Finishers = 0;

        //Sycronization object
        private static Object Locker = new object();

        //Create threads and wait all of them, then write in console message All done
        private static void CreateAndStartWorkers(object workerParam)
        {
            try
            {
                WorkerParam WP = (WorkerParam)workerParam;
                Thread[] Treads = new Thread[WP.AmountOfWorkers];
                for (int i = 0; i < WP.AmountOfWorkers; i++)
                {
                    Worker work = new Worker(WP.WorkAmount, WP.Difficulty);
                    work.Position = i;
                    Thread w = new Thread(new ThreadStart(work.Work));
                    w.Start();
                    Treads[i] = w;
                }
                foreach (Thread thr in Treads)
                {
                    thr.Join();
                }
                WriteAllDone(WP);
            }
            catch(Exception e)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(e.Message);
            }
        }

        //Create thread which will create workers and will wait them
        public static void CreateAndStartWorkersAsync(int AmountOfWorkers, int Difficulty = 1000, int WorkAmount = 100)
        {
            WorkerParam WP = new WorkerParam();
            WP.AmountOfWorkers = AmountOfWorkers;
            WP.Difficulty = Difficulty;
            WP.WorkAmount = WorkAmount;
            Thread w = new Thread(CreateAndStartWorkers);
            w.Start(WP);
        }

        //Give finish position for workers
        //It use sycronization object to order access to Finishers
        public static void GetFinishPosition(ref int FinishPosition)
        {
            lock (Locker)
            { 
                Finishers++;
                FinishPosition = Finishers;
            }
        }

        //Write in console messagew All done
        private static void WriteAllDone(WorkerParam WP)
        {
            Console.SetCursorPosition(0, WP.AmountOfWorkers);
            Console.WriteLine("All done!");
        }
    }
}
