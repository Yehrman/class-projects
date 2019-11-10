using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifficultLoop
{
    class Program
    {
        static void Main(string[] args)
        {
          
  Console.WriteLine("Display the pattern like diamond");
            int i, j, k;
            for (i = 0; i <= 5; i++)
            {
                for (j = 1; j <= 5 - i; j++)
                {
                    Console.Write(" ");
                }
                for (k = 1; k <= 2 * i - 1; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            for ( i = 4; i >=1; i--)
            {
                for (j = 4; j > i - 1; j--)
                {
                    Console.Write(" ");
                }
                for (k = 1; k <= 2 * i - 1; k++)
                {
                    Console.Write("*");
                }
               
                Console.WriteLine();

            }







            Console.ReadKey();
        }
          
        }
}
