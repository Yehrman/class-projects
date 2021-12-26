using System;
using System.Collections.Generic;
using System.Linq;
using Yahtzee;
using Humanizer;
namespace PlayYatzhee
{
    public class YatzheeAI
    {
        private int SelectedNumber { get; set; }
        private string SelectedDie { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        Random random = new Random();
        GetSetYatzheeDiceLists GetSet = new GetSetYatzheeDiceLists();
        ShowYatzheeCollections Show = new ShowYatzheeCollections();
        GetSetScoringOptions PossibleScores = new GetSetScoringOptions();
        //So far this just throws the dice and randomly decides how many
        //dice to select between 0 and 5. Could improve on this
        //say if there are more then 1 of a number select it
            List<string> moves = new List<string>();
        public List<int> ConsecutiveChecker(List<int> ints)
        {
            List<int> pairs = new List<int>();
            for (int i = 0; i < ints.Count; i++)
            {
                if(ints[i+1]==ints[i]+1)
                {
                    pairs.Add(ints[i]);
                }
                else
                {
                    break;
                }
            }
            return pairs;
        }
        public Dictionary <int,int> IdenticalChecker(List<int> list)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int check = 0;
            int number=0;
            for (int i = 0; i <list.Count; i++)
            {
                if(list[i]==list[i+1])
                {
                    check++;
                    number = list[i];
                    
                }
                dict.Add(check, number);
            }
            return dict;
        }
        public void StartTurn(string player)
        {
         if (player=="player1")
            {
                moves = PossibleScores.GetPlayer1Moves;
            }
            else
            {
                moves = PossibleScores.GetPlayer2Moves;
            }
            if (GetSet.GetDiceCount > 0)
            {
                GetSet.ThrowDice();
                Show.PrintSpinresult();
                var options = GetSet.GetSpinResult;
                while (  options.Count>0)
                {
                    options.Sort();

                    List<int> checker = ConsecutiveChecker(options);
                    if(checker.Count>=3 &&moves.Any(x=>x== "large straight"||x== "small straight"||x=="full house"))
                    {
                        foreach (var item in checker)
                        {
                            GetSet.AddToSelectedDice_RemoveFromSpinResult(item);
                            
                        }
                    }
                    var number = IdenticalChecker(options);
                    var val = number.First();
                    //Needs work from here
                    if(val.Value>2)
                    {
                        int count = val.Key;
                        //int count = options.Where(x => x == number).Count();
                        string move = count.ToWords();
                        if (moves.Any(x => x.Contains(move)||x.Contains("yatzhee")||x.Contains("Three of a kind")||x.Contains("large straight")))
                        {
                          
                            GetSet.AddToSelectedDice_RemoveFromSpinResult(numbers);
                        }

                    if(count==3)
                    {
                        var numbers = options.Where(x => x != options[i]).ToList();
                       if(numbers[0]==numbers[1] && moves.Any(x=>x=="full house"))
                            {
                                GetSet.AddToSelectedDice_RemoveFromSpinResult(options); 
                            }
                    }
                    }
                    
                    //for (int j = 0; j < random.Next(0, 6); j++)
                    //{
                    //    SelectedNumber = random.Next(GetSet.GetSpinResult.Count);

                    //    if (options.Any(x => x == SelectedNumber) && GetSet.GetSelectedDice.Count() < 5)
                    //    {
                    //        GetSet.AddToSelectedDice_RemoveFromSpinResult(SelectedNumber);
                    //    }
                    //    else if (options.Any(x => x != SelectedNumber) || GetSet.GetSelectedDice.Count() == 5)
                    //    {
                    //        break;
                    //    }
                    //}
                }
            }
        }
        public void ShowSelectedDiceAndScoringOptions(Dictionary<string, int> options, List<string> moves)
        {
            Show.PrintSelectedDice();
            PossibleScores.SetScoringOptions(options, moves);
            Show.ShowScoringOptions(options);
        }
    }
}