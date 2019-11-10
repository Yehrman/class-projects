using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSquareRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is a program to get the square root o a number it'll only work if the square is'nt a decimal if it is it'll return 0
               string num = Console.ReadLine();
               int numb = Convert.ToInt32(num);
                 decimal sum = 0m;
               for (int i = 2; i < numb/2; i++)
               {
                   int a = i * i;
                   if (a == numb)
                   {
                       sum += i;
                       break;
                   }
               }
               Console.WriteLine("square root of "+numb+" is "+ sum);
            
        // Console.Write(Math.Sqrt(23729837));
            Console.ReadKey();
        }
    }
}
