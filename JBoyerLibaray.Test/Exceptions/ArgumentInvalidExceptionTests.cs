using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.UnitTests;
using System.Runtime.Serialization;

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
            var argumentInvalidException = new ArgumentInvalidException("Exception Message", _innerException);

            // Act
            var result = UnitTestHelper.Serialize(argumentInvalidException);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArgumentInvalidException_AbleToCreateExceptionFromSerizedInfo()
        {
            // Arrange
            var argumentInvalidException = new ArgumentInvalidException("Exception Message", _innerException);
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedEmptyConstructor()
        {
            // Arrange
            var argumentInvalidException = new ArgumentInvalidException();
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(argumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedOneArgConstructor()
        {
            // Arrange
            var argumentInvalidException = new ArgumentInvalidException("Param Name");
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(argumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedTwoArgConstructorMsgInnerException()
        {
            // Arrange
            var argumentInvalidException = new ArgumentInvalidException("Message", new Exception());
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(argumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedTwoArgConstructorParamNameMsg()
        {
            // Arrange
            var argumentInvalidException = new ArgumentInvalidException("Param Name", "Message");
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(argumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_ValuesAreRetainedWhenSeserializedThreeArgConstructor()
        {
            // Arrange
            var argumentInvalidException = new ArgumentInvalidException("Param Name", "Object Value", "Message");
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.AreEqual(argumentInvalidException.Message, result.Message);
        }

        [TestMethod]
        public void ArgumentInvalidException_GetObjectDataWhenISeriable()
        {
            // Arrange
            ISerializable argumentInvalidException = new ArgumentInvalidException("Param Name", "Object Value", "Message");
            var info = UnitTestHelper.Serialize(argumentInvalidException);

            // Act
            var result = UnitTestHelper.Deserialize<ArgumentInvalidException>(info);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentInvalidException));
            Assert.AreEqual("Message\r\nParameter name: Param Name\r\nActual value was Object Value.", result.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentInvalidException_GetObjectDataThrowsErrorWhenInfoIsNull()
        {
            // Arrange
            ISerializable argumentInvalidException = new ArgumentInvalidException("Param Name", "Object Value", "Message");
            SerializationInfo info = null;
            StreamingContext context = new StreamingContext();
            
            // Act
            argumentInvalidException.GetObjectData(info, context);

            // Assert
        }
    }
}
