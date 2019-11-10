using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeryhardLoop
{
    class Program
    {
        //LA528
        static void Main(string[] args)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    Console.Write("0");
                }
                Console.Write("**");
                if(i>1&&i<8)
                {
                    Console.Write("*");
                }
                for (int k = 8; k >i+1; k--)
                {
                    Console.Write("0");
                }
                Console.WriteLine();

            }
            Console.ReadKey();
        }
    }
}
