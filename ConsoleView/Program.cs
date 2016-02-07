using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using core.Rabin;

namespace ConsoleView
{
    class Program
    {
        static void Print(int[] array)
        {
            foreach (int item in array)
            {
                if (item == 0)
                {
                    continue;
                }

                Console.Write(item);
                Console.Write(" ");
            }
            
        }

        static void Main(string[] args)
        {
            
            do
            {
                Rabin rabin = new Rabin();
                rabin.StartTests(100); 
                Console.WriteLine(rabin.PrimeNumber);
                Console.ReadKey();
                Console.Clear();
            
            }
            while (true);
           
        }
    }
}
