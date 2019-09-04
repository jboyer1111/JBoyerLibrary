using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Extensions
{

    [TestClass, ExcludeFromCodeCoverage]
    public class CharExtentionsTests
    {

        [TestMethod]
        public void CharExtentions_ToUpper()
        {
            // Arrange
            char letter = 'i';
            char result = ' ';

            // Act
            CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
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
            CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
            {
                result = letter.ToUpperInvariant();
            });

            // Assert
            Assert.AreEqual('I', result);
        }

        [TestMethod]
        public void CharExtentions_ToLower()
        {
            // Arrange
            char letter = 'İ';
            char result = ' ';

            // Act
            CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
            {
                result = letter.ToLower();
            });

            // Assert
            Assert.AreEqual('i', result);
        }

        [TestMethod]
        public void CharExtentions_ToLowerInvariant()
        {
            // Arrange
            char letter = 'İ';
            char result = ' ';

            // Act
            CultureInfoHelper.ExecuteInCulture("tr-Tr", () =>
            {
                result = letter.ToLowerInvariant();
            });

            // Assert
            Assert.AreEqual('İ', result);
        }

    }

}
