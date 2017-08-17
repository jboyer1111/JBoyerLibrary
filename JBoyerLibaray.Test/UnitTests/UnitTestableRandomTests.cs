using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UnitTestableRandomTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnitTestableRandom_ConstructorThrowsArugumentNullExceptionWhenIntArrayIsNull()
        {
            // Arrange
            int[] numbers = null;

            // Act
            new UnitTestableRandom(numbers);

            // Assert
        }

        [TestMethod]
        public void UnitTestableRandom_ConstructorNoArgs()
        {
            // Arrange

            // Act
            new UnitTestableRandom();

            // Assert
        }

        [TestMethod]
        public void UnitTestableRandom_ConstructorIntArgs()
        {
            // Arrange

            // Act
            new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Assert
        }

        [TestMethod]
        public void UnitTestableRandom_NextReturnsNextNumber()
        {
            // Arrange
            var expected = new int[] { 1, 2, 3, 4 };
            var result = new List<int>();
            var rand = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.Next());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UnitTestableRandom_NextReturnsNextNumberStartBackAtBeginingOfListWhenReachesEnd()
        {
            // Arrange
            var expected = new int[] { 1, 2, 3, 1, 2, 3 };
            var result = new List<int>();
            var rand = new UnitTestableRandom(1, 2, 3);

            // Act
            for (int i = 0; i < 6; i++)
            {
                result.Add(rand.Next());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UnitTestableRandom_NextMaxValueOnlyReturnsValidNumbers()
        {
            // Arrange
            var expected = new int[] { 1, 1, 1, 1 };
            var result = new List<int>();
            var rand = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.Next(2));
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UnitTestableRandom_NextMinMaxValueOnlyReturnsValidNumbers()
        {
            // Arrange
            var expected = new int[] { 3, 4, 5, 3 };
            var result = new List<int>();
            var rand = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.Next(3, 6));
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UnitTestableRandom_NextBytesFillsByteArray()
        {
            // Arrange
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var result = new byte[10];
            var rand = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            rand.NextBytes(result);

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }


        [TestMethod]
        public void UnitTestableRandom_NextBytesFillsByteArrayTwo()
        {
            // Arrange
            var expected = new byte[] { 4, 5, 6, 7, 8, 9, 10, 1, 2, 3 };
            var result = new byte[10];
            var rand = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Get Three numbers
            rand.Next();
            rand.Next();
            rand.Next();

            // Act
            rand.NextBytes(result);

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UnitTestableRandom_NextDouble()
        {
            // Arrange
            var expected = new double[] { 1, 2, 3, 4 };
            var result = new List<double>();
            var rand = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.NextDouble());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UnitTestableRandom_NextDoubleReturnsNextNumberStartBackAtBeginingOfListWhenReachesEnd()
        {
            // Arrange
            var expected = new double[] { 1, 2, 3, 1, 2, 3 };
            var result = new List<double>();
            var rand = new UnitTestableRandom(1, 2, 3);

            // Act
            for (int i = 0; i < 6; i++)
            {
                result.Add(rand.NextDouble());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }
    }
}
