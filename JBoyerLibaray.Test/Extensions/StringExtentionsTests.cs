using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Extensions;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.Exceptions;

namespace JBoyerLibaray.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StringExtentionsTests
    {

        #region Repeat Tests

        [TestMethod]
        public void StringExtentions_RepeatMakesAbiggerStringOfTheOrningalRepeatedNTimes()
        {
            // Arrange
            const string expected = "1234123412341234";    

            // Act
            string result = "1234".Repeat(4);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void StringExtentions_RepeatThrowsErrorWhen1()
        {
            // Arrange

            // Act
            "1234".Repeat(1);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void StringExtentions_RepeatThrowsErrorWhen0()
        {
            // Arrange

            // Act
            "1234".Repeat(0);

            // Assert
        }

        #endregion

        #region Like Tests

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingContainsSyntax()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%test%");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingContainsSyntaxAndStringNotInString()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%apple%");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingContainsSyntaxButDifferentCasing()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%Test%");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingContainsSyntaxAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%test%", true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingContainsSyntaxAndStringNotInStringAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%apple%", true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingContainsSyntaxButDifferentCasingAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%Test%", true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingStartsWithSyntax()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingStartsWithSyntaxAndStringNotBeginOfString()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("Apple%");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingStartsWithSyntaxButDifferntCasing()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("this%");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingStartsWithSyntaxAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%", true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingStartsWithSyntaxAndStringNotBeginOfStringAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("Apple%", true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingStartsWithSyntaxButDifferntCasingAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("this%", true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingEndsWithSyntax()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%statement.");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingEndsWithSyntaxAndStringNotEndOfString()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%apple.");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingEndsWithSyntaxButDifferntCasing()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%Statement.");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingEndsWithSyntaxAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%statement.", true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsFalseWhenUsingEndsWithSyntaxAndStringNotEndOfStringAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%apple.", true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeReturnsTrueWhenUsingEndsWithSyntaxButDifferntCasingAndIgnoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("%Statement.", true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeRandomLikeStringOne()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%statement.");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StringExtentions_LikeRandomLikeStringOneDifferentCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%Statement.");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StringExtentions_LikeRandomLikeStringOneDifferentCaseAndIngoreCase()
        {
            // Arrange
            const string text = "This is a test of the sql 'like' like statement.";

            // Act
            bool result = text.Like("This%Statement.", true);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region CapitalizeEveryWord Tests

        [TestMethod]
        public void StringExtentions_CapitalizeEveryWordCapitalizesWords()
        {
            // Arrange

            // Act
            var result = "test".CapitalizeEveryWord();

            // Assert
            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeEveryWordConvertsToMixedCase()
        {
            // Arrange

            // Act
            var result = "TEST".CapitalizeEveryWord();

            // Assert
            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeEveryWordUsesPucuations()
        {
            // Arrange

            // Act
            var result = "test test;test:test!test?test,test.test_test-test/test&test'test(test\"test\ttest".CapitalizeEveryWord();

            // Assert
            Assert.AreEqual("Test Test;Test:Test!Test?Test,Test.Test_Test-Test/Test&Test'Test(Test\"Test\tTest", result);
        }

        #endregion

        #region CapitalizeFirstChar

        [TestMethod]
        public void StringExtentions_CapitalizeFirstWordCapitalizesWords()
        {
            // Arrange

            // Act
            var result = "test".CapitalizeFirstChar();

            // Assert
            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeFirstWordConvertsToMixedCase()
        {
            // Arrange

            // Act
            var result = "TEST".CapitalizeFirstChar();

            // Assert
            Assert.AreEqual("Test", result);
        }

        #endregion

        #region AddToEndOfFileName

        [TestMethod]
        public void TestMethod()
        {
            // Arrange

            // Act

            // Assert
        }

        #endregion
    }
}
