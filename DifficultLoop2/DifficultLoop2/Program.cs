using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifficultLoop2
{
    class Program
    {
        static void Main(string[] args)
        {
            //L25A6
            //Y Horowitz explained it to me
            for (int i = 1; i < 4; i++)
            {
                for (int j = 4; j > i; j--)
                {
                    Console.Write(" ");
                }
                for (int k = 1; k <= i; k++)
                {
                    Console.Write(k);
                }
                //I could'nt figure out how to reverse the the numbers  12321. How does 123 turn into 21 From this program it's clear that you
                //don't reverse the numbers.You simply have another loop printing from where it reverses.
                
                for (int l = i - 1; l >= 1; l--)
                {
                    Console.Write(l);
                }
                Console.WriteLine();
            }
            Console.ReadKey();

        }
    }
}
