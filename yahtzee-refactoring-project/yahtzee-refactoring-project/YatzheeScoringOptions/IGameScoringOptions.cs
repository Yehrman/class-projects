using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.YatzheeObjects
{
   public interface IGameScoringOptions
    {
        Dictionary<string, int> GetScoringOptionsPlayer1 { get; }

        Dictionary<string, int> GetScoringOptionsPlayer2 { get; }
        void SetScoringOptions(Dictionary<string, int> options, List<string> moves);
        int SelectScoringOption(Dictionary<string, int> options, List<string> moves);
    }
}
