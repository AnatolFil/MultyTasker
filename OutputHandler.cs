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
        public static Mutex mtx = new Mutex();
        
        public static void Write(Worker worker)
        {
            Console.CursorVisible = false;
            // Получить мьютекс
            mtx.WaitOne();
            Console.SetCursorPosition(0, worker.Position);
            Console.WriteLine(worker.Position + " (" + worker.ManagedThreadId + ") " + worker.Progress);
            mtx.ReleaseMutex();
        }

    }
}
