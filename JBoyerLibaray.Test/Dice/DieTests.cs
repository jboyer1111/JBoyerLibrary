using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.Dice
{

    [TestClass, ExcludeFromCodeCoverage]
    public class DieTests
    {

        [TestMethod]
        public void Die_Constructor_One()
        {
            // Arrange

            // Act
            new Die();

            // Assert
        }

        [TestMethod]
        public void Die_Constructor_Two()
        {
            // Arrange

            // Act
            new Die(2);

            // Assert
        }

        [TestMethod]
        public void Die_Roll_ReturnsNumberFrom1ToSide()
        {
            // Arrange
            var die = new Die();
            Dictionary<int, int> results = new Dictionary<int, int>();

            // Act
            for (int i = 0; i < 2000; i++)
            {
                var roll = die.Roll();

                if (results.ContainsKey(roll))
                {
                    results[roll]++;
                }
                else
                {
                    results.Add(roll, 1);
                }
            }

            // Assert
            Assert.IsFalse(results.Keys.Count > 6, "Die has too many sides");
        }

    }

}
