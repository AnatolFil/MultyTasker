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
        //How many time worker can sleep
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
        public int Difficulty { get; set; }
        private double totalTime;
        public double TotalTime
        {
            get
            {
                return totalTime;
            }
        }
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
        public void Work()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Random RandTime = new Random(DateTime.Now.Millisecond);
            OutputHandler.WriteProgressInfo(this);
            for (int i=0;i<AmoutOfWork; i++)
            {
                Task.Delay(RandTime.Next(10, Difficulty)).Wait();
                progress++;
                OutputHandler.WriteProgressInfo(this);  
            }
            stopwatch.Stop();
            totalTime = stopwatch.Elapsed.TotalSeconds;
            int FinPosition = 0;
            SuperVisor.GetFinishPosition(ref FinPosition);
            finishPosition = FinPosition;
            OutputHandler.WriteFinPositionAndTime(this);
        }
    }
}
