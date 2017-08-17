using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using JBoyerLibaray.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.FileSystem
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
        public void FileHelper_IsTiffStreamReturnsFalseWhenNotLongEnough()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("I".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsFalseWhenFirstTwoBytesNotTheSame()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("IM*\0aaaa".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsFalseWhenFirstTwoBytesAreTheSameButNotOneOfExpectedValues()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("aa*\0aaaa".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsFalseWhenFirstTwoBytesAreTheIntelMarkButMagicNumberIsWrong()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("II**aaaa".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsFalseWhenFirstTwoBytesAreTheMotorolaMarkButMagicNumberIsWrong()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("MM**aaaa".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsTrueWhenFirstTwoBytesAreTheIntelMark()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            // Mark - II
            // Magic Number -  *\0
            var result = fileHelper.IsTiff("II*\0aaaa".ToStream());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffStreamReturnsTrueWhenFirstTwoBytesAreTheMotorolaMark()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            // Mark - MM
            // Magic Number -  \0*
            var result = fileHelper.IsTiff("MM\0*aaaa".ToStream());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsFalseWhenNotLongEnough()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("I".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsFalseWhenFirstTwoBytesNotTheSame()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("IM*\0aaaa".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsFalseWhenFirstTwoBytesAreTheSameButNotOneOfExpectedValues()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("aa*\0aaaa".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsFalseWhenFirstTwoBytesAreTheIntelMarkButMagicNumberIsWrong()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("II**aaaa".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsFalseWhenFirstTwoBytesAreTheMotorolaMarkButMagicNumberIsWrong()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("MM**aaaa".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsTrueWhenFirstTwoBytesAreTheIntelMark()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            // Mark - II
            // Magic Number -  *\0
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("II*\0aaaa".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FileHelper_IsTiffFilePathReturnsTrueWhenFirstTwoBytesAreTheMotorolaMark()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            // Mark - MM
            // Magic Number -  \0*
            fileWrapper.Setup(f => f.Open("T", FileMode.Open, FileAccess.Read)).Returns("MM\0*aaaa".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsTiff("T");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FileHelper_IsPDFStreamReturnsFalseWhenNotPDF()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsPDF("NotPDF".ToStream());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsPDFStreamReturnsFalseWhenIsPDF()
        {
            // Arrange
            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsPDF("%PDF".ToStream());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FileHelper_IsPDFFilePathReturnsFalseWhenNotPDF()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("P", FileMode.Open, FileAccess.Read)).Returns("NotPDF".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsPDF("P");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHelper_IsPDFFilePathReturnsTrueWhenPDF()
        {
            // Arrange
            var fileWrapper = new Mock<IFileWrapper>();
            fileWrapper.Setup(f => f.Open("P", FileMode.Open, FileAccess.Read)).Returns("%PDF".ToStream());

            var mockFileSystemHelper = new Mock<IFileSystemHelper>();
            mockFileSystemHelper.Setup(f => f.File).Returns(fileWrapper.Object);
            var fileHelper = new FileHelper(mockFileSystemHelper.Object);

            // Act
            var result = fileHelper.IsPDF("P");

            // Assert
            Assert.IsTrue(result);
        }
    }
}
