using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibonacci
{//In a fibonacci you add 2 numbers and then the you add the larger number with the sum till the end of the fibonacci (which you specify)
    class Program
    {
        static void Main(string[] args)
        {
            int a =  0;
            int b = 1;
            int count = 0;
            while(b<4000)
            {
                int c = a + b;
                //smaller number becomes larger number
                a = b;
                //larger becomes sum and the procces continues
                b = c;
                if(b<4000)
                {
                    Console.WriteLine(b);
                    count++;
                }
            
                
            }
            Console.WriteLine(count);
            Console.ReadKey();
            }
           
           
        }
    }

