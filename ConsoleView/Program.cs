using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using core.Kardano;

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
            Kardano kardano = new Kardano(2);

            string text = Console.ReadLine();
            int[] encrypted = kardano.Encrypt(text);
            Print(encrypted);

            Console.WriteLine();
            Console.WriteLine(kardano.Decrypt(encrypted));

            Console.ReadKey();

        }
    }
}
