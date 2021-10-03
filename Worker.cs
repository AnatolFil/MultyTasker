using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    class Worker
    {
        //Amount of steps to do
        private int AmoutOfWork;
        //Current progress of task
        private int progress;
        public int Progress
        {
            get
            {
                return progress;
            }
        }
        private int managedThreadId;
        public int ManagedThreadId
        {
            get
            {
                return managedThreadId;
            }
        }
        public int Position { get; set; }
        //How many time worker can sleep
        public int Difficulty { get; set; }
        //How many time worker has been doing work, it ll be set when
        //worker finish work
        private double totalTime;
        public double TotalTime
        {
            get
            {
                return totalTime;
            }
        }
        //Show how fast work`s done work
        private int finishPosition;
        public int FinishPosition
        {
            get
            {
                return finishPosition;
            }
        }

        public Worker(int AmoutOfWork = 100, int Difficulty = 100)
        {
            this.AmoutOfWork = AmoutOfWork;
            this.Difficulty = Difficulty;
            progress = 0;
        }

        //Do some work. Increase Progress and sleep random time.
        //Worker can randomly rise exception and show it in progressbar with red colour
        //Worker counts time to know how many time it spent on work
        //After all work done it set totalTime and get his finish position in SuperVisor class
        public void Work()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Random RandTime = new Random(DateTime.Now.Millisecond);
            OutputHandler.WriteWorkerInfo(this);
            for (int i = 0; i < AmoutOfWork; i++)
            {
                try
                {
                    if (RandTime.Next(1, 100) > 95)//Randomly rise exception
                    {
                        throw new Exception("New Exception!");
                    }
                    Task.Delay(RandTime.Next(10, Difficulty)).Wait();//Delay on random time, it can be adjusted by param Difficulty
                    progress++;
                    OutputHandler.WriteWorkerProgress(this);//Write in console progress
                }
                catch(Exception e)
                {
                    progress++;
                    OutputHandler.WriteWorkerProgress(this, true);
                }
            }
            stopwatch.Stop();
            totalTime = stopwatch.Elapsed.TotalSeconds;
            int FinPosition = 0;
            SuperVisor.GetFinishPosition(ref FinPosition);
            finishPosition = FinPosition;
            OutputHandler.WriteFinPositionAndTime(this);//Write in console about worker finish position and spended time
        }
    }
}
