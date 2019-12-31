using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp166 //Y Ehrman Game of war code without object oriented design
{
    class Program
    {

        static void Main(string[] args)
        {
          
         
            Cards cards = new Cards();
            Queue queue = new Queue();
            Queue q = new Queue();
            cards.SetName("Shimon", "Chaim");
            cards.DivideCards( queue,q);
        while(queue.Count!=0||q.Count!=0)
            {
                cards.Game();
                if(q.Count==0)
                {
                    Console.WriteLine("Game over Shimon wins. Press any key to continue");
                    Console.ReadKey();
                }
                else if(queue.Count==0)
                {
                    Console.WriteLine("Game over Chaim wins. Press any key to continue");
                    Console.ReadKey();
                }
            }
               
          





            // game.War();


        }
     
      
    }
}