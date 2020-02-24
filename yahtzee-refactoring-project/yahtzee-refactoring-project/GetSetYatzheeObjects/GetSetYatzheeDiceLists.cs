using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class GetSetYatzheeObjects : YatzheeCollections
    {
        Random random = new Random();

        public List<int> GetSpinResult { get => SpinResult; }
       public int GetDiceCount { get; private set; } = 5;

        public void ThrowDice()
        {
            for (int i = 0; i < GetDiceCount; i++)
            {
                SpinResult.Add(random.Next(1, 7));
            }
        }

        public void SetDiceCount()
        {
            GetDiceCount = SpinResult.Count();
        }

        public void ResetDiceCount()
        {
            GetDiceCount = 5;
        }

        public void RemoveAllDiceFromSpinResult()
        {
            SpinResult.RemoveAll(x => x > 0);
        }

        public void AddToSelectedDice_RemoveFromSpinResult(int dice)
        {
            //1,3.2,5,6
            SelectedDice.Add(dice);
            SelectedDice.Sort();
            SpinResult.Remove(dice);
        }
       

        public void RemoveAllDiceFromSelectedDice()
        {
            SelectedDice.RemoveAll(x => x > 0);
        }
     
        public  List<int> GetSelectedDice { get => SelectedDice; }      
    }
}
