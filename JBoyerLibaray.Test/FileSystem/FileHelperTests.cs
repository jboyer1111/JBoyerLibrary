using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using JBoyerLibaray.Extensions;

namespace JBoyerLibaray.FileSystem
{
    [TestClass]
    public class FileHelperTests
    {
        [TestMethod]
        public void FileHelper_ConstructorNoArg()
        {
            // Arrange

            // Act
            new FileHelper();

            // Assert
        }

        [TestMethod]
        public void FileHelper_ConstructorInjectedFileSystemHelper()
        {
            // Arrange
            var fileSystemHelper = new Mock<IFileSystemHelper>().Object;

            // Act
            new FileHelper(fileSystemHelper);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileHelper_ConstructorThrowsArgNullExceptionIfNull()
        {
            // Arrange

            // Act
            new FileHelper(null);

            // Assert
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsFalseWhenNotTiff()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("NotTiff".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsFalseWhenNotTiff()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("NotTiff".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsFalseWhenNotLongEnough()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("a".ToStream());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
