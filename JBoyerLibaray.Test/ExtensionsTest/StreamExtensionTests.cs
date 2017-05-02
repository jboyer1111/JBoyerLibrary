using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JBoyerLibaray.Extensions;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Test.ExtensionsTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StreamExtensionTests
    {
        [TestMethod]
        public void SeekTest()
        {
            using (MemoryStream streamOne = new MemoryStream())
            using (MemoryStream streamTwo = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(streamOne) { AutoFlush = true })
            {
                streamWriter.WriteLine("Test");
                streamOne.Seek(0, SeekOrigin.Begin);
                streamOne.CopyTo(streamTwo);
                streamOne.Seek(0, SeekOrigin.Begin);
                streamTwo.Seek(0);

                Assert.AreEqual(streamOne.Position, streamTwo.Position);
            }
        }
    }
}
