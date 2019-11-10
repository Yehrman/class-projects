using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpPractice
{
    class Program
    {
        public class Animal
        {
            public string Name { get; set; }
            public virtual void MakeNoise()
            {
                Console.WriteLine("Booooooooooooooooo");
            }
            public double Size(double height,double width=3.33,bool big=true)
            { 
                double result = height * width;
                return result;
            }
           
        }
                public class Bear:Animal
            {
            
                public override void MakeNoise()
                {
                Console.WriteLine("Roooooooooooooooooo");
                }
            }
        
        static void Main(string[] args)
        {
            
            Animal animal = new Animal();
            animal.Name = "jjj";
            animal.MakeNoise();
            //animal.Size(4.23, completed:, false);
            animal.Size(width:3.44
            Animal a = new Bear();
            a.Name = "kkkk";
            a.MakeNoise();
            List<int> s = new List<int>();
            s.Add(3);
            s.Add(34);
            s.Add(32);
            s.Add(76);
            s.Add(67);
          e=>e.age>22
        }
    }
}
