using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            // int a = 1;
            /*  for (int i = 1; i < 4; i++)
              {
                  for (int j = 4; j <i; j--)
                  {
                      Console.Write(" ");
                  }
                  for (int k = 1; k <= i; k++)
                  {
                      Console.Write(k);
                  }
                  for (int l = i-1; l >=1 ; l--)
                  {
                      Console.Write(l);

                  }
                  Console.WriteLine();
              }*/
            for (int i = 1; i < 4; i++)
            {
                for (int j = 4; j > i; j--)
                {
                    Console.Write(" ");
                }
                for (int k = 1; k <= i; k++)
                {
                    Console.Write(k);

                }
                for (int l = i - 1; l >= 1; l--)
                {
                    Console.Write(l);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
