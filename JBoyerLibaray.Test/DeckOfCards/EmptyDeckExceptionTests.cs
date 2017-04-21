using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    public class EmptyDeckExceptionTests
    {
        private readonly Exception _innerException;

        public EmptyDeckExceptionTests()
        {
            _innerException = new Exception("Inner Exception Message");
        }

        [TestMethod]
        public void EmptyDeckException_ConstructorNoArg()
        {
            // Arrange

            // Act
            new EmptyDeckException();

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void EmptyDeckException_ConstructorOneArg()
        {
            // Arrange

            // Act
            new EmptyDeckException("Exception Message");

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void EmptyDeckException_ConstructorTwoArgs()
        {
            // Arrange

            // Act
            new EmptyDeckException("Exception Message", _innerException);

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void EmptyDeckException_AbleToSeriablizeException()
        {
            // Arrange
            var emptyDeckException = new EmptyDeckException("Exception Message", _innerException);

            // Act
            var result = UnitTestHelper.Serialize(emptyDeckException);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmptyDeckException_AbleToCreateExceptionFromSerizedInfo()
        {
            // Arrange
            var emptyDeckException = new EmptyDeckException("Exception Message", _innerException);
            var info = UnitTestHelper.Serialize(emptyDeckException);

            // Act
            var result = UnitTestHelper.Deserialize<EmptyDeckException>(info);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
