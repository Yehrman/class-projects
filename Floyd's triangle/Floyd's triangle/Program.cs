using System;
using System.Collections.Generic;
using System.Linq;


namespace Floyd_s_triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Y Horowitz explained it to me
           string a = "1";
            Console.WriteLine(a);
            for (int i = 1; i <3; i++)
            { 
                    a = "0"+a;
                    Console.WriteLine(a);
                    a="1"+a;
                    Console.WriteLine(a);
                }
                Console.ReadKey();
            //Did'nt relize what was needed for L5A23 Y Horowitz explained it to me and I did it here
            int c = 1;
            int d = 99;
           
            for (int i = 1; i<=99; i++)
            {
                Console.WriteLine(c + "---"+d);
                c++;
                d--;      
            }

            Console.ReadLine();
            }

         
        }
    }


