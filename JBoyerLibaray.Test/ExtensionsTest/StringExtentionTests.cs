using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Test.ExtensionsTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StringExtentionTests
    {
        [TestMethod]
        public void RepeatMakesAbiggerStringOfTheOrningalRepeatedNTimes()
        {
            // Arrange
            const string expected = "1234123412341234";    

            // Act
            string result = "1234".Repeat(4);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LikeWorksLikeSql_Contains_CS()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%test%");
            bool result2 = text.Like("%Test%");

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void LikeWorksLikeSql_Contains_CI()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%test%", true);
            bool result2 = text.Like("%Test%", true);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void LikeWorksLikeSql_StartsWith_CS()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%");
            bool result2 = text.Like("this%");

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void LikeWorksLikeSql_StartsWith_CI()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%", true);
            bool result2 = text.Like("this%", true);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void LikeWorksLikeSql_EndsWith_CS()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%statement.");
            bool result2 = text.Like("%Statement.");

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void LikeWorksLikeSql_EndsWith_CI()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%statement.", true);
            bool result2 = text.Like("%statement.", true);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(result2);
        }
    }
}
