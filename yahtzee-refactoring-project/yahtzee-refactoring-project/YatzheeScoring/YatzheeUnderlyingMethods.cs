using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class YatzheeUnderlyingMethods
    {
        public int AmountOfDiceWithSameNumber(int number, List<int> dice)
        {
            int count = 0;
            for (int i = 1; i <= 6; i++)
            {
                var amount = dice.Where(x => x == i);
                count = amount.Count();
                if (count == number)
                {
                    return count;
                }
            }
            return 0;
        }
        public int SequentialNumbersChecker( List<int> dice)
        {
            int count = 1;
            for (int i = 0; i < dice.Count - 1; i++)
            {
                var current = dice[i];
                var next = current + 1;
                if (dice[i + 1] == next)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }
    }
}

