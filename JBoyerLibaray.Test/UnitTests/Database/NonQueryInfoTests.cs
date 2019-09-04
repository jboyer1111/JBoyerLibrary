using JBoyerLibaray.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class NonQueryInfoTests
    {

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: expectedParameters")]
        public void NonQueryInfo_Constructor_NullParametersThrowsArgumentNullException()
        {
            // Arrange

            // Act
            new NonQueryInfo(null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: nonQueryCallback")]
        public void NonQueryInfo_Constructor_Callback_NullCallbackThrowsArgumentNullException()
        {
            // Arrange

            // Act
            new NonQueryInfo(null, new string[] { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: expectedParameters")]
        public void NonQueryInfo_Constructor_Callback_NullParametersThrowsArgumentNullException()
        {
            // Arrange

            // Act
            new NonQueryInfo((d, p) => { }, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Non query is missing a required Parameter with the name \"Missing\".")]
        public void NonQueryInfo_DoCallback_ThrowsExceptionWhenMissAParameter()
        {
            // Arrange
            var info = new NonQueryInfo((d, p) => { }, new string[] { "Missing" });

            // Act
            info.DoCallback(new FakeDatabase(), new FakeParameterCollection());

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Non query was passed an extra Parameter with the name \"Extra\".")]
        public void NonQueryInfo_DoCallback_ThrowsExceptionWhenHasAnExtraParameter()
        {
            // Arrange
            var info = new NonQueryInfo((d, p) => { }, new string[] { });
            var fakeParameterCollection = new FakeParameterCollection();
            fakeParameterCollection.Add(new FakeParameter("Extra", "Value"));

            // Act
            info.DoCallback(new FakeDatabase(), fakeParameterCollection);

            // Assert
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void NonQueryInfo_DoCallback_ThrowsExceptionWhenUnableToCastToIDataParameter()
        {
            // Arrange
            var parameterCollection = new List<TableRow>() { new TableRow(1) };

            var info = new NonQueryInfo((d, p) => { }, new string[] { });
            Mock<IDataParameterCollection> mockParameterCollection = new Mock<IDataParameterCollection>();
            mockParameterCollection.Setup(c => c.GetEnumerator()).Returns(() => parameterCollection.GetEnumerator());
            
            // Act
            info.DoCallback(new FakeDatabase(), mockParameterCollection.Object);

            // Assert
        }

        [TestMethod]
        public void NonQueryInfo_DoCallback_NoCallbackDoesNothingExtra()
        {
            // Arrange
            var info = new NonQueryInfo(new string[] { });

            // Act
            info.DoCallback(new FakeDatabase(), new FakeParameterCollection());

            // Assert
        }

        [TestMethod]
        public void NonQueryInfo_DoCallback_RunsPassedCallback()
        {
            // Arrange
            bool result = false;
            var info = new NonQueryInfo((d, p) => { result = true; }, new string[] { });

            // Act
            info.DoCallback(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsTrue(result, "Callback was not executed.");
        }

    }

}
