using System.Linq;
using System.Collections.Generic;
using System;

namespace Yahtzee
{
  
    internal class YahtzeeScoreCalculator:YatzheeAmountOfDice
    {
   
        YatzheeRightNumberChecker numberChecker = new YatzheeRightNumberChecker();
        YatzheeNumberOfAKind numberOfAKind = new YatzheeNumberOfAKind();
     
           internal int Chance(List<int> dice)
            {           
            int sum = dice.Sum();
                return sum;
            }
            
        internal int Ones(List<int> ones)
        {
            return numberChecker.CalculateScore(1, ones);
        }

        internal int Twos(List<int> twos)
        {
            return numberChecker.CalculateScore(2, twos);
        }

        internal int Threes(List<int> threes)
        {
            return numberChecker.CalculateScore(3, threes);
        }

        internal int Fours(List<int> fours)
        {
            return numberChecker.CalculateScore(4, fours);
        }

        internal int Fives(List<int> fives)
        {
            return numberChecker.CalculateScore(5, fives);
        }

        internal int sixes(List<int> sixes)
        {
            return numberChecker.CalculateScore(6, sixes);
        }

        internal  int Yatzhee(List<int> dice)
        {     
            var Count =AmountOfDiceWithSameNumber(5, dice);
            if (Count == 5)
            {
                return 50;
            }
            return 0;
        }
        internal int FourOfAKind(List<int> dice)
        {
            return numberOfAKind.CalculateScore(4, dice);
        }
        internal int ThreeOfAKind(List<int> dice)
        {
            return numberOfAKind.CalculateScore(3, dice);
        }

        //   Small straight: Get four sequential dice, 1,2,3,4 or 2,3,4,5 or 3,4,5,6. Scores 30 points.
        internal int SmallStraight(List<int> dice)
        {
            var Count =AmountOfDiceInSequence(dice);
            if (Count == 4)
            {
                return 30;
            }
            return 0;
        }
        //Large straight: Get five sequential dice, 1,2,3,4,5 or 2,3,4,5,6. Scores 40 points.
        internal int LargeStraight(List<int> dice)
        {
            var Count =AmountOfDiceInSequence(dice);
            if (Count == 5)
            {
                return 40;
            }
            return 0;
        }
        //Full house: Get three of a kind and a pair, e.g. 3,3,1,3,1 or 3,3,3,6,6. Scores 25 points.
        internal int FullHouse(List<int> dice)
        {

            var Three =AmountOfDiceWithSameNumber(3, dice);
            var Two =AmountOfDiceWithSameNumber(2, dice);
            int Count = Three + Two;
           
            if (Count == 5)
            {
                return 25;
            }
            return 0;
        }
    }
}

