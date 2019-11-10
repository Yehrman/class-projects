using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareOfNumber
{
    class Program
    {
        //19. Write a while loop to get the square of each number (meaning that number * itself * itself) until the result is >120.
        static void Main(string[] args)
        {
            int i = 1;         
            int j = 1;     
            while (i<120)
            {
                int product = i * j;
                if (product < 120)
                {
                    Console.WriteLine("The square of " + i + " and " + j + "= " + product);
                    j++;
                    i++;
                }
            }
            Console.ReadKey();
        }
    }
}
