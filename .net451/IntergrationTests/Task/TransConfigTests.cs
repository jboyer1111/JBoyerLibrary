using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.FileSystem;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Task
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransConfigTests
    {

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

        private List<string> _tmpFiles = new List<string>();

        [TestMethod]
        public void TransConfig_Construtor()
        {
            // Arrange

            // Act
            new TransConfig();

            // Assert
        }

        [TestMethod]
        public void TransConfig_Execute()
        {
            // Arrange
            var baseDir = Path.Combine(Environment.CurrentDirectory, "Task");
            var transConfig = new TransConfig()
            {
                ConfigPath = Path.Combine(baseDir, "InputXML.xml"),
                TransFormConfig = Path.Combine(baseDir, "Transform.xslt"),
                OutputPath = Path.Combine(baseDir, "OutputXML.xml")
            };

            // Act
            if (!transConfig.Execute())
            {
                Assert.Fail("Failed to transform config file. See test output for more details.");
            }

            var result = File.ReadAllText(Path.Combine(baseDir, "OutputXML.xml"));

            // Assert
            Assert.AreEqual(TRANSFORMEDFILE, result);
        }

        [TestMethod]
        public void TransConfig_ExecuteSavesToConfigPathIfOutputPathEmpty()
        {
            // Arrange
            var baseDir = Path.Combine(Environment.CurrentDirectory, "Task");
            var filePath = Path.Combine(baseDir, "InputOutputXML.xml");

            _tmpFiles.Add(filePath);
            File.Copy(Path.Combine(baseDir, "InputXML.xml"), filePath);

            var transConfig = new TransConfig()
            {
                ConfigPath = filePath,
                TransFormConfig = Path.Combine(baseDir, "Transform.xslt")
            };

            // Act
            if (!transConfig.Execute())
            {
                Assert.Fail("Failed to transform config file. See test output for more details.");
            }

            var result = File.ReadAllText(Path.Combine(baseDir, "InputOutputXML.xml"));

            // Assert
            Assert.AreEqual(TRANSFORMEDFILE, result);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var baseDir = Path.Combine(Environment.CurrentDirectory, "Task");

            if (File.Exists(Path.Combine(baseDir, "OutputXML.xml")))
            {
                try
                {
                    File.Delete(Path.Combine(baseDir, "OutputXML.xml"));
                }
                catch
                {

                }
            }

            foreach (var tmpFile in _tmpFiles)
            {
                if (File.Exists(tmpFile))
                {
                    try
                    {
                        File.Delete(tmpFile);
                    }
                    catch
                    {

                    }
                }
            }

            _tmpFiles.Clear();
        }

    }
}
