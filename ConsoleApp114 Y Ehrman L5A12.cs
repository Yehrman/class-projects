using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5A12 //Y Ehrman
{
       //12. Calculate the factorial of an inputted number.  Factorial means 1X2X3X4...up to that number.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Put in a number");
             string num = Console.ReadLine();
               int numb = Convert.ToInt32(num);
          int sum = 0;
              int j = 1;
            while (j<numb)
              {
                int ans = j * (j + 1);
                j++;
                int answ = ans * j;
                sum += answ;
            }
              Console.WriteLine("The factorial is " + sum);
            Console.ReadKey();

        }
 
    }        
}
