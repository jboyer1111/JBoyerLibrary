using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Dice;
using System.Collections.Generic;

namespace JBoyerLibaray.Test.DiceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DiceTestOne()
        {
            var die = new WeightedDie(4, i =>
            {
                switch (i)
                {
                    case 1:
                        return 8;
                    default:
                        return 1;
                }
            });
            List<int> rolls = new List<int>(10000);
            for (var i = 0; i < 10000; i++)
            {
                rolls.Add(die.Roll());
            }

            var stats = (
                from f in rolls
                group f by f into groups
                select new {
                    Num = groups.First(),
                    Count = groups.Count()
                }
            ).OrderBy(n => n.Num).ToList();


         }
    }
}
