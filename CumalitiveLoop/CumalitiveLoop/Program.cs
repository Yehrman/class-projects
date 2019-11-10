using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumalitiveLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            //L5A18
            //Find the sum of the following series: 1 + 11 + 111 + 1111 + 11111 ...  e
            int temp= 1;
            int sum = 0;
       
                for (int i= 1; i < 6; i++)
                {
               
                temp *= 10;     
                temp++;
                sum += temp;
                }
                //Not sure how to put the starting 1 in the loop so just added it to the total 
            Console.WriteLine(sum+1);
         
            Console.ReadKey();
        }
    }
}
