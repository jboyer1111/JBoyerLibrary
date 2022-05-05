using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.FileSystem;
using Moq;
using System.Security.Principal;
using JBoyerLibaray.UnitTests;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ErrorLogTests
    {
        [TestMethod]
        public void ErrorLog_Constructor_One()
        {
            // Arrange

            // Act
            new ErrorLog("D:\\Temp\\Somefile.txt");

            // Assert
        }

        [TestMethod]
        public void ErrorLog_Constructor_Two()
        {
            // Arrange
            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();

            // Act
            new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorLog_Constructor_ThrowsErrorWhenPathIsNull()
        {
            // Arrange

            // Act
            new ErrorLog(null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorLog_Constructor_ThrowsErrorWhenPathIsEmpty()
        {
            // Arrange

            // Act
            new ErrorLog("");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorLog_Constructor_ThrowsErrorWhenPathIsWhiteSpace()
        {
            // Arrange

            // Act
            new ErrorLog("   ");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErrorLog_Constructor_ThrowsErrorIfFileSystemHelperIsNull()
        {
            // Arrange
            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();

            // Act
            new ErrorLog("D:\\Temp\\Somefile.txt", null, mockTimeProvider.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErrorLog_Constructor_ThrowsErrorIfTimeProviderIsNull()
        {
            // Arrange
            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();

            // Act
            new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, null);

            // Assert
        }

        [TestMethod]
        public void ErrorLog_Write_DoesNothingIfExceptionIsNull()
        {
            // Arrange
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()));

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            var fakeUser = new FakeUser("Bob");

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            Exception exception = null;

            // Act
            errorLog.Write(fakeUser, exception);

            // Assert
            mockFileWrapper.Verify(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ErrorLog_Write_DoesNothingIfMessageIsNull()
        {
            // Arrange
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()));

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            var fakeUser = new FakeUser("Bob");

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = null;

            // Act
            errorLog.Write(fakeUser, message);

            // Assert
            mockFileWrapper.Verify(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ErrorLog_Write_DoesNothingIfMessageIsEmpty()
        {
            // Arrange
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()));

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            var fakeUser = new FakeUser("Bob");

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "";

            // Act
            errorLog.Write(fakeUser, message);

            // Assert
            mockFileWrapper.Verify(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ErrorLog_Write_DoesNothingIfMessageIsWhitespace()
        {
            // Arrange
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()));

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            var fakeUser = new FakeUser("Bob");

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "   ";

            // Act
            errorLog.Write(fakeUser, message);

            // Assert
            mockFileWrapper.Verify(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ErrorLog_Write_OutputsMessageToFile()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            var fakeUser = new FakeUser("Bob");

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(fakeUser, message);

            // Assert
            Assert.AreEqual(
                String.Format(
                    "01-01-0001 12:00:00 AM CST: Bob:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine                    
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_SetsUsernameToUnknownIfUserIsNull()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(null, message);

            // Assert
            Assert.AreEqual(
                String.Format(
                    "01-01-0001 12:00:00 AM CST: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_SetsUsernameToUnknownIfUserIndentiyPropIsNull()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            Mock<IPrincipal> mockPrincipal = new Mock<IPrincipal>();

            // Act
            errorLog.Write(mockPrincipal.Object, message);

            // Assert
            Assert.AreEqual(
                String.Format(
                    "01-01-0001 12:00:00 AM CST: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_SetsUsernameToUnknownIfNotAuthenicated()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            var fakeUser = FakeUser.Anonymous();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(fakeUser, message);

            // Unknown
            Assert.AreEqual(
                String.Format(
                    "01-01-0001 12:00:00 AM CST: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_PutsCorrectTimezoneAbbrevForDaylightTime()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(t => t.Now).Returns(new DateTime(2017, 4, 12, 6, 30, 0));

            var fakeUser = FakeUser.Anonymous();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(fakeUser, message);

            // Unknown
            Assert.AreEqual(
                String.Format(
                    "04-12-2017 06:30:00 AM CDT: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_PutsCorrectTimezoneAbbrevForStandardTime()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(t => t.Now).Returns(new DateTime(2017, 1, 1, 6, 30, 0));

            var fakeUser = FakeUser.Anonymous();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(fakeUser, message);

            // Unknown
            Assert.AreEqual(
                String.Format(
                    "01-01-2017 06:30:00 AM CST: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_PutsAmForMornings()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(t => t.Now).Returns(new DateTime(2017, 1, 1, 6, 30, 0));

            var fakeUser = FakeUser.Anonymous();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(fakeUser, message);

            // Unknown
            Assert.AreEqual(
                String.Format(
                    "01-01-2017 06:30:00 AM CST: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }

        [TestMethod]
        public void ErrorLog_Write_PutsPmForAfternoons()
        {
            // Arrange
            string result = null;

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.AppendAllText(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((p, c) =>
            {
                result += c;
            });

            Mock<IFileSystemHelper> mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            Mock<ITimeProvider> mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(t => t.Now).Returns(new DateTime(2017, 1, 1, 13, 30, 0));

            var fakeUser = FakeUser.Anonymous();

            var errorLog = new ErrorLog("D:\\Temp\\Somefile.txt", mockFileSystemHelper.Object, mockTimeProvider.Object);

            string message = "Some Error Text Here";

            // Act
            errorLog.Write(fakeUser, message);

            // Unknown
            Assert.AreEqual(
                String.Format(
                    "01-01-2017 01:30:00 PM CST: Unknown:{0}Some Error Text Here{0}{0}",
                    Environment.NewLine
                ),
                result
            );
        }
    }
}
