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
        #region AddToEndOfFileName

        [TestMethod]
        public void StringExtentions_AddToEndOfFileNameAddsTextToEndOfFileName()
        {
            // Arrange
            string expected = "TestOne.txt";

            // Act
            string result = "Test.txt".AddToEndOfFilename("One");

            // Assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringExtentions_AddToEndOfFileNameThrowsArugmentNullExceptionWhenFilenameIsNull()
        {
            // Arrange
            string filename = null;

            // Act
            filename.AddToEndOfFilename("Test");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringExtentions_AddToEndOfFileNameThrowsArugmentNullExceptionWhenFilenameIsEmpty()
        {
            // Arrange

            // Act
            "".AddToEndOfFilename("Test");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringExtentions_AddToEndOfFileNameThrowsArugmentNullExceptionWhenFilenameIsWhitespace()
        {
            // Arrange

            // Act
            "\r\t\n".AddToEndOfFilename("Test");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringExtentions_AddToEndOfFileNameThrowsArugmentNullExceptionWhenAddToEndIsNull()
        {
            // Arrange
            string filename = "Test.txt";

            // Act
            filename.AddToEndOfFilename(null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringExtentions_AddToEndOfFileNameThrowsArugmentNullExceptionWhenAddToEndIsEmpty()
        {
            // Arrange
            string filename = "Test.txt";

            // Act
            filename.AddToEndOfFilename("");

            // Assert
        }

        #endregion

        #region CapitalizeEveryWord

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

        [TestMethod]
        public void StringExtentions_CapitalizeEveryWordNullReturnsNull()
        {
            // Arrange
            string test = null;

            // Act
            var result = test.CapitalizeEveryWord();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeEveryWordEmptyReturnsEmpty()
        {
            // Arrange
            string test = "";

            // Act
            var result = test.CapitalizeEveryWord();

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeEveryWordWhitespaceReturnsSameWhitespace()
        {
            // Arrange
            string test = "\r\t\n";

            // Act
            var result = test.CapitalizeEveryWord();

            // Assert
            Assert.AreEqual("\r\t\n", result);
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

        [TestMethod]
        public void StringExtentions_CapitalizeFirstWordSingeChar()
        {
            // Arrange

            // Act
            var result = "t".CapitalizeFirstChar();

            // Assert
            Assert.AreEqual("T", result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeFirstWordNullReturnsNull()
        {
            // Arrange
            string word = null;

            // Act
            var result = word.CapitalizeFirstChar();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeFirstWordEmptyReturnsEmpty()
        {
            // Arrange
            string word = "";

            // Act
            var result = word.CapitalizeFirstChar();

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void StringExtentions_CapitalizeFirstWordWhitespaceReturnsTheWhitespace()
        {
            // Arrange
            string word = "\r\t\n";

            // Act
            var result = word.CapitalizeFirstChar();

            // Assert
            Assert.AreEqual("\r\t\n", result);
        }

        #endregion

        #region Contains

        [TestMethod]
        public void StringExtentions_ContainsUsesPassedComparer()
        {
            // Arrange

            // Act
            var result = "The quick brown fox jumped over the lazy dog.".Contains("FOX", StringComparison.CurrentCultureIgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region Like

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

        #region Repeat

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

        #region SplitCamelCase

        [TestMethod]
        public void StringExtentions_SplitCamelCaseNull()
        {
            // Arrange
            string word = null;

            // Act
            var result = word.SplitCamelCase();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StringExtentions_SplitCamelCaseEmpty()
        {
            // Arrange
            string word = "";

            // Act
            var result = word.SplitCamelCase();

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void StringExtentions_SplitCamelCaseWhitesapce()
        {
            // Arrange
            string word = "\t\r\n";

            // Act
            var result = word.SplitCamelCase();

            // Assert
            Assert.AreEqual("\t\r\n", result);
        }

        [TestMethod]
        public void StringExtentions_SplitCamelCaseWordOne()
        {
            // Arrange
            string word = "Nothing";

            // Act
            var result = word.SplitCamelCase();

            // Assert
            Assert.AreEqual("Nothing", result);
        }

        [TestMethod]
        public void StringExtentions_SplitCamelCaseWordOneHashTag()
        {
            // Arrange
            string word = "Nothing";

            // Act
            var result = word.SplitCamelCase('#');

            // Assert
            Assert.AreEqual("Nothing", result);
        }

        [TestMethod]
        public void StringExtentions_SplitCamelCaseWordTwo()
        {
            // Arrange
            string word = "NothingTwo";

            // Act
            var result = word.SplitCamelCase();

            // Assert
            Assert.AreEqual("Nothing Two", result);
        }

        [TestMethod]
        public void StringExtentions_SplitCamelCaseWordTwoHashTag()
        {
            // Arrange
            string word = "NothingTwo";

            // Act
            var result = word.SplitCamelCase('#');

            // Assert
            Assert.AreEqual("Nothing#Two", result);
        }

        #endregion

    }
}
