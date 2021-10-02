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
        private int position;
        public int Position
        {
            get
            {
                return position;
            }
        }
        public int Difficulty { get; set; }

        //Lock default constructor and leave only custom constructor
        // to be sure in right initialization
        private Worker()
        {

        }
        public Worker(int AmoutOfWork = 100, int Difficulty = 100)
        {
            this.AmoutOfWork = AmoutOfWork;
            this.Difficulty = Difficulty;
            progress = 0;
        }



        //Do some work. Increase Progress and sleep random time.
        private double Work(int Position)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            managedThreadId = Thread.CurrentThread.ManagedThreadId;
            Random RandTime = new Random(DateTime.Now.Millisecond);
            this.position = Position;
            OutputHandler.Write(this);
            for (int i=0;i<AmoutOfWork; i++)
            {
                Task.Delay(RandTime.Next(10, Difficulty)).Wait();
                //Thread.Sleep(RandTime.Next(10, Difficulty));
                progress++;
                OutputHandler.Write(this);  
            }
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalSeconds;
        }

        public async void DoWorkAsync(int Position)
        {            
            await Task.Run(() => Work(Position));
        }
    }
}
