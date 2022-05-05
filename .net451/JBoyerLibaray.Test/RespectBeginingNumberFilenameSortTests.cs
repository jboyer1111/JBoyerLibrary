using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RespectBeginingNumberFilenameSortTests
    {
        [TestMethod]
        public void RespectBeginingNumberFilenameSort_XAndYNullReturns0()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare(null, null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_XIsNullReturnsNeg1()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare(null, "");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_YIsNullReturnsPos1()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("", null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_XHasNumberYDoesNot()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("1aa", "aaa");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_YHasNumberXDoesNot()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("aaa", "1aa");

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_XNumberIsLessThanY()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("1aa", "12aa");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_YNumberIsLessThanX()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("12aa", "1aa");

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_NumbersAreEqual()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("12aa", "12aa");

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void RespectBeginingNumberFilenameSort_NumbersAreEqualButTextIsNot()
        {
            // Arrange
            var comparer = new RespectBeginingNumberFilenameSort();

            // Act
            var result = comparer.Compare("12a", "12b");

            // Assert
            Assert.AreEqual(-1, result);
        }
    }
}
