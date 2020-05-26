using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
 internal  class YatzheeNumberOfAKind:YatzheeAmountOfDice,IDiceScoreCalculator
    {
        public  int CalculateScore(int number, List<int> dice)
        {
            int Sum = dice.Sum();
            var Count =AmountOfDiceWithSameNumber(number, dice);
            if (Count == number)
            {
                return Sum;
            }          
            return 0;
        }
       
    }
}

