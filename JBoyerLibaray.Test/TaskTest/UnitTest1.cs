using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Task;
using System.IO;

namespace JBoyerLibaray.Test.TaskTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string TransformTestOuputPath = @"D:\MyLibrary\JBoyerLibaray.Test\TaskTest\InputXML2.xml"; 

        [TestCleanup]
        public void CleanUpFiles()
        {
            if (File.Exists(TransformTestOuputPath))
                File.Delete(TransformTestOuputPath);
        }

        [TestMethod]
        public void TasksAcuallyTransfromsConfigFile()
        {
            //Arrange
            TransConfig trans = new TransConfig()
            { 
                ConfigPath = @"D:\MyLibrary\JBoyerLibaray.Test\TaskTest\InputXML.xml",
                TransFormConfig = @"D:\MyLibrary\JBoyerLibaray.Test\TaskTest\Transfrom.xslt",
                OutputPath = TransformTestOuputPath
            };
            string expected = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>{0}<configuration>{0}  <startup> {0}    <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5\" />{0}  </startup>{0}  <appSettings>{0}    <add key=\"HasTransformed\" value=\"true\"/>{0}  </appSettings>{0}</configuration>", Environment.NewLine);

            //Act
            trans.Execute();

            //Assert
            var result = File.ReadAllText(TransformTestOuputPath);
            Assert.AreEqual(expected, result);
        }
    }
}
