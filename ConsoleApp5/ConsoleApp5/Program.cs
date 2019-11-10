using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            int numb = Convert.ToInt32(Console.ReadLine());
            int product = numb;
           
            for (int i = 0; i < 7; i++)
            {
               product*= numb;
               
               
            }
            Console.WriteLine(product);
            Console.ReadKey();
        }
    }
}
