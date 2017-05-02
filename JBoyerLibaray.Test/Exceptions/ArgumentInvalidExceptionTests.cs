using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.Exceptions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ArgumentInvalidExceptionTests
    {
        private readonly Exception _innerException;

        public ArgumentInvalidExceptionTests()
        {
            _innerException = new Exception("Inner Exception Message");
        }

        [TestMethod]
        public void ArgumentInvalidException_ConstructorNoArg()
        {
            // Arrange

            // Act
            new ArgumentInvalidException();

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void ArgumentInvalidException_ConstructorOneArg()
        {
            // Arrange

            // Act
            new ArgumentInvalidException("Exception Message");

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void ArgumentInvalidException_ConstructorTwoArgs()
        {
            // Arrange

            // Act
            new ArgumentInvalidException("Exception Message", _innerException);

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void ArgumentInvalidException_AbleToSeriablizeException()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException("Exception Message", _innerException);

            // Act
            var result = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArgumentInvalidException_AbleToCreateExceptionFromSerizedInfo()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException("Exception Message", _innerException);
            var info = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedEmptyConstructor()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException();
            var info = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(ArgumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedOneArgConstructor()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException("Param Name");
            var info = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(ArgumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedTwoArgConstructorMsgInnerException()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException("Message", new Exception());
            var info = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(ArgumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedTwoArgConstructorParamNameMsg()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException("Param Name", "Message");
            var info = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(ArgumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedThreeArgConstructor()
        {
            // Arrange
            var ArgumentInvalidException = new ArgumentInvalidException("Param Name", "Object Value", "Message");
            var info = UnitTestHelper.Serialize(ArgumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(ArgumentInvalidException.Message, result.Message);
        }
    }
}
