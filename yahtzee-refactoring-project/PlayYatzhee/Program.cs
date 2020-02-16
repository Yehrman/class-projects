using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee;

namespace PlayYatzhee
{
    class Program
    {
        private  int SelectedNumber { get; set; }
        private string SelectedDice { get; set; }
        public static string Player1 { get; set; }
        public static string Player2 { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        YatzheeExecution yatzhee = new YatzheeExecution();
        public void CheckScores()
        {
          //  YatzheeExecution yatzhee = new YatzheeExecution();
            YahtzeeScoreCalculator score = new YahtzeeScoreCalculator();      
             Console.WriteLine("The score of ones for this dice roll is "+score.Ones(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of twos for this dice roll is "+score.Twos(yatzhee.GetSelectedDice));
              Console.WriteLine("The score of threes for this dice roll is "+score.Threes(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of fours for this dice roll is " + score.Fours(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of fives for this dice roll is " + score.Fives(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of sixes for this dice roll is " + score.sixes(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of large straight for this dice roll is " + score.LargeStraight(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of small straight for this dice roll is " + score.SmallStraight(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of full house for this dice roll is " + score.FullHouse(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of chance for this dice roll is " + score.Chance(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of Yatzhee for this dice roll is " + score.Yahtzee(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of three of a kind for this dice roll is " + score.ThreeOfAKind(yatzhee.GetSelectedDice));
            Console.WriteLine("The score of is four of a kind for this dice roll is " + score.FourOfAKind(yatzhee.GetSelectedDice));
        }
        private  string Dice { get; set; }
        public  void StartTurn()
        {  
            Console.WriteLine("press any key to spin");
            Console.ReadKey();
            Console.WriteLine();
         
                yatzhee.ThrowDice();        
            yatzhee.PrintSpinresult();
            Console.WriteLine();
            for (int i = 0; i < yatzhee.GetDiceCount; i++)
            {
                Console.WriteLine("Press the number of the dice to select it otherwise press any key to spin again");
                string[] options = { "1", "2", "3", "4", "5", "6" };
                SelectedDice = Console.ReadLine();
                if (options.Any(x => x == SelectedDice))
                {
                    SelectedNumber = int.Parse(SelectedDice);
                    yatzhee.AddDice(SelectedNumber);
                }
                else if(options.Any(x=>x!=SelectedDice||yatzhee.GetDiceCount==0))
                {
                    break;                   
                }   
                
            }
           
            yatzhee.SetDiceCount();
            yatzhee.RemoveAllDice();
        }
     
        //   public  int GetSelectedNumber { get => SelectedNumber; }
     
       
         static Dictionary<string, int> Options()
          {
            YatzheeExecution yatzhee = new YatzheeExecution();
            YahtzeeScoreCalculator score = new YahtzeeScoreCalculator();
            return new Dictionary<string, int>()
              {
                {"ones",score.Ones(yatzhee.GetSelectedDice) },
                {"twos",score.Twos(yatzhee.GetSelectedDice) },
                {"threes",score.Threes(yatzhee.GetSelectedDice) },
                {"fours",score.Fours(yatzhee.GetSelectedDice) },
                {"fives",score.Fives(yatzhee.GetSelectedDice) },
                {"sixes",score.sixes(yatzhee.GetSelectedDice) },
                {"large straight",score.LargeStraight(yatzhee.GetSelectedDice) },
                {"small straght",score.SmallStraight(yatzhee.GetSelectedDice) },
                {"full house",score.FullHouse(yatzhee.GetSelectedDice) },
                {"three of a kind",score.ThreeOfAKind(yatzhee.GetSelectedDice) },
                {"four of a kind ",score.FourOfAKind(yatzhee.GetSelectedDice) },
                  {"chance",score.Chance(yatzhee.GetSelectedDice) },
                  {"yatzhee",score.Yahtzee(yatzhee.GetSelectedDice) }
              };
          }
        static void Main(string[] args)
        {
            Program p = new Program();

            Player1 = "Ned";
            Player2 = "Bill";
            while (true)
            {
                Console.WriteLine(Player1 + "'s turn ");

                //  string[] options = { "1", "2", "3", "4", "5", "6" };
                for (int i = 0; i <3; i++)
                {
                    p.StartTurn();                   
                }
                p.CheckScores();
                p.yatzhee.Set5Dice();
                
                
                Console.WriteLine(Player2 + "'s turn ");
                for (int i = 0; i < 3; i++)
                {
                    p.StartTurn();                 
                }
                p.CheckScores();
                p.yatzhee.Set5Dice();
             

               // var clas = new Class2(p.GetSelectedNumber);
//       Console.WriteLine("The score is " + clas.Score(yatzhee.GetSelectedDice));

            }



        }

    }
}
