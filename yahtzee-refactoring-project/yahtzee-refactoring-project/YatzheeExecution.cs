using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
  
    public class YatzheeExecution
    {
        private  List<int> SpinResult { get; set; } = new List<int>();
        private  List<int> SelectedDice { get; set; } = new List<int>();
          
        public YatzheeExecution()
        {
           var spin = SpinResult;
           var select = SelectedDice;
        }

        Random random = new Random();

        public int GetDiceCount { get; private set; } = 5;

        public void ThrowDice()
        {
            for (int i = 0; i < GetDiceCount; i++)
            {
               SpinResult.Add(random.Next(1, 7));
            }
        } 
   
        public void Set5Dice()
        {
            GetDiceCount = 5;
        }
    public void RemoveAllDice()
        {
            SpinResult.RemoveAll(x => x > 0);
        }

        public void AddDice(int dice)
        {
            //1,3.2,5,6
            SelectedDice.Add(dice);
            SpinResult.Remove(dice);
        }

        public void SetDiceCount()
        {
            GetDiceCount = SpinResult.Count();
        }

      
        public List<int> GetSelectedDice { get => SelectedDice; }
        public void PrintSpinresult()
        {
            foreach (var item in SpinResult)
            {
                Console.Write(item + " ");
            }
        }

        public void PrintSelectedDice()
        {
            foreach (var item in SelectedDice)
            {
                Console.Write("You have selected these dice " + item + " ");
            }
        }


    }
}
