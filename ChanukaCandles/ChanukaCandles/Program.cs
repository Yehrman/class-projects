using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanukaCandles
{
    class Program
    {
        static void Main(string[] args)
        {
            int min = 2;
           // int day = 1;
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
               // Console.WriteLine("On day "+day+" we use "+min+" candles");
                //day++;
                sum += min;
                min++;
               

            }
            Console.WriteLine("The total number of candles is " + sum);
            Console.ReadKey();
        }
    }
}
