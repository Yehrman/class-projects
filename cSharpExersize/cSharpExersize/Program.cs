using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpPrimeNumberExersize
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write a program to check if a numbers inputted by the user is prime or not. Program should ask the user for 5 numbers.
            bool prime = true;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Enter a number");
                string num = Console.ReadLine();
                int numb = Convert.ToInt32(num);
                for (int j = 2; j <= numb / 2; j++)
                {
                    if (numb % j != 0)
                    {
                        prime=true;
                    }
                    else
                    {
                        prime = false;
                        break;
                    }
                }
                //When checking a bool do if(prime)
                if(prime)
                {
                    Console.WriteLine("prime");
                }
                else
                {
                    Console.WriteLine("Not prime");
                }
            }
                Console.ReadKey();
            }
        }
    }


