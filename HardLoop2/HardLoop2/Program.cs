using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLoop2
{
    class Program
    {
        //L5A27 Y Horowitz helped with L5A28 and based on that I Baruch Hashem figured out this
        static void Main(string[] args)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    Console.Write(" 0 ");
                }
                Console.Write(" * ");
                for (int k = 7; k > i-1; k--)
                {
                    Console.Write(" 0 ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
