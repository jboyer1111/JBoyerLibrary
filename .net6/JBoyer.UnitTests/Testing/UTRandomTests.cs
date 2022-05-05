using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    [TestClass, ExcludeFromCodeCoverage]
    public class UTRandomTests
    {

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UTRandom_Constructor_ThrowsArugumentNullExceptionWhenIntArrayIsNull()
        {
            // Arrange
            int[] numbers = null;

            // Act
            new UTRandom(numbers);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "You need to have at least one number. (Parameter 'numbers')")]
        public void UTRandom_Constructor_NoArgs()
        {
            // Arrange

            // Act
            new UTRandom();

            // Assert
        }

        [TestMethod]
        public void UTRandom_Constructor_IntArgs()
        {
            // Arrange

            // Act
            new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Assert
        }

        [TestMethod]
        public void UTRandom_Next_ReturnsNextNumber()
        {
            // Arrange
            var expected = new int[] { 1, 2, 3, 4 };
            var result = new List<int>();
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.Next());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UTRandom_Next_ReturnsNextNumberStartBackAtBeginingOfListWhenReachesEnd()
        {
            // Arrange
            var expected = new int[] { 1, 2, 3, 1, 2, 3 };
            var result = new List<int>();
            var rand = new UTRandom(1, 2, 3);

            // Act
            for (int i = 0; i < 6; i++)
            {
                result.Add(rand.Next());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UTRandom_Next_MaxValue_OnlyReturnsValidNumbers()
        {
            // Arrange
            var expected = new int[] { 1, 1, 1, 1 };
            var result = new List<int>();
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.Next(2));
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "List of numbers do not have value less than the max value. (Parameter 'maxValue')")]
        public void UTRandom_Next_MaxValue_ThrowsErrorIfMaxNumberCanNotBeSupplied()
        {
            // Arrange
            var rand = new UTRandom(10, 20, 30);

            // Act
            rand.Next(6);

            // Assert
        }

        [TestMethod]
        public void UTRandom_Next_MinValue_MaxValue_OnlyReturnsValidNumbers()
        {
            // Arrange
            var expected = new int[] { 3, 4, 5, 3 };
            var result = new List<int>();
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.Next(3, 6));
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "List of numbers do not have a valid value with passed parameters.")]
        public void UTRandom_Next_MinValue_MaxValue_ThrowsErrorIfMaxNumberCanNotBeSupplied()
        {
            // Arrange
            var rand = new UTRandom(10, 20, 30);

            // Act
            rand.Next(1, 6);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "List of numbers do not have a valid value with passed parameters.")]
        public void UTRandom_Next_MinValue_MaxValue_ThrowsErrorIfMinNumberCanNotBeSupplied()
        {
            // Arrange
            var rand = new UTRandom(1, 2, 3, 4, 10, 11, 12);

            // Act
            rand.Next(5, 10);

            // Assert
        }

        [TestMethod]
        public void UTRandom_NextBytes_FillsByteArray()
        {
            // Arrange
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var result = new byte[10];
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            rand.NextBytes(result);

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UTRandom_NextBytes_FillsByteArrayTwo()
        {
            // Arrange
            var expected = new byte[] { 4, 5, 6, 7, 8, 9, 10, 1, 2, 3 };
            var result = new byte[10];
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

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
        public void UTRandom_NextDouble_ReturnsNumbersAsDoubles()
        {
            // Arrange
            var expected = new double[] { 1, 2, 3, 4 };
            var result = new List<double>();
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Act
            for (int i = 0; i < 4; i++)
            {
                result.Add(rand.NextDouble());
            }

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void UTRandom_NextDouble_ReturnsNextNumberStartBackAtBeginingOfListWhenReachesEnd()
        {
            // Arrange
            var expected = new double[] { 1, 2, 3, 1, 2, 3 };
            var result = new List<double>();
            var rand = new UTRandom(1, 2, 3);

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
