using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StreamExtensionsTests
    {
        [TestMethod]
        public void StreamExtensions_SeekDoesSeekFromBegining()
        {
            // Arrange
            using (MemoryStream streamOne = new MemoryStream())
            using (MemoryStream streamTwo = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(streamOne) { AutoFlush = true })
            {
                // Act
                // -- Add Text to Stream One
                streamWriter.WriteLine("Test");
                
                // -- Copy contents form Stream one to Stream two
                streamOne.Seek(0, SeekOrigin.Begin);
                streamOne.CopyTo(streamTwo);

                // -- Use both Seek Variations to move both streams to begining
                streamOne.Seek(0, SeekOrigin.Begin);
                streamTwo.Seek(0);

                // Assert
                Assert.AreEqual(streamOne.Position, streamTwo.Position);
            }
        }
    }
}
