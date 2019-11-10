using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorial
{
    class Program
    {
        //   12. Calculate the factorial of an inputted number.  Factorial means 1X2X3X4...up to that number.
        static void Main(string[] args)
        {
           
            int sum = 1;
            Console.WriteLine("Enter a number");
            string num = Console.ReadLine();
            int numb = Convert.ToInt32(num);
            for (int i = 2; i <=numb ; i++)
            {
                sum *= i;
            }
            Console.Write(sum);
            Console.ReadKey();
        }
    }
}
