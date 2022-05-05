using JBoyerLibaray.HelperClasses;
using JBoyerLibaray.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JBoyerLibaray.Web
{

    [TestClass, ExcludeFromCodeCoverage]
    public class WebPageCacheTests
    {

        private string _fullCacheXml = null;
        private Dictionary<string, string> _boop = new Dictionary<string, string>();

        public WebPageCacheTests()
        {
            string pageTemplate = "<Page><URL>{0}</URL><FileName>{1}</FileName><CacheDate>{2:yyyy-MM-ddTHH:mm:ss.fffffffK}</CacheDate></Page>";
            string pageTemplateExpire = "<Page><URL>{0}</URL><FileName>{1}</FileName><CacheDate>{2:yyyy-MM-ddTHH:mm:ss.fffffffK}</CacheDate><ExperationDate>{3:yyyy-MM-ddTHH:mm:ss.fffffffK}</ExperationDate></Page>";

            //"<Pages><Page><URL>https://www.google.com/</URL><FileName>GoogleFile</FileName><CacheDate>2022-01-19T13:46:28.3504068-06:00</CacheDate></Page></Pages>";

            StringBuilder sb = new StringBuilder();
            sb.Append("<Pages>");

            sb.AppendFormat(pageTemplate, "https://www.google.com/", "GoogleFile", DateTime.Now);
            sb.AppendFormat(pageTemplateExpire, "https://www.youtube.com/", "YoutubeFile", DateTime.Now, DateTime.Now.AddDays(-1));
            sb.AppendFormat(pageTemplate, "", "GoogleFile", DateTime.Now);
            sb.AppendFormat(pageTemplate, "https://www.billy.com/", "", DateTime.Now);
            sb.AppendFormat(pageTemplate, "https://www.bob.com/", "BobFile", null);
            sb.AppendFormat(pageTemplateExpire, "https://www.milly.com/", "MillyFile", DateTime.Now, DateTime.Now.AddDays(50));


            sb.Append("</Pages>");
            _fullCacheXml = sb.ToString();
        }

        #region Constructor Tests

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: cacheFolderPath")]
        public void WebPageCache_Consturctor_ThrowsArugmentNullExceptionWhenPathIsNull()
        {
            // Arrange

            // Act
            new WebPageCache(null, FSHSetupInfo.Default);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: cacheFolderPath")]
        public void WebPageCache_Consturctor_ThrowsArugmentNullExceptionWhenPathIsEmpty()
        {
            // Arrange

            // Act
            new WebPageCache("", FSHSetupInfo.Default);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: cacheFolderPath")]
        public void WebPageCache_Consturctor_ThrowsArugmentNullExceptionWhenPathIsWhiteSpace()
        {
            // Arrange

            // Act
            new WebPageCache("\t\r\n", FSHSetupInfo.Default);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: fileSystemHandler")]
        public void WebPageCache_Consturctor_ThrowsArugmentNullExceptionWhenFileSystemHelperIsNull()
        {
            // Arrange

            // Act
            new WebPageCache("Some path", null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Unable to create directory")]
        public void WebPageCache_Consturctor_ThrowsExceptionCannotCreateMissingDirectory()
        {
            // Arrange
            var fshInfo = new FSHSetupInfo();
            fshInfo.MockDirectory.Setup(d => d.CreateDirectory(It.IsAny<string>())).Throws(new Exception("Bar"));

            // Act
            new WebPageCache("Some path", fshInfo.FileSystemHelper);

            // Assert
        }

        [TestMethod]
        public void WebPageCache_Consturctor_CacheFileDoesNotExist()
        {
            // Arrange

            // Act
            var cache = new WebPageCache("Some path", FSHSetupInfo.Default);

            // Assert
            Assert.AreEqual(0, cache.Count);
        }

        [TestMethod]
        public void WebPageCache_Consturctor_CacheFileDoesExist()
        {
            // Arrange
            var fshInfo = new FSHSetupInfo();
            fshInfo.MockFile.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            fshInfo.MockFile.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(_fullCacheXml);

            // Act
            var cache = new WebPageCache("Some path", fshInfo.FileSystemHelper);

            // Assert
            Assert.AreEqual(1, cache.Count);
        }

        #endregion


    }

}
