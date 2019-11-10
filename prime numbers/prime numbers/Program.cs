using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prime_numbers
{
    class Program
    {
       
        static void Main(string[] args)
        {
      //   Taken from YY Kosbie  This needs biur bools need biur
            for (int i = 1; i < 200; i+=2)
            {
                bool flag = true;
                for(int j=2;j<=i/2;j++)
                {
                    flag = flag & (i %j != 0);
                }
                if(flag)
                {
                    Console.WriteLine(i + "is prime");
                }
            }
           
            Console.ReadLine();
        }
    }
}