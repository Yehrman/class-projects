using System.Linq;
using System.Collections.Generic;
using System;

namespace Yahtzee
{
  
    public class YahtzeeScoreCalculator
    {
        //We need to check user input try/catch
        YatzheeRightNumberChecker numberChecker = new YatzheeRightNumberChecker();
        YatzheeNumberOfAKind numberOfAKind = new YatzheeNumberOfAKind();
        YatzheeUnderlyingMethods UnderlyingMethods = new YatzheeUnderlyingMethods();

           public int Chance(List<int> dice)
            {
            
            int sum = dice.Sum();
                return sum;
            }
            
        public int Yahtzee(List<int> dice)
        {
            var Count =UnderlyingMethods.AmountOfDiceWithSameNumber(5, dice);
            if (Count == 5)
            {
                return 50;
            }
            return 0;

        }

        public int Ones(List<int> ones)
        {
            return numberChecker.CalculateScore(1, ones);
        }

        public int Twos(List<int> twos)
        {
            return numberChecker.CalculateScore(2, twos);
        }

        public int Threes(List<int> threes)
        {
            return numberChecker.CalculateScore(3, threes);
        }

        public int Fours(List<int> fours)
        {
            return numberChecker.CalculateScore(4, fours);
        }

        public int Fives(List<int> fives)
        {
            return numberChecker.CalculateScore(5, fives);
        }

        public int sixes(List<int> sixes)
        {
            return numberChecker.CalculateScore(6, sixes);
        }


        public int FourOfAKind(List<int> dice)
        {
            return numberOfAKind.CalculateScore(4, dice);
        }
        public int ThreeOfAKind(List<int> dice)
        {
            return numberOfAKind.CalculateScore(3, dice);
        }

        //   Small straight: Get four sequential dice, 1,2,3,4 or 2,3,4,5 or 3,4,5,6. Scores 30 points.
        public int SmallStraight(List<int> dice)
        {
            var Count = UnderlyingMethods.SequentialNumbersChecker(dice);
            if (Count == 4)
            {
                return 30;
            }
            return 0;
        }
        //Large straight: Get five sequential dice, 1,2,3,4,5 or 2,3,4,5,6. Scores 40 points.
        public int LargeStraight(List<int> dice)
        {
            var Count = UnderlyingMethods.SequentialNumbersChecker(dice);
            if (Count == 5)
            {
                return 40;
            }
            return 0;
        }
        //Full house: Get three of a kind and a pair, e.g. 3,3,1,3,1 or 3,3,3,6,6. Scores 25 points.
        public int FullHouse(List<int> dice)
        {

            var Three = UnderlyingMethods.AmountOfDiceWithSameNumber(3, dice);
            var Two = UnderlyingMethods.AmountOfDiceWithSameNumber(2, dice);
            int Count = Three + Two;
            /*   int Count = 0;
             for (int i = 1; i <= 6; i++)
             {
             var amount = dice.Where(x => x == i);
             int count = amount.Count();
             if (count == 3 || count == 2)
             {
                 Count += count;
             }*/
            if (Count == 5)
            {
                return 25;
            }
            return 0;
        }
    }
}

