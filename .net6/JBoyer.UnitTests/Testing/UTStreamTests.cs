using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace JBoyer.Testing
{

    [TestClass, ExcludeFromCodeCoverage]
    public class UTStreamTests
    {

        private UTStream? _stream = null;

        [TestInitialize]
        public void TestInit()
        {
            _stream = new UTStream();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _stream?.UnitTestDispose();
        }

        [TestMethod]
        public void UTStream_AttemptedToDispose_ReturnsTrueWhenDisposeWasCalled()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }

            // Act
            _stream.Dispose();
            _stream.Dispose();
            _stream.Dispose();

            // Assert
            Assert.IsTrue(_stream.AttemptedToDispose, "AttemptedToDispose was not true.");
        }

        [TestMethod]
        public void UTStream_AttemptedToDispose_using_ReturnsTrueWhenDisposeWasCalled()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }
            _stream.UnitTestDispose();
            _stream = null;

            // Act
            using (_stream = new UTStream())
            {

            }

            // Assert
            Assert.IsTrue(_stream.AttemptedToDispose, "AttemptedToDispose was not true.");
        }

        [TestMethod]
        public void UTStream_AttemptedToDispose_ReturnsFalseWhenDisposeWasNotCalled()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }

            // Act

            // Assert
            Assert.IsFalse(_stream.AttemptedToDispose, "AttemptedToDispose was not false.");
        }

        [TestMethod]
        public void UTStream_AttemptedToDisposeCount_ReturnsTrueWhenDisposeWasCalled()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }

            // Act
            _stream.Dispose();
            _stream.Dispose();
            _stream.Dispose();

            // Assert
            Assert.AreEqual(3, _stream.AttemptedToDisposeCount);
        }

        [TestMethod]
        public void UTStream_AttemptedToDisposeCount_using_ReturnsTrueWhenDisposeWasCalled()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }
            _stream.UnitTestDispose();
            _stream = null;

            // Act
            using (_stream = new UTStream())
            {

            }

            // Assert
            Assert.AreEqual(1, _stream.AttemptedToDisposeCount);
        }

        [TestMethod]
        public void UTStream_AttemptedToDisposeCount_ReturnsFalseWhenDisposeWasNotCalled()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }

            // Act

            // Assert
            Assert.AreEqual(0, _stream.AttemptedToDisposeCount);
        }

        [TestMethod]
        public void UTStream_ReadStreamAsText_ReturnsExpectedText()
        {
            // Arrange
            if (_stream == null)
            {
                Assert.Fail($"{nameof(_stream)} was not setup");
                return;
            }

            using StreamWriter sw = new StreamWriter(_stream, leaveOpen: true) { AutoFlush = true };
            sw.Write("This is a test");
            _stream.Seek(_stream.Position / 2, SeekOrigin.Begin);

            long beforePos = _stream.Position;

            // Act
            string result = _stream.ReadStreamAsText;

            // Assert
            Assert.AreEqual(beforePos, _stream.Position, "Before position does not match after.");
            Assert.AreEqual("This is a test", result);
            Assert.IsFalse(_stream.AttemptedToDispose);
        }

    }

}
