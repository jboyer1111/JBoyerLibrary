using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyerLibaray
{
    [TestClass]
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
            var result = comparer.Compare("1est", "Test");

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
