using System;
using System.Collections.Generic;
using System.Linq;

namespace Yahtzee
{
     class YatzheeRightNumberChecker : IDiceScoreCalculator
    {
        public int CalculateScore(int number, List<int> dice)
        {
            int sum = 0;
            foreach (var item in dice)
            {
                if (item == number)
                {
                    sum += item;
                }
            }
            return sum;
        }
    }

  

  
  
}
    