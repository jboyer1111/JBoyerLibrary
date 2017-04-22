using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyerLibaray
{
    [TestClass]
    public class ThueMorseTests
    {
        [TestMethod]
        public void ThueMorse_NegativeNumber()
        {
            // Arrange

            // Act
            var result = ThueMorse.GetSquenceLengthOf(-1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ThueMorse_Zero()
        {
            // Arrange

            // Act
            var result = ThueMorse.GetSquenceLengthOf(0);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ThueMorse_One()
        {
            // Arrange

            // Act
            var result = ThueMorse.GetSquenceLengthOf(1);

            // Assert
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void ThueMorse_Two()
        {
            // Arrange

            // Act
            var result = ThueMorse.GetSquenceLengthOf(2);

            // Assert
            Assert.AreEqual("AB", result);
        }

        [TestMethod]
        public void ThueMorse_Three()
        {
            // Arrange

            // Act
            var result = ThueMorse.GetSquenceLengthOf(3);

            // Assert
            Assert.AreEqual("ABB", result);
        }

        [TestMethod]
        public void ThueMorse_Four()
        {
            // Arrange

            // Act
            var result = ThueMorse.GetSquenceLengthOf(4);

            // Assert
            Assert.AreEqual("ABBA", result);
        }
    }
}
