using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number");
            string num = Console.ReadLine();
            int number = Convert.ToInt32(num);
            Console.WriteLine("Enter a exponent");
            string exp = Console.ReadLine();
            int exponent = Convert.ToInt32(exp);
            int product = number;
            for(int i=1;i<exponent;i++)
            {
                product *=number;
            }
            Console.WriteLine("The product is "+product);
            Console.ReadKey();
        }
    }
}
