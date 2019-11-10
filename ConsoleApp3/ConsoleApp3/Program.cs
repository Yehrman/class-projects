using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int temp = 1;
            int sum = 1;
            for (int i = 0; i < 4; i++)
            {
                temp *= 10;
                temp++;
                //You need sum to add all the multiplied numbers together
                sum += temp;
            }
            Console.Write(sum);
            Console.ReadKey();
        }

        
            
          
          
        }
    }

