using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpExersizes
{
    class Program
    {
        static void Main(string[] args)
        {
            //7. Write a for loop that gets the product of the first 11 integers (and prints it out to the console).
            int product = 1;
            for (int i = 1; i < 12; i++)
            {
                product *= i;
               
            }
            Console.WriteLine(product);
            Console.ReadKey();
      }
   }
}

        