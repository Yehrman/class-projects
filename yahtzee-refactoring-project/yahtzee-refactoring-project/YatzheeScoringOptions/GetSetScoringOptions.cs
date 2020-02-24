using System;
using System.Linq;
using System.Collections.Generic;
using Yahtzee.YatzheeObjects;
namespace Yahtzee
{
    public class GetSetScoringOptions : YatzheeCollections,IGameScoringOptions
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

        int result;

        public int SelectScoringOption(Dictionary<string,int> options, List<string> moves)
        {
            Console.WriteLine("Type the scoring option of your choice");
            string prompt = Console.ReadLine();
            if (options.ContainsKey(prompt))
            {
                options.TryGetValue(prompt, out result);
                moves.Remove(prompt);
                return result;
            }

            else
            {
                Console.WriteLine("Please select a different option");
                return 0;
            }
        }
    }
}

