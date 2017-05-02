using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CharExtentionsTests
    {
        [TestMethod]
        public void CharExtentions_ToUpper()
        {
            // Arrange
            char letter = 'i';
            char result = ' ';

            // Act
            UnitTestHelper.ExecuteInCulture("tr-Tr", () =>
            {
                result = letter.ToUpper();
            });

            // Assert
            Assert.AreEqual('İ', result);
        }

        [TestMethod]
        public void CharExtentions_ToUpperInvariant()
        {
            // Arrange
            char letter = 'i';
            char result = ' ';

            // Act
            UnitTestHelper.ExecuteInCulture("tr-Tr", () =>
            {
                result = letter.ToUpperInvariant();
            });

            // Assert
            Assert.AreEqual('I', result);
        }
    }
}
