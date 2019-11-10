using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopingExe
{
    class Program
    {   
        //Write a single loop to display the following output (will use an if statement.)(HINT: Lines 0,3,6 are the same and 1,2,4,5 and the same
        static void Main(string[] args)
        {
            for (int i = 0; i < 7; i++)
            {
                if (i == 0 || i == 3 || i == 6)
                    Console.WriteLine("****");
                else
                    Console.WriteLine("*   *");
            }
            Console.ReadKey();
        }
    }
}
