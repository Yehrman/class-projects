using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5A12 //Y Ehrman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Put in a number");
            string num = Console.ReadLine();
            int numb = Convert.ToInt32(num);
            int i = numb - 1;
            int ans = numb * i;
            int sum = 0;
            while (i > 0)
            {
                i--;
                int answ = ans * i;
                sum += answ;
               
            }
            Console.WriteLine("The factorial is " + sum);
            Console.ReadKey();
        }
    }
}
