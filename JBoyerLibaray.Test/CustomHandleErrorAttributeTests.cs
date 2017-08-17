using JBoyerLibaray.FileSystem;
using JBoyerLibaray.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CustomHandleErrorAttributeTests
    {

        [TestMethod]
        public void CustomHandleErrorAttribute_ConstructorOne()
        {
            // Arrange

            // Act
            new CustomHandleErrorAttribute();

            // Assert
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_ConstructorTwo()
        {
            // Arrange
            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();

            // Act
            new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomHandleErrorAttribute_ConstructorThrowErrorIfArgumentIsNull()
        {
            // Arrange

            // Act
            new CustomHandleErrorAttribute(null);

            // Assert
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionArgNullDoesNothing()
        {
            // Arrange
            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>()));

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            // Act
            customHandle.OnException(null);

            // Assert
            mockDirectoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionNullAppSetting()
        {            
            // Arrange
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>()));

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            ConfigurationManager.AppSettings["ErrorLogPath"] = null;

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsNull(ConfigurationManager.AppSettings["ErrorLogPath"]);
            mockDirectoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionEmptyAppSetting()
        {
            // Arrange
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>()));

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            ConfigurationManager.AppSettings["ErrorLogPath"] = "";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "");
            mockDirectoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionWhitespaceAppSetting()
        {
            // Arrange
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>()));

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            ConfigurationManager.AppSettings["ErrorLogPath"] = "   ";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "   ");
            mockDirectoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionPathToExistingFolderCallsExistsOnlyOnce()
        {
            // Arrange
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();

            var existsQueue = new ReturnsQueue<bool>(false);
            existsQueue.Enqueue(true);
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>())).Returns(() => existsQueue.GetNext());

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            ConfigurationManager.AppSettings["ErrorLogPath"] = "D:\\Temp";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "D:\\Temp");
            mockDirectoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionTestsIfParentExistsIfPathDoesNot()
        {
            // Arrange
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>())).Returns(false);
            mockDirectoryWrapper.Setup(d => d.GetParentPath(It.IsAny<string>())).Returns<string>(s =>
            {
                var parts = s.Split(Path.DirectorySeparatorChar);
                if (parts.Length < 2)
                {
                    return s;
                }

                return String.Join(Path.DirectorySeparatorChar.ToString(), parts.Take(parts.Length - 1).ToArray());
            });

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            ConfigurationManager.AppSettings["ErrorLogPath"] = "D:\\Temp";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "D:\\Temp");
            mockDirectoryWrapper.Verify(d => d.Exists("D:"), Times.Once);
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionTestsAttemptsToCreateFolderIfParentExsits()
        {
            // Arrange
            var exceptionContext = new FakeController().CreateExceptionContext("Index", new Exception());

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();

            var returnsQueue = new ReturnsQueue<bool>(false);
            returnsQueue.Enqueue(false);
            returnsQueue.Enqueue(true);

            mockDirectoryWrapper.Setup(d => d.Exists(It.IsAny<string>())).Returns(() => returnsQueue.GetNext());
            mockDirectoryWrapper.Setup(d => d.GetParentPath(It.IsAny<string>())).Returns<string>(s =>
            {
                var parts = s.Split(Path.DirectorySeparatorChar);
                if (parts.Length < 2)
                {
                    return s;
                }

                return String.Join(Path.DirectorySeparatorChar.ToString(), parts.Take(parts.Length - 1).ToArray());
            });
            mockDirectoryWrapper.Setup(d => d.CreateDirectory(It.IsAny<string>()));

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            ConfigurationManager.AppSettings["ErrorLogPath"] = "D:\\Temp";

            // Act
            customHandle.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(ConfigurationManager.AppSettings["ErrorLogPath"] == "D:\\Temp");
            mockDirectoryWrapper.Verify(d => d.CreateDirectory(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionAjaxRequestReturnsJsonErrorResult()
        {
            // Arrange
            var expectedException = new Exception();
            var exceptionContext = new FakeController().CreateExceptionContext("Index", expectedException, true);

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            // Act
            customHandle.OnException(exceptionContext);
            var result = exceptionContext.Result as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedException.ToString().Trim(), result.Data);
        }



        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionReturnsViewResult()
        {
            // Arrange
            var expectedException = new Exception();
            var exceptionContext = new FakeController().CreateExceptionContext("Index", expectedException);

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            // Act
            customHandle.OnException(exceptionContext);
            var result = exceptionContext.Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void CustomHandleErrorAttribute_OnExceptionReturnsViewResultWhenExceptionIsUnauthorizedAccessException()
        {
            // Arrange
            // Unauthorized Access Exception is not handled by the base error handler.
            // My code Handles it test if it does
            var expectedException = new UnauthorizedAccessException();
            var exceptionContext = new FakeController().CreateExceptionContext("Index", expectedException);

            Mock<IDirectoryWrapper> mockDirectoryWrapper = new Mock<IDirectoryWrapper>();
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.Directory).Returns(mockDirectoryWrapper.Object);
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var customHandle = new CustomHandleErrorAttribute(mockFileSystemHelper.Object);

            // Act
            customHandle.OnException(exceptionContext);
            var result = exceptionContext.Result as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestInitialize]
        public void TestInitialize()
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
