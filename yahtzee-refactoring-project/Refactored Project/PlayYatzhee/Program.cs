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
        private int SelectedNumber { get; set; }
        private string SelectedDie { get; set; }

        public static string Player1 { get; set; }
        public static string Player2 { get; set; }

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        GetSetYatzheeDiceLists GetSet = new GetSetYatzheeDiceLists();
        ShowYatzheeCollections Show = new ShowYatzheeCollections();
        GetSetScoringOptions PossibleScores = new GetSetScoringOptions();
        
      

       
        public void StartTurn()
        {
            if (GetSet.GetDiceCount > 0)
            {
              
                Console.WriteLine("press any key to spin");
               Console.ReadKey();
                Console.WriteLine();

                GetSet.ThrowDice();
                Show.PrintSpinresult();
                Console.WriteLine();
                var options = GetSet.GetSpinResult;
                List<string> scoringOptions = options.ConvertAll(x => x.ToString());
                for (int i = 0; i < GetSet.GetDiceCount; i++)
                {
                     Console.WriteLine("Press the number of the dice to select it otherwise press any key to spin again");

                       SelectedDie= Console.ReadLine();
                    
                 //   SelectedNumber = random.Next(GetSet.GetSpinResult.Count);
                   // GetSet.AddToSelectedDice_RemoveFromSpinResult(SelectedNumber);
                    if (scoringOptions.Any(x => x == SelectedDie) && GetSet.GetSelectedDice.Count() < 5)
                    {
                        SelectedNumber = int.Parse(SelectedDie);
                        GetSet.AddToSelectedDice_RemoveFromSpinResult(SelectedNumber);
                    }
                   else   if (options.Any(x => x != SelectedNumber) || GetSet.GetSelectedDice.Count() == 5)
                    {
                        break;
                    }
                }
            }
            GetSet.SetDiceCount();
            GetSet.RemoveAllDiceFromDiceList(GetSet.GetSpinResult);
            Console.WriteLine();
        }
        public void ShowSelectedDiceAndScoringOptions(Dictionary<string,int> options,List<string>moves)
        {
            Show.PrintSelectedDice();       
            PossibleScores.SetScoringOptions(options, moves);
            Show.ShowScoringOptions(options);
        }
        public void FinishTurn(List<int> dice,Dictionary<string,int> options)
        {
            GetSet.ResetDiceCount();
            GetSet.RemoveAllDiceFromDiceList(dice);
            PossibleScores.RemoveScoringOptions(options);
        }
        static void Main(string[] args)
        {
            int score;
            YatzheeAI aI = new YatzheeAI();
            Program p = new Program();
            Console.WriteLine("Player 1 type your name");
            Player1 = Console.ReadLine();
            Console.WriteLine("Player 2 type your name");
            Player2 = Console.ReadLine();
            p.PossibleScores.SetMoves(p.PossibleScores.GetPlayer1Moves);
            p.PossibleScores.SetMoves(p.PossibleScores.GetPlayer2Moves); 

            while (p.PossibleScores.GetPlayer2Moves.Count() > 0)
            {
                Console.WriteLine(Player1 + "'s turn ");
                p.Show.ShowPossibleMoves(p.PossibleScores.GetPlayer1Moves);
                for (int i = 0; i < 3; i++)
                {
                       p.StartTurn();
                   // aI.StartTurn();
                }

                p.ShowSelectedDiceAndScoringOptions(p.PossibleScores.GetScoringOptionsPlayer1, p.PossibleScores.GetPlayer1Moves);

                score = p.PossibleScores.SelectScoringOption(p.PossibleScores.GetScoringOptionsPlayer1, p.PossibleScores.GetPlayer1Moves);
                p.Player1Score += score;
                Console.WriteLine(Player1+" scored "+score+" on this turn");
                Console.WriteLine(Player1 + "'s overall score is " + p.Player1Score);
                score = 0;

                p.FinishTurn(p.GetSet.GetSelectedDice, p.PossibleScores.GetScoringOptionsPlayer1);
           
                Console.WriteLine(Player2 + "'s turn ");
                p.Show.ShowPossibleMoves(p.PossibleScores.GetPlayer2Moves);
                for (int i = 0; i < 3; i++)
                {
                    p.StartTurn();

                }
                p.ShowSelectedDiceAndScoringOptions(p.PossibleScores.GetScoringOptionsPlayer2, p.PossibleScores.GetPlayer2Moves);

                score = p.PossibleScores.SelectScoringOption(p.PossibleScores.GetScoringOptionsPlayer2, p.PossibleScores.GetPlayer2Moves);
                p.Player2Score += score;
                Console.WriteLine(Player2 + " scored " + score + " on this turn");
                Console.WriteLine(Player2 + "'s score is " + p.Player2Score);           
                score = 0;

                p.FinishTurn(p.GetSet.GetSelectedDice, p.PossibleScores.GetScoringOptionsPlayer2);
            }

            if (p.Player1Score > p.Player2Score)
                {
                    Console.WriteLine(Player1 + " wins by a score of " + p.Player1Score + " to " + p.Player2Score);
                }
                else if (p.Player1Score == p.Player2Score)
                {
                    Console.WriteLine("TIE!!!!");
                }
                else
                {
                    Console.WriteLine(Player2 + " wins by a score of " + p.Player2Score + " to " + p.Player1Score);
                }
            Console.ReadKey();
            }
        }
    }