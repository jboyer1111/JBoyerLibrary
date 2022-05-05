using JBoyerLibaray.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace JBoyerLibaray.Web
{

    [TestClass, ExcludeFromCodeCoverage]
    public class WebDownloaderFileTests
    {

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: contentType")]
        public void WebDownloaderFile_Constructor_ThrowsArgumentExceptionWhenContentTypeIsNull()
        {
            // Arrange

            // Act
            using (new WebDownloaderFile(null, new MemoryStream())) { }

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: contentType")]
        public void WebDownloaderFile_Constructor_ThrowsArgumentExceptionWhenContentTypeIsEmpty()
        {
            // Arrange

            // Act
            using (new WebDownloaderFile("", new MemoryStream())) { }

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: contentType")]
        public void WebDownloaderFile_Constructor_ThrowsArgumentExceptionWhenContentTypeIsWhitespace()
        {
            // Arrange

            // Act
            using (new WebDownloaderFile("\t\r\n", new MemoryStream())) { }

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: stream")]
        public void WebDownloaderFile_Constructor_ThrowsArgumentExceptionWhenStreamIsNull()
        {
            // Arrange

            // Act
            using (new WebDownloaderFile("application/json", null)) { }

            // Assert
        }

        [TestMethod]
        public void WebDownloaderFile_Constructor_ConstructsObject()
        {
            // Arrange

            // Act
            using (new WebDownloaderFile("application/json", new MemoryStream())) { }

            // Assert
        }

        [TestMethod]
        public void WebDownloaderFile_Constructor_DisposesStreamWhenDisposed()
        {
            // Arrange
            var stream = new UTStream();
            
            // Act
            using (new WebDownloaderFile("application/json", stream)) { }

            // Assert
            Assert.AreEqual(1, stream.AttemptedToDisposeCount);
            stream.UnitTestDispose();
        }

    }

}
