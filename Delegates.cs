using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delagates
{
    public class Foo
    {
        public delegate void Noise(string n);
        public Noise noise;
        class Program
        {
            public static void American(string n)
            {
                Console.WriteLine("Americans say "+n);
            }
            public static  void Spanish(string n)
            {
                Console.WriteLine("Spanish say "+n);
            }
          
            static void Main(string[] args)
            {
                Foo P = new Foo();
                P.noise = Spanish;
                P.noise("senor");
                
                P.noise = American;
                P.noise("sir");
                Console.ReadKey();
            }
        }
    }
}