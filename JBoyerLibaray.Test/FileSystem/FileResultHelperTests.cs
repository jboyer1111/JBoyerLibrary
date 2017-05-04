using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.FileSystem
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FileResultHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileResultHelper_GetMIMETypeThrowsErrorWhenExtentionIsNull()
        {
            // Arrange

            // Act
            FileResultHelper.GetMimeType(null);

            // Assert
        }

        [TestMethod]
        public void FileResultHelper_GetMIMETypeReturnsMIMEType()
        {
            // Arrange

            // Act
            var result = FileResultHelper.GetMimeType(".txt");

            // Assert
            Assert.AreEqual("text/plain", result);
        }

        [TestMethod]
        public void FileResultHelper_GetMIMETypeDoesNotCareIfArgHasPeriod()
        {
            // Arrange

            // Act
            var result = FileResultHelper.GetMimeType("txt");

            // Assert
            Assert.AreEqual("text/plain", result);
        }

        [TestMethod]
        public void FileResultHelper_GetMIMETypeIfHelperDoesNotKnowExtentionWillReturnDefaultValue()
        {
            // Arrange

            // Act
            var result = FileResultHelper.GetMimeType(".zelda");

            // Assert
            Assert.AreEqual("application/octet-stream", result);
        }
    }
}
