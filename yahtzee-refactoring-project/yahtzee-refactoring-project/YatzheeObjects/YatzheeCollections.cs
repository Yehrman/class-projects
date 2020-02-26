using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.YatzheeObjects;

namespace Yahtzee
{
    //I made this class be inherited by the classes that will set/get them simply for orginization to not  overcrowd the set/get
    //classes
    public class YatzheeCollections 
    {
        protected static List<int> SpinResult { get; set; } = new List<int>();

        protected static List<int> SelectedDice { get; set; } = new List<int>();

        protected static Dictionary<string, int> ScoringOptions()
        {
            GetSetYatzheeDiceLists getSet = new GetSetYatzheeDiceLists();

            YahtzeeScoreCalculator score = new YahtzeeScoreCalculator();

            return new Dictionary<string, int>()
            {
                {    "(on)es", score.Ones(getSet.GetSelectedDice) },
                { "(tw)os", score.Twos(getSet.GetSelectedDice) },
                { "(th)rees", score.Threes(getSet.GetSelectedDice)},
                { "(fo)urs", score.Fours(getSet.GetSelectedDice) },
                { "(fi)ves", score.Fives(getSet.GetSelectedDice)},
                { "(si)xes", score.sixes(getSet.GetSelectedDice) },
                { "(la)rge straight", score.LargeStraight(getSet.GetSelectedDice) },
                { "(sm)all straight", score.SmallStraight(getSet.GetSelectedDice)},
                { "(fu)ll house", score.FullHouse(getSet.GetSelectedDice) },
                { "(Th)ree of a kind", score.ThreeOfAKind(getSet.GetSelectedDice) },
                { "(Fo)ur of a kind", score.FourOfAKind(getSet.GetSelectedDice)},
                { "(ch)ance", score.Chance(getSet.GetSelectedDice) },
                { "(ya)tzhee", score.Yatzhee(getSet.GetSelectedDice)}
             };
        }

    }
  
}
