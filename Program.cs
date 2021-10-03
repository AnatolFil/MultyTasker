using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultyTasker
{
    //Program create 16 thread which simulate some work and show info in console
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SuperVisor.CreateAndStartWorkersAsync(14, 1000, 100);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(e.Message);
            }
            
            Console.ReadLine();
        }
    }
}
