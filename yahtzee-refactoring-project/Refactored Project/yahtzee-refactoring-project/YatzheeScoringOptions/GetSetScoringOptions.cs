using System;
using System.Linq;
using System.Collections.Generic;
using Yahtzee.YatzheeObjects;
namespace Yahtzee
{
    public class GetSetScoringOptions : YatzheeCollections, IGameScoringOptions
    {
        private List<string> Player1Moves { get; set; } = new List<string>();
        private List<string> Player2Moves { get; set; } = new List<string>();

        public void SetMoves(List<string> moves)
        {
            moves.AddRange(ScoringOptions().Keys);        
        }
        public List<string> GetPlayer1Moves { get => Player1Moves; }
        public List<string> GetPlayer2Moves { get => Player2Moves; }


        public void SetScoringOptions(Dictionary<string, int> options, List<string> moves)
        {

            foreach (var item in ScoringOptions())
            {
                if (moves.Any(x => x == item.Key))
                {
                    options.Add(item.Key, item.Value);
                }
            }
        }
        public void RemoveScoringOptions(Dictionary<string, int> options)
        {
            foreach (var item in ScoringOptions())
            {
                options.Remove(item.Key);
            }
        }
        public Dictionary<string, int> GetScoringOptionsPlayer1 { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> GetScoringOptionsPlayer2 { get; set; } = new Dictionary<string, int>();

     
        private List<string> SetSubStringMovesList(List<string> moves)
        {
            List<string> TempMoves = new List<string>();
            foreach (var item in moves)
            {
                var substring = item.Substring(1, 2);
                TempMoves.Add(substring);
            }
            return TempMoves;
        }
        int result;

        public int SelectScoringOption(Dictionary<string, int> options, List<string> moves)
        {
            var TempMoves = SetSubStringMovesList(moves);
            Console.WriteLine("Type the  letters  in brackets of the scoring option of your choice. [Make sure to type the correct case (upper or lower)]");
           
            for (int i = 0; i < 3; i++)
            {
                string prompt = Console.ReadLine();
                if (TempMoves.Any(x => x == prompt))
                  {
                    var Move = moves.FirstOrDefault(x => x.Substring(1, 2) == prompt);
                    options.TryGetValue(Move, out result);
                    moves.Remove(Move);
                    return result;
                }


                else 
                {
                    Console.WriteLine("You picked a invalid option.Please select a valid option");
                }
            }
            Console.WriteLine("You picked a invalid option.Please try again next turn");
            return 0;
        }
        
    }
}

