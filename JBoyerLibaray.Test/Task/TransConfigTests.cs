using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Task;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Moq;
using JBoyerLibaray.FileSystem;
using JBoyerLibaray.Extensions;
using JBoyerLibaray.UnitTests;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace JBoyerLibaray.Task
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransConfigTests
    {
        #region Config File

        private readonly string CONFIGFILE = String.Join(
            Environment.NewLine,
            new string[]
            {
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                "<configuration>",
                "  <appSettings>",
                "    <add key=\"HasTransformed\" value=\"false\" />",
                "  </appSettings>",
                "  <startup>",
                "    <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5\" />",
                "  </startup>",
                "</configuration>"
            }
        );

        #endregion

        #region Trans File

        private readonly string TRANSFILE = String.Join(
            Environment.NewLine,
            new string[]
            {
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                "<configuration xmlns:xdt=\"http://schemas.microsoft.com/XML-Document-Transform\">",
                "  <appSettings>",
                "    <add key=\"HasTransformed\" value=\"true\" xdt:Transform=\"Replace\" xdt:Locator=\"Match(key)\"/>",
                "  </appSettings>",
                "</configuration>"
            }
        );

        #endregion

        #region TransFormed File

        private readonly string TRANSFORMEDFILE = String.Join(
            Environment.NewLine,
            new string[]
            {
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                "<configuration>",
                "  <appSettings>",
                "    <add key=\"HasTransformed\" value=\"true\"/>",
                "  </appSettings>",
                "  <startup>",
                "    <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5\"/>",
                "  </startup>",
                "</configuration>"
            }
        );

        #endregion

        private List<IUnitTestDisposable> _cleanup = new List<IUnitTestDisposable>(10);

        [TestMethod]
        public void TransConfig_ConstructorNoArg()
        {
            //Arrange

            //Act
            new TransConfig();

            //Assert

        }

        [TestMethod]
        public void TransConfig_ConstructorInjectedFileSystemHelper()
        {
            //Arrange
            Mock<IFileSystemHelper> _fileSystemHelper = new Mock<IFileSystemHelper>();

            //Act
            new TransConfig(_fileSystemHelper.Object);

            //Assert
        }

        [TestMethod]
        public void TransConfig_Execute()
        {
            // Arrange
            var stream = new UnitTestStream();

            // This is to "Dispose" the Stream. UnitTestStream need to call UnitTestDispose to Dispose It
            _cleanup.Add(stream);

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.OpenRead("C")).Returns(CONFIGFILE.ToStream());
            mockFileWrapper.Setup(f => f.OpenRead("T")).Returns(TRANSFILE.ToStream());
            mockFileWrapper.Setup(f => f.Open("O", FileMode.Create)).Returns(stream);

            Mock<IFileSystemHelper> _fileSystemHelper = new Mock<IFileSystemHelper>();
            _fileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var transConfig = new TransConfig(_fileSystemHelper.Object)
            {
                ConfigPath = "C",
                TransFormConfig = "T",
                OutputPath = "O"
            };

            // Act
            if (!transConfig.Execute())
            {
                Assert.Fail("Failed to transform config file. See test output for more details.");
            }

            string result = stream.ReadStreamAsText;

            // Assert
            Assert.AreEqual(TRANSFORMEDFILE, result);
        }

        [TestMethod]
        public void TransConfig_ExecuteSavesToConfigPathIfOutputPathEmpty()
        {
            // Arrange
            var stream = new UnitTestStream();

            // This is to "Dispose" the Stream. UnitTestStream need to call UnitTestDispose to Dispose It
            _cleanup.Add(stream);

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.OpenRead("C")).Returns(CONFIGFILE.ToStream());
            mockFileWrapper.Setup(f => f.OpenRead("T")).Returns(TRANSFILE.ToStream());
            mockFileWrapper.Setup(f => f.Open("C", FileMode.Create)).Returns(stream);

            Mock<IFileSystemHelper> _fileSystemHelper = new Mock<IFileSystemHelper>();
            _fileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var transConfig = new TransConfig(_fileSystemHelper.Object)
            {
                ConfigPath = "C",
                TransFormConfig = "T"
            };

            // Act
            if (!transConfig.Execute())
            {
                Assert.Fail("Failed to transform config file. See test output for more details.");
            }

            string result = stream.ReadStreamAsText;

            // Assert
            Assert.AreEqual(TRANSFORMEDFILE, result);
        }

        [TestMethod]
        public void TransConfig_BuildEngineGetSetTest()
        {
            // Arrange
            Mock<IFileSystemHelper> _fileSystemHelper = new Mock<IFileSystemHelper>();
            var transConfig = new TransConfig(_fileSystemHelper.Object);

            var excpectedValue = new Mock<IBuildEngine>().Object;

            // Act
            transConfig.BuildEngine = excpectedValue;

            // Assert
            Assert.AreEqual(excpectedValue, transConfig.BuildEngine);
        }

        [TestMethod]
        public void TransConfig_HostObjectGetSetTest()
        {
            // Arrange
            Mock<IFileSystemHelper> _fileSystemHelper = new Mock<IFileSystemHelper>();
            var transConfig = new TransConfig(_fileSystemHelper.Object);

            var excpectedValue = new Mock<ITaskHost>().Object;

            // Act
            transConfig.HostObject = excpectedValue;

            // Assert
            Assert.AreEqual(excpectedValue, transConfig.HostObject);
        }


        [TestMethod]
        public void TransConfig_ExecuteReturnsFalseIfExceptionOccured()
        {
            // Arrange
            var stream = new UnitTestStream();

            // This is to "Dispose" the Stream. UnitTestStream need to call UnitTestDispose to Dispose It
            _cleanup.Add(stream);

            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup(f => f.OpenRead(It.IsAny<string>())).Throws(new Exception());

            Mock<IFileSystemHelper> _fileSystemHelper = new Mock<IFileSystemHelper>();
            _fileSystemHelper.Setup(f => f.File).Returns(mockFileWrapper.Object);

            var transConfig = new TransConfig(_fileSystemHelper.Object)
            {
                ConfigPath = "C",
                TransFormConfig = "T",
                OutputPath = "O"
            };

            // Act
            var result = transConfig.Execute();

            // Assert
            Assert.IsFalse(result);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            foreach (var unitTestCleanupItem in _cleanup)
            {
                if (unitTestCleanupItem != null)
                {
                    unitTestCleanupItem.UnitTestDispose();
                }
            }

            _cleanup.Clear();
        }
    }
}
