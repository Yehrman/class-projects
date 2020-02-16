using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class YatzheeNumberOfAKind:IDiceScoreCalculator
    {
        public int CalculateScore(int number, List<int> dice)
        {
            int Sum = dice.Sum();
            var NumberOfKind = new YatzheeUnderlyingMethods();
            var Count = NumberOfKind.AmountOfDiceWithSameNumber(number, dice);
            if (Count == number)
            {
                return Sum;
            }          
           //     Console.WriteLine("Please pick a valid option");
            return 0;
        }
    }
}

