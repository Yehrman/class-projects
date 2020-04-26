using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{




    class Animals
    {
        public string Name;
        public virtual string MakeNoise()
        {

            string a = "Animal noise";
            return a;



        }
        class Cows : Animals
        {
            public override string MakeNoise()
            {
                string a = "moooooo";
                return a;

            }
        }
        class Lions : Animals
        {
            public override string MakeNoise()
            {
                string a = "roar";
                return a;
            }

        }



        static void Main(string[] args)
        {
            Animals animal = new Animals();
            animal.Name = "Jim";
            Animals cows = new Cows();
            cows.Name = "Betsy";

            Animals lions = new Lions();
            lions.Name = "Jonga";

            List<Animals> animals = new List<Animals>() { animal, cows, lions };
            foreach (Animals item in animals)
            {
                Console.WriteLine(item.Name + " makes " + item.MakeNoise());
            }



            Console.ReadKey();
        }
    }

        }
    
