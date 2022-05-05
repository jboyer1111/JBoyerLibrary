using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Exceptions
{

    [TestClass, ExcludeFromCodeCoverage]
    public class ExceptionHelperTests
    {

        [TestMethod]
        public void ExceptionHelper_GetMemberName()
        {
            // Arrange
            string MemberName = "";

            // Act
            var result = ExceptionHelper.GetMemberName(() => MemberName);

            // Assert
            Assert.AreEqual("MemberName", result);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentNullException()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentNullException(() => ArgName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentNullException));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: ArgName", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentNullExceptionWithMessage()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentNullException(() => ArgName, "Message");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentNullException));
            Assert.AreEqual("Message\r\nParameter name: ArgName", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentException()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentException(() => ArgName, "Message");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentException));
            Assert.AreEqual("Message\r\nParameter name: ArgName", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentExceptionNullMessage()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentException(() => ArgName, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentException));
            Assert.AreEqual("\r\nParameter name: ArgName", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentOutOfRangeException()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentOutOfRangeException(() => ArgName, "Message", "Object Value");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentOutOfRangeException));
            Assert.AreEqual("Message\r\nParameter name: ArgName\r\nActual value was Object Value.", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentOutOfRangeExceptionNullMessage()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentOutOfRangeException(() => ArgName, null, "Object Value");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentOutOfRangeException));
            Assert.AreEqual("\r\nParameter name: ArgName\r\nActual value was Object Value.", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentInvalidException()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentInvalidException(() => ArgName, "Message", "Object Value");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentInvalidException));
            Assert.AreEqual("Message\r\nParameter name: ArgName\r\nActual value was Object Value.", result.Message);
        }

        [TestMethod]
        public void ExceptionHelper_CreateArgumentInvalidExceptionNullMessage()
        {
            // Arrange
            string ArgName = "";

            // Act
            var result = ExceptionHelper.CreateArgumentInvalidException(() => ArgName, null, "Object Value");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArgumentInvalidException));
            Assert.AreEqual("\r\nParameter name: ArgName\r\nActual value was Object Value.", result.Message);
        }

    }

}
