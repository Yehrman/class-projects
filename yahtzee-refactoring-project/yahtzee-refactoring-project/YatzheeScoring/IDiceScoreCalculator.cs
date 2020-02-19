using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
   public interface IDiceScoreCalculator
    {
        int CalculateScore(int number,List<int> dice);
    }
}
