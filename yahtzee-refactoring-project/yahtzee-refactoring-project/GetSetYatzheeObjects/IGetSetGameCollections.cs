using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.YatzheeObjects
{
    interface IGetSetGameCollections
    {
        List<int> GetSpinResult { get; }
        List<int> GetSelectedDice { get; }
        void ThrowDice();
        void RemoveAllDiceFromDiceList(List<int>dice);
        void AddToSelectedDice_RemoveFromSpinResult(int dice);
    }
}
