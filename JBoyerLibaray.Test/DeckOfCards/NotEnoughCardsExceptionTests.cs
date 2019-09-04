using JBoyerLibaray.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.DeckOfCards
{

    [TestClass, ExcludeFromCodeCoverage]
    public class NotEnoughCardsExceptionTests
    {
        private readonly Exception _innerException;

        public NotEnoughCardsExceptionTests()
        {
            _innerException = new Exception("Inner Exception Message");
        }

        [TestMethod]
        public void NotEnoughCardsException_ConstructorNoArg()
        {
            // Arrange

            // Act
            new NotEnoughCardsException();

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void NotEnoughCardsException_ConstructorOneArg()
        {
            // Arrange

            // Act
            new NotEnoughCardsException("Exception Message");

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void NotEnoughCardsException_ConstructorTwoArgs()
        {
            // Arrange

            // Act
            new NotEnoughCardsException("Exception Message", _innerException);

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void NotEnoughCardsException_AbleToSeriablizeException()
        {
            // Arrange
            var NotEnoughCardsException = new NotEnoughCardsException("Exception Message", _innerException);

            // Act
            var result = UnitTestHelper.Serialize(NotEnoughCardsException);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NotEnoughCardsException_AbleToCreateExceptionFromSerizedInfo()
        {
            // Arrange
            var NotEnoughCardsException = new NotEnoughCardsException("Exception Message", _innerException);
            var info = UnitTestHelper.Serialize(NotEnoughCardsException);

            // Act
            var result = UnitTestHelper.Deserialize<NotEnoughCardsException>(info);

            // Assert
            Assert.IsNotNull(result);
        }

    }

}
