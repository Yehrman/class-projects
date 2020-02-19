using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class YatzheeCollections
    {
        protected static List<int> SpinResult { get; set; } = new List<int>();

        protected static List<int> SelectedDice { get; set; } = new List<int>();

        protected static Dictionary<string, int> ScoringOptions()
        {
            GetSetYatzheeObjects getSet = new GetSetYatzheeObjects();

            YahtzeeScoreCalculator score = new YahtzeeScoreCalculator();

            return new Dictionary<string, int>()
            {
                {    "ones", score.Ones(getSet.GetSelectedDice) },
                { "twos", score.Twos(getSet.GetSelectedDice) },
                { "threes", score.Threes(getSet.GetSelectedDice)},
                { "fours", score.Fours(getSet.GetSelectedDice) },
                { "fives", score.Fives(getSet.GetSelectedDice)},
                { "sixes", score.sixes(getSet.GetSelectedDice) },
                { "large straight", score.LargeStraight(getSet.GetSelectedDice) },
                { "small straght", score.SmallStraight(getSet.GetSelectedDice)},
                { "full house", score.FullHouse(getSet.GetSelectedDice) },
                { "three of a kind", score.ThreeOfAKind(getSet.GetSelectedDice) },
                { "four of a kind", score.FourOfAKind(getSet.GetSelectedDice)},
                { "chance", score.Chance(getSet.GetSelectedDice) },
                { "yatzhee", score.Yahtzee(getSet.GetSelectedDice)}
             };
        }

    }
}
