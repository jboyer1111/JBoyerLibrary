using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyerLibaray
{
    [TestClass]
    public class RespectNumbersFilenameComparerTests
    {
        [TestMethod]
        public void RespectNumbersFilenameComparer_XAndYNullReturns0()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare(null, null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XIsNullReturnsNeg1()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare(null, "");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_YIsNullReturnsPos1()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("", null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XAndYAreNonNumbers()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number.txt", "Number.txt");

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XHasNumberAndYDoesNot()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number1.txt", "Number.txt");

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_YHasNumberAndXDoesNot()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number.txt", "Number1.txt");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_BothHaveANumber()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number1", "Number1");

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XHasLeadingZeros()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number0000000001.txt", "Number1.txt");

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XIsGreaterThanY()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number101.txt", "Number1.txt");

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_YIsGreaterThanX()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number11.txt", "Number101.txt");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XAndYHaveSameNumberRelisesOnRemainingText()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number000000000000000001a", "Number1b");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_XIsShorterThanY()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number1a", "Number1aa");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectNumbersFilenameComparer_YIsShorterThanX()
        {
            // Arrange
            var comparer = new RespectNumbersFilenameComparer();

            // Act
            var result = comparer.Compare("Number1aa", "Number1a");

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
