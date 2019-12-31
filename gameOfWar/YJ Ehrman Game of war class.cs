using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace ConsoleApp166
{
    enum Rankings
    {

        Jack = 10,
        Queen,
        King,
        Ace
    }

    internal class Cards
    {
      static  Random random = new Random();
         public static ArrayList array = new ArrayList() { 1, 2, 3, 4, 5, 6, 7, 8, 9, Rankings.Jack, Rankings.Queen, Rankings.King,Rankings.Ace};
       static  ArrayList GetCards()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < array.Count; j++)
                {
                    list.Add(array[j]);
                }  
            }
            return list;
        }

       static   ArrayList ShuffleCards()
        {
            ArrayList list = GetCards();
            ArrayList array = new ArrayList();
            while (list.Count != 0)
            {
                int a = random.Next(0, list.Count);
                array.Add(list[a]);
                list.RemoveAt(a);
            }
            return array;
        }
  static Queue Player1 { get; set; }
       static  Queue Player2 { get; set; }
      
      
        string Player1Name { get; set; }
         string Player2Name { get; set; }
        
           public void SetName(string p1, string player2name)
            {
            //  Console.WriteLine("Type in player name");
            Player1Name=p1 ;
                 Player2Name = player2name;
            }
        public void DivideCards(Queue queue1, Queue queue2)
        {
             Player1 = queue1;
             Player2 = queue2;
            ArrayList al = ShuffleCards();
            for (int i = 0; i < al.Count; i += 2)
            {
                queue1.Enqueue(al[i]);
            }
            for (int i = 1; i < al.Count; i += 2)
            {
                queue2.Enqueue(al[i]);
            }
        }
       public Queue GetPlayer1()
        {
            return Player1;
        }
        public Queue GetPlayer2()
        {
            return Player2;
        }
        public void Game()
            {
           
              Console.WriteLine("Press any key");
               Console.ReadKey();
              var Player1Pick=  Player1.Dequeue();
                Console.WriteLine(Player1Name + " picks "+ Player1Pick);
              Console.WriteLine("Press any key");
                Console.ReadKey();
                var Player2Pick = Player2.Dequeue();
                Console.WriteLine(Player2Name + " picks "+ Player2Pick);
                int a = (int)Player1Pick;
                int b = (int)Player2Pick;
                if (a > b)
                {

                    Player1.Enqueue(Player1Pick);
                    Player1.Enqueue(Player2Pick);
                    Console.WriteLine(Player1Name + " wins ");
                }
                else if (b > a)
                {
                    Player2.Enqueue(Player1Pick);
                    Player2.Enqueue(Player2Pick);
                    Console.WriteLine(Player2Name + " wins ");
                }
                else if(a==b)
                {
                War();
                }
        }
         void War ()
        {
            var Player1Pick = Player1.Dequeue();
            var Player2Pick = Player2.Dequeue();
            int a = (int)(Player1Pick);
            int b = (int)(Player2Pick);
            if(a==b)
            {
                Console.WriteLine("war");
                var p1Card1 = Player1.Dequeue();
                var p1Card2 = Player1.Dequeue();
                var p1TopCard = Player1.Dequeue();
                var p2Card1 = Player2.Dequeue();
                var p2Card2 = Player2.Dequeue();
                var P2TopCard = Player2.Dequeue();
                int g = (int)(p1TopCard);
                int h = (int)(P2TopCard);
                if (g > h)
                {
                    Player1.Enqueue(p1Card1);
                    Player1.Enqueue(p1Card2);
                    Player1.Enqueue(p1TopCard);
                    Player1.Enqueue(p2Card1);
                    Player1.Enqueue(p2Card2);
                    Player1.Enqueue(P2TopCard);
                    Console.WriteLine(Player1Name + " wins");
                }
                else if (h > g)
                {
                    Player2.Enqueue(p1Card1);
                    Player2.Enqueue(p1Card2);
                    Player2.Enqueue(p1TopCard);
                    Player2.Enqueue(p2Card1);
                    Player2.Enqueue(p2Card2);
                    Player2.Enqueue(P2TopCard);
                    Console.WriteLine(Player2Name + " wins");
                }
            }
        }
        }
       
    }
   
