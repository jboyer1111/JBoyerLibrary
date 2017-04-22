using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    public class EmptyHandExceptionTests
    {
        private readonly Exception _innerException;

        public EmptyHandExceptionTests()
        {
            _innerException = new Exception("Inner Exception Message");
        }

        [TestMethod]
        public void EmptyHandException_ConstructorNoArg()
        {
            // Arrange

            // Act
            new EmptyHandException();

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void EmptyHandException_ConstructorOneArg()
        {
            // Arrange

            // Act
            new EmptyHandException("Exception Message");

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void EmptyHandException_ConstructorTwoArgs()
        {
            // Arrange

            // Act
            new EmptyHandException("Exception Message", _innerException);

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void EmptyHandException_AbleToSeriablizeException()
        {
            // Arrange
            var emptyHandException = new EmptyHandException("Exception Message", _innerException);

            // Act
            var result = UnitTestHelper.Serialize(emptyHandException);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmptyHandException_AbleToCreateExceptionFromSerizedInfo()
        {
            // Arrange
            var emptyHandException = new EmptyHandException("Exception Message", _innerException);
            var info = UnitTestHelper.Serialize(emptyHandException);

            // Act
            var result = UnitTestHelper.Deserialize<EmptyHandException>(info);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
