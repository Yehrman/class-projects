using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8thPowerV2
{
    class Program
    {
        //if you are asking a user for 5 numbers that requires a nested loop.To do a single loop you can only do 1 number I think.
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number");
            string num = Console.ReadLine();
            int numb = Convert.ToInt32(num);
            decimal product = 0m;
            product = numb;
            for (int i = 0; i <7; i++)
            {
                decimal temp = product * numb;
                product = temp;
           
            }
            Console.WriteLine(numb + "^8 = " + product);
            Console.ReadKey();
        }
    }
}
