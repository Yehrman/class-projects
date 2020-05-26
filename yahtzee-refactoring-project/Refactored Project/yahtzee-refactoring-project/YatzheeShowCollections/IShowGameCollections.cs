using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.YatzheeObjects
{
    interface IShowGameCollections
    {
     void PrintSpinresult();
        void PrintSelectedDice();
        void ShowScoringOptions(Dictionary<string, int> options);
    }
}
