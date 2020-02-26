using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.YatzheeObjects;
namespace Yahtzee
{
   public class ShowYatzheeCollections:YatzheeCollections,IShowGameCollections
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
        public void ShowPossibleMoves(List<string> moves)
        {
            if (moves.Count() <= 12)
            {
                Console.WriteLine("These are the moves you can do ");
                foreach (var item in moves)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
