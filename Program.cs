using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    class Program
    {
        static void fun()
        {
            Thread.Sleep(3000);
            Console.WriteLine("I m sync, but i m doing it like async " + Thread.CurrentThread.ManagedThreadId.ToString());
            Thread.Sleep(30000);
        }

        static async void funAsync()
        {
            Thread.Sleep(1000);
            Console.WriteLine("I ll run async fun");
            await Task.Run(() => fun());
            Console.WriteLine("I ve run async fun");
        }

        static void Main(string[] args)
        {
            for(int i=0;i<50;i++)
            {
                Worker Worker = new Worker(Difficulty: 1000);
                Worker.DoWorkAsync(i);
                
            }
            
            Console.ReadLine();
        }
    }
}
