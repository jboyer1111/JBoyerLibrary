using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.Exceptions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CouldNotConvertToNumberTests
    {
        private readonly Exception _innerException;

        public CouldNotConvertToNumberTests()
        {
            _innerException = new Exception("Inner Exception Message");
        }

        [TestMethod]
        public void CouldNotConvertToNumber_ConstructorNoArg()
        {
            // Arrange

            // Act
            new CouldNotConvertToNumber();

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void CouldNotConvertToNumber_ConstructorOneArg()
        {
            // Arrange

            // Act
            new CouldNotConvertToNumber("Exception Message");

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void CouldNotConvertToNumber_ConstructorTwoArgs()
        {
            // Arrange

            // Act
            new CouldNotConvertToNumber("Exception Message", _innerException);

            // Assert
            // No assert. Just making sure it does not throw exception.
        }

        [TestMethod]
        public void CouldNotConvertToNumber_AbleToSeriablizeException()
        {
            // Arrange
            var CouldNotConvertToNumber = new CouldNotConvertToNumber("Exception Message", _innerException);

            // Act
            var result = UnitTestHelper.Serialize(CouldNotConvertToNumber);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CouldNotConvertToNumber_AbleToCreateExceptionFromSerizedInfo()
        {
            // Arrange
            var CouldNotConvertToNumber = new CouldNotConvertToNumber("Exception Message", _innerException);
            var info = UnitTestHelper.Serialize(CouldNotConvertToNumber);

            // Act
            var result = UnitTestHelper.Deserialize<CouldNotConvertToNumber>(info);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
