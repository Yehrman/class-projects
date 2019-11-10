using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumAlgExersize
{
    class Program
    {
        static void Main(string[] args)
        {
            //9. Write a program that gets 10 numbers from the user and prints out their sum and average (hint: you should be using a loop).
            Console.WriteLine("Please type 10 numbers");
            int sum = 0;
           
            for (int i = 0; i < 10; i++)
            {
                string num = Console.ReadLine();
                int numb = Convert.ToInt32(num);
                Console.WriteLine("that is 1");
                sum += numb;
           
            }
            int avg = sum / 10;
            Console.WriteLine("The sum is "+sum+" the average is "+avg);
            Console.ReadKey();
        }
    }
}
