using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        public static int Factorial(int num)
        {
                if (num < 1)
                {
                return 1;
                }
                else
                {
                    int sum = num;
                    sum *= Factorial(num - 1);
                    return sum;
                }
        }


      static  string reversedString = string.Empty;
        static string  Reverse(string word)
        {
         
            if (word.Length<1)
            {
                return word;
            }
            else
            {

                reversedString += word.Last();
                word=  word.Remove(word.Length-1);
                Reverse(word);
                return reversedString;

            }
           
        }
       
        static void Main(string[] args)
        {

            // Console.WriteLine(Factorial(10));
            Console.Write(Reverse("football"));
                Console.ReadLine();
            
        }
    }
}
