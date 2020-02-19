using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
   public class ShowYatzheeCollections:YatzheeCollections
    {
        public void PrintSpinresult()
        {
            foreach (var item in SpinResult)
            {
                Console.Write(item + " ");
            }
        }

        public void PrintSelectedDice()
        {
            Console.Write("You have selected these dice ");
            foreach (var item in SelectedDice)
            {
                Console.Write(item + " ");
            }
        }
        public void ShowScoringOptions(Dictionary<string, int> options)
        {
            foreach (var item in options)
            {
                Console.WriteLine(item);
            }
        }
    }
}
