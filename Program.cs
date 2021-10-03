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
        static void Main(string[] args)
        {
            SuperVisor.CreateAndStartWorkers(16, 1000);
            Console.ReadLine();
        }
    }
}
