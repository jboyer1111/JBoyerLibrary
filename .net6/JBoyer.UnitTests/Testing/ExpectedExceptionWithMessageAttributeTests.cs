using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    [TestClass, ExcludeFromCodeCoverage]
    public class ExpectedExceptionWithMessageAttributeTests
    {

        [TestMethod]
        public void ExpectedExceptionWithMessageAttribute_Verify_ThrowsExceptionWhenTypeIsNull()
        {
            try
            {
                // Arrange
                var attr = new ExpectedExceptionWithMessageAttribute(null, "Message");

                // Act
                attr.UTVerify(new Exception());
            }
            catch (AssertInconclusiveException e) when (String.Equals(e.Message, "Exception Type was not set.")) 
            {
                return;
            }

            Assert.Fail("Excpected Exception Type not thrown");
        }

        [TestMethod]
        public void ExpectedExceptionWithMessageAttribute_Verify_ThrowsExceptionWhenTypeDoesNotMatchThrownType()
        {
            try
            {
                // Arrange
                var attr = new ExpectedExceptionWithMessageAttribute(typeof(ArgumentException), "Message");

                // Act
                attr.UTVerify(new Exception("Text"));
            }
            catch (AssertFailedException e) when (String.Equals(e.Message, "ExpectedExceptionWithMessageAttribute failed. Expected exception type: System.ArgumentException. Actual exception type: System.Exception. Exception message: Text"))
            {
                return;
            }

            Assert.Fail("Excpected Exception Type not thrown");
        }

        [TestMethod]
        public void ExpectedExceptionWithMessageAttribute_Verify_ThrowsExceptionWhenMessagesAreNotTheSame()
        {
            try
            {
                // Arrange
                var attr = new ExpectedExceptionWithMessageAttribute(typeof(ArgumentException), "Message");

                // Act
                attr.UTVerify(new ArgumentException("Text"));
            }
            catch (AssertFailedException e) when (String.Equals(e.Message, "Assert.AreEqual failed. Expected:<Message>. Actual:<Text>. "))
            {
                return;
            }

            Assert.Fail("Excpected Exception Type not thrown");
        }

        [TestMethod]
        public void ExpectedExceptionWithMessageAttribute_Verify_DoesNotThrowErrorWhenMatchesProperly()
        {
            // Arrange
            var attr = new ExpectedExceptionWithMessageAttribute(typeof(ArgumentException), "Message");

            // Act
            attr.UTVerify(new ArgumentException("Message"));
        }

    }

}
