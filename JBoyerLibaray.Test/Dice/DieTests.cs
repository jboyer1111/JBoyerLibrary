﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Dice
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DieTests
    {

        [TestMethod]
        public void Die_ConstructorOne()
        {
            // Arrange

            // Act
            new Die();

            // Assert
        }

        [TestMethod]
        public void Die_ConstructorTwo()
        {
            // Arrange

            // Act
            new Die(2);

            // Assert
        }

        [TestMethod]
        public void Die_RollKeepsValuesInRange()
        {
            // Arrange
            var die = new Die();
            List<int> results = new List<int>(2000);

            // Act
            for (int i = 0; i < 2000; i++)
            {
                results.Add(die.Roll());
            }

            // Assert
            Assert.IsFalse(results.Any(r => r < 1 || r > 6));
        }

        [TestMethod]
        public void Die_RollsAreWellDistributed()
        {
            Assert.Inconclusive("Fix me");

            // Arrange
            var die = new Die();
            List<int> results = new List<int>(2000);

            // Act
            for (int i = 0; i < 2000; i++)
            {
                results.Add(die.Roll());
            }

            var buckets = (
                from r in results
                group r by r into b
                select new {
                   Group = b.First(),
                   Count = b.Count()
                }
            ).ToList();

            // Assert
            var distributedValue = (results.Count() + 0.000) / (buckets.Count() + 0.000);

            Assert.IsTrue(buckets.Any(b => b.Count < distributedValue - 10 || b.Count > distributedValue + 10 ));
        }

    }
}
