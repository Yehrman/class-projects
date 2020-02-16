using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
  public  class Class2:Interface1
    {
        int Number { get; set; }
        public Class2(int num)
        {
            Number = num;
        }
public int Score(List<int> dice)
        {
            int score = 0;
            foreach (var item in dice)
            {
                if(item==Number)
                {
                    score += item;
                }
            }
            return score;
        }
    }
}
