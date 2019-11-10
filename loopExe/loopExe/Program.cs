using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loopExe
{
    class Program
    {
        // L5A23 Not sure what is wanted 
        static void Main(string[] args)
        {
            for (int i = 1; i <=8 ; i++)
            {
                if (i == 1)
                    Console.WriteLine("1----------99");
                if (i == 2)
                    Console.WriteLine("2------------98");
                if (i == 3)
                    Console.WriteLine("3--------------97");
                if (i == 4)
                    Console.WriteLine(".            .");
                if (i == 5)
                    Console.WriteLine(".             .");
                if (i == 6)
                    Console.WriteLine(".              .");
                if (i == 7)
                    Console.WriteLine("98-------------2");
                if (i == 8)
                    Console.WriteLine("99--------------1");
            }
           
            Console.ReadKey();
        }
    }
}
