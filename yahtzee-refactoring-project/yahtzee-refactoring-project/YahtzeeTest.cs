using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
namespace Yahtzee
{
    [TestFixture]
    public class YahtzeeTest
    {
        YahtzeeScoreCalculator yahtzee = new YahtzeeScoreCalculator();
        YatzheeNumberOfAKind numberOfAKind = new YatzheeNumberOfAKind();
        YatzheeRightNumberChecker numberChecker = new YatzheeRightNumberChecker();
        YatzheeUnderlyingMethods underlyingMethods = new YatzheeUnderlyingMethods();
        [Test]
        public void Check_If_NumberOfKind_works()
        {
           int [] arr = { 4, 4, 2, 4, 4 };
            var list = arr.ToList();
            var sum = list.Sum();
            Assert.AreEqual(sum, numberOfAKind.CalculateScore(4,list));
        }
        [Test]
        public void Check_If_RightNumberChecker_Works()
        {
            int[] arr = { 2, 3, 5, 2, 1 };
            var list = arr.ToList();
            Assert.AreEqual(4, numberChecker.CalculateScore(2, list));
        }
        [Test]
        public void Does_Chance_scores_sum_of_all_dice()
        {
            int[] arr = { 3, 2, 5, 4, 2 };
            var list = arr.ToList();
            var sum = list.Sum();
            Assert.AreEqual(sum,yahtzee.Chance(list));
           
        }

        [Test]
        public void Does_Yahtzee_scores_50()
        {
            int[] arr = { 3, 3, 3, 3, 3 };
            var list = arr.ToList();
            int[] arr2 = { 3, 5, 3, 3, 3 };
            var list2 = arr2.ToList();
            Assert.AreEqual(50, yahtzee.Yahtzee(list));
            Assert.AreEqual(0, yahtzee.Yahtzee(list2));
        }
      
        public void Check_If_HowManyNumbersOfAkind_works()
        {
            int[] arr = { 1, 2, 3, 4, 3 };
            var list = arr.ToList();
            int[] arr2 = { 3, 5, 3, 3, 3 };
            var list2 = arr.ToList();
            Assert.AreEqual(0, underlyingMethods.AmountOfDiceWithSameNumber(3, list));
            Assert.AreEqual(4, underlyingMethods.AmountOfDiceWithSameNumber(4, list2));
        }
            [Test]
            public void Test_1s()
            {
            int[] arr = { 1, 4, 3, 1, 3 };
            var list = arr.ToList();
            int[] arr2 = { 2, 5, 2, 3, 3 };
            var list2 = arr2.ToList();
            int[] arr3 = { 1, 1, 2, 1, 1 };
            var list3 = arr3.ToList();
            Assert.AreEqual(2, yahtzee.Ones(list));
                Assert.AreEqual(0, yahtzee.Ones(list2));
                Assert.AreEqual(4, yahtzee.Ones(list3));
            }


        [Test]
        public void Test_4s()
        {
            int[] arr = { 1, 4, 3, 4, 3 };
            var list = arr.ToList();
            int[] arr2 = { 4, 4, 2, 3, 4 };
            var list2 = arr2.ToList();
            Assert.AreEqual(8, yahtzee.Fours(list));
            Assert.AreEqual(12, yahtzee.Fours(list2));
        }


        [Test]
            public void Three_of_a_kind()
            {
            int[] arr = { 1, 3, 3, 4, 3 };
            var list = arr.ToList();
            var sum = list.Sum();
            int[] arr2 = { 3, 5, 3, 5, 3 };
            var list2 = arr2.ToList();
            var sum2 = list2.Sum();
            int[] arr3 = { 3, 1, 4, 5, 3 };
            var list3 = arr3.ToList();
            Assert.AreEqual(sum, yahtzee.ThreeOfAKind(list));
                Assert.AreEqual(sum2,yahtzee.ThreeOfAKind(list2));
               Assert.AreEqual(0, yahtzee.ThreeOfAKind(list3));
            }

            [Test]
            public void Four_of_a_kind()
            {
            int[] arr = { 3, 3, 3, 4, 3 };
            var list = arr.ToList();
            var sum = list.Sum();
            int[] arr2 = { 6, 6, 3, 6, 6 };
            var list2 = arr2.ToList();
            var sum2 = list2.Sum();
            int[] arr3 = { 1, 6, 3, 6, 6 };
            var list3 = arr3.ToList();
            Assert.AreEqual(sum, yahtzee.FourOfAKind(list));
                Assert.AreEqual(sum2, yahtzee.FourOfAKind(list2));
                 Assert.AreEqual( 0, yahtzee.FourOfAKind(list3));
            }

        [Test]
        public void SmallStraight()
        {
            int[] arr = { 2, 3, 4, 5, 2 };
            var list = arr.ToList();
            int[] arr2 = { 1, 4, 1, 3, 2 };
            var list2 = arr2.ToList();
            Assert.AreEqual(30, yahtzee.SmallStraight(list));
            Assert.AreEqual(0, yahtzee.SmallStraight(list2));
        }
        
        [Test]
        public void LargeStraight()
        {
            int[] arr = { 2, 3, 4, 5, 6 };
            var list = arr.ToList();
            int[] arr2 = { 1, 4, 1, 3, 2 };
            var list2 = arr2.ToList();
            Assert.AreEqual(40, yahtzee.LargeStraight(list));
            Assert.AreEqual(0, yahtzee.LargeStraight(list2));
        }
        
        [Test]
        public void FullHouse()
        {
            int[] arr = { 3, 3, 2, 3, 2 };
            var list = arr.ToList();
            int[] arr2 = { 1, 2, 4, 3, 2 };
            var list2 = arr2.ToList();
            Assert.AreEqual(25, yahtzee.FullHouse(list));
            Assert.AreEqual(0, yahtzee.FullHouse(list2));
        }
        [Test]
        public void Check_if_SequentialNumbersChecker_works()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            var list = arr.ToList();
            int[] arr2 = { 1, 2, 3, 4, 2 };
            var list2 = arr2.ToList();
            Assert.AreEqual(5, underlyingMethods.SequentialNumbersChecker(list));
            Assert.AreEqual(4, underlyingMethods.SequentialNumbersChecker(list2));
        }
    }

}
