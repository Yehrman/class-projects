using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.YatzheeObjects;

namespace Yahtzee
{
    public class GetSetYatzheeDiceLists : YatzheeCollections, IGetSetGameCollections 
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

        public void RemoveAllDiceFromDiceList(List<int>dice)
        {
            dice.RemoveAll(x => x > 0);
        }
       
    
        public void AddToSelectedDice_RemoveFromSpinResult(int die)
        {       
            SpinResult.Remove(die);
                
            if(SelectedDice.Count()<1)
            {
                SelectedDice.Add(die);
            }
            else if(SelectedDice.All(x=>x!=die))
            {
                SelectedDice.Add(die);
                SelectedDice.Sort();
            }
            else 
            {
                SelectedDice.Add(die);
            }
         
        }
           
        public  List<int> GetSelectedDice { get => SelectedDice; }      
    }
}
