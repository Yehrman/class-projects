using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonicSeriesExe
{
    class Program
    {
        /* The harmonic series is a series of numbers that looks like this:
    1 + 1/2 + 1/3 + 1/4 + 1/5 ... 1/n terms
    Write a program to print the first 9 terms (up to 1/9) of the harmonic series. 
    Then have your program print the sum of all of those terms.*/
        static void Main(string[] args)
        {
            decimal sum = 1;
            int x = 1;
            Console.Write(x+"+");
            //Remember dividing a integer in c# will alaways end up in a 0.You must do implicit cast to decimal look at stack overflow division returns o
            for (decimal i = 2m; i <= 9m; i++)
            {
                // For some reason you can't use '' only "" here. 
                Console.Write(x + "/" + i+"+");
                sum += x / i;
            }
            Console.WriteLine("="+sum);
                Console.ReadKey();
            }
        }
}
