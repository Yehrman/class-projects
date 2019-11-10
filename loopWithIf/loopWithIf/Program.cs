using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loopWithIf
{
    class Program
    {
        static void Main(string[] args)
        {
            //L5A16
            int i = 0;
            int j = 0;
            int k = j + 2;
            Console.WriteLine("****");
            while (i < 6)
            {
                if (i == k)
                {
                    Console.WriteLine("****");
                    k += 3;
                }
                else
                {
                    Console.WriteLine("*   *");
                }
                i++;
            }
            Console.ReadKey();
        }
    }
}
