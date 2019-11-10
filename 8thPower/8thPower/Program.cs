using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8thPower
{
    class Program
    {
        // 20. Ask the user for 5 numbers. For each number, print the number raised to the power of 8. 
        static void Main(string[] args)
        {   
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Enter a number");
                string num = Console.ReadLine();
                int numb = Convert.ToInt32(num);
               decimal product = numb;
                for (int j = 0; j < 7; j++)
                {
                //you want a to increase to the power of numb 
                   decimal temp= product*numb;
                    product = temp;
                }
                Console.WriteLine(numb+"^8 = "+product);
            }
            Console.ReadKey();
        }
    }
}
