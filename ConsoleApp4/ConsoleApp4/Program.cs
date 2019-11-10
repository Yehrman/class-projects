using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    //Harmonic series improved
    class Program
    {
        static void Main(string[] args)
        {
         double result= 1;
            int a = 1;
            Console.Write(a+"+");
            for (double i = 2; i <= 9; i++)
            {
                //division of a int must result in at least 1 therefore we use double 
                Console.Write(a + "/" + i+"+");
                result += a / i;
            }
            Console.Write("=" + result);
            Console.ReadKey();
        }
        }
    }

