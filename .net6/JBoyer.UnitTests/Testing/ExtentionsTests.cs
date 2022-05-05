using JBoyer.Testing.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    [TestClass, ExcludeFromCodeCoverage]
    public class ExtentionsTests
    {

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Unable to find parameter: Test")]
        public void Extentions_GetParamValue_ThrowsExceptionWhenParmeterDoesNotExist()
        {
            // Arrange
            var parameters = new FakeParameterCollection();

            // Act
            parameters.GetParamValue<int>("Test");

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Parameter value is null")]
        public void Extentions_GetParamValue_ThrowsExceptionWhenParmeterValueIsNull()
        {
            // Arrange
            var parameters = new FakeParameterCollection();
            parameters.Add(new FakeParameter("Test", null));

            // Act
            parameters.GetParamValue<int>("Test");

            // Assert
        }

        [TestMethod]
        public void Extentions_GetParamValue_ReturnsExpectedValue()
        {
            // Arrange
            var parameters = new FakeParameterCollection();
            parameters.Add(new FakeParameter("Test", 50));

            // Act
            int result = parameters.GetParamValue<int>("Test");

            // Assert
            Assert.AreEqual(50, result);
        }

    }

}
