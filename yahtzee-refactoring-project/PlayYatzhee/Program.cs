using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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

        YatzheeCollections yatzhee = new YatzheeCollections();
        GetSetYatzheeObjects GetSet = new GetSetYatzheeObjects();
        ShowYatzheeCollections Show = new ShowYatzheeCollections();
        GetSetScoringOptions PossibleScores = new GetSetScoringOptions();

        int score;
        int score2;

        private  string Dice { get; set; }
  
        public  void StartTurn()
        {        
            if (GetSet.GetDiceCount > 0)
            {
                Console.WriteLine("press any key to spin");
                Console.ReadKey();
                Console.WriteLine();

                GetSet.ThrowDice();
                Show.PrintSpinresult();
                Console.WriteLine();

                for (int i = 0; i < GetSet.GetDiceCount; i++)
                {
                    Console.WriteLine("Press the number of the dice to select it otherwise press any key to spin again");
                    string[] options = { "1", "2", "3", "4", "5", "6" };
                    SelectedDice = Console.ReadLine();
                    if (options.Any(x => x == SelectedDice))
                    {
                        SelectedNumber = int.Parse(SelectedDice);
                        GetSet.AddDice(SelectedNumber);
                    }
                    else if (options.Any(x => x != SelectedDice) || SelectedDice.Count() == 5)
                    {
                        break;
                    }
                }
            }
            GetSet.SetDiceCount();
            GetSet.RemoveAllDiceFromSpinResult();
        }
    
     
        //   public  int GetSelectedNumber { get => SelectedNumber; }
     
       
 
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Player 1 type your name");       
            Player1 = Console.ReadLine();
            Console.WriteLine("Player 2 type your name");
            Player2 = Console.ReadLine();
            p.PossibleScores.SetMoves(p.PossibleScores.GetPlayer1Moves);
            p.PossibleScores.SetMoves(p.PossibleScores.GetPlayer2Moves);
      
            while (p.PossibleScores.GetPlayer2Moves.Count()>0)
            {
                Console.WriteLine(Player1 + "'s turn ");

                //  string[] options = { "1", "2", "3", "4", "5", "6" };
                for (int i = 0; i <3; i++)
                {
                    p.StartTurn();
                 
                }
               p.Show.PrintSelectedDice();
                p.PossibleScores.SetScoringOptions(p.PossibleScores.GetScoringOptionsPlayer1,p.PossibleScores.GetPlayer1Moves);           
                p.Show.ShowScoringOptions(p.PossibleScores.GetScoringOptionsPlayer1);
                p.score = p.PossibleScores.SelectScoringOption(p.PossibleScores.GetScoringOptionsPlayer1,p.PossibleScores.GetPlayer1Moves);
                p.Player1Score += p.score;
                Console.WriteLine(Player1 + "'s score is " + p.Player1Score);
                p.GetSet.ResetDiceCount();
                p.GetSet.RemoveAllDiceFromSelectedDice();
                p.score = 0;
                p.PossibleScores.RemoveScoringOptions(p.PossibleScores.GetScoringOptionsPlayer1);

                Console.WriteLine(Player2 + "'s turn ");
                for (int i = 0; i < 3; i++)
                {
                    p.StartTurn();
                 
                }
               p.Show.PrintSelectedDice();
               p.PossibleScores.SetScoringOptions(p.PossibleScores.GetScoringOptionsPlayer2,p.PossibleScores.GetPlayer2Moves);
                p.Show.ShowScoringOptions(p.PossibleScores.GetScoringOptionsPlayer2);
                p.score2 = p.PossibleScores.SelectScoringOption(p.PossibleScores.GetScoringOptionsPlayer2,p.PossibleScores.GetPlayer2Moves);
                p.Player2Score += p.score2;
                Console.WriteLine(Player2 + "'s score is " + p.Player2Score);
                p.GetSet.ResetDiceCount();
                p.GetSet.RemoveAllDiceFromSelectedDice();
                p.score2 = 0;
                p.PossibleScores.RemoveScoringOptions(p.PossibleScores.GetScoringOptionsPlayer2);
            }
            if(p.score>p.score2)
            {
                Console.WriteLine(Player1 + " wins by a score of " + p.score + " to " + p.score2);
            }
            else if(p.score==p.score2)
            {
                Console.WriteLine("TIE!!!!");
            }
            else
            {
                Console.WriteLine(Player2 + " wins by a score of " + p.score2 + " to " + p.score);
            }
        }
    }
}
