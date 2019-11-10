using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string fact = Console.ReadLine();
            int factor = Convert.ToInt32(fact);
            int sum = 1;
            for (int i = 1; i <=factor; i++)
            {
                sum += sum * i;
                
            }
            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}