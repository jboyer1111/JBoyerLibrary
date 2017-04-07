using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JBoyerLibaray.ImageHelpers;
using System.Drawing;
using System.IO;
using JBoyerLibaray.Extensions;
using System.Drawing.Imaging;
using JBoyerLibaray.UnitTests;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Diagnostics;
using Moq;
using JBoyerLibaray.FileSystem;

namespace JBoyerLibaray
{
    [TestClass]
    public class CustomHandleErrorAttributeTests
    {
        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionArgNullDoesNothing()
        {
            // Arrange
            var customHandle = new CustomHandleErrorAttribute();
            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>()));

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            // Act
            customHandle.OnException(null);

            // Assert
            mockDirectoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionNullAppSetting()
        {            
            // Arrange
            var customHandle = new CustomHandleErrorAttribute();
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsNull(ConfigurationManager.AppSettings["ErrorLogPath"]);
            // Asserting that no errors are thrown cannot figure out how to test Trace class
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionEmptyAppSetting()
        {
            // Arrange
            var customHandle = new CustomHandleErrorAttribute();
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            ConfigurationManager.AppSettings["ErrorLogPath"] = "";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "");
            // Asserting that no errors are thrown cannot figure out how to test Trace class
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionWhitespaceAppSetting()
        {
            // Arrange
            var customHandle = new CustomHandleErrorAttribute();
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            ConfigurationManager.AppSettings["ErrorLogPath"] = "\r\t\n";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "");
            // Asserting that no errors are thrown cannot figure out how to test Trace class
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ConfigurationManager.RefreshSection("AppSettings");
        }

        #region Inner Class

        public class FakeController : Controller
        {
            
        }

        #endregion
    }
}
