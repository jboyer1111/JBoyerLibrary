using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NumberExtentionsTests
    {
        #region Private Variables

        private const int aSecond =  1000;
        private const int aMinute = 60000;
        private const int aHour = 3600000;
        private const int aDay = 86400000;

        #endregion

        #region ToExcelColumn Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberExtentions_ToExcelColumnNameThrowsErrorWhenLessThanZero()
        {
            // Arrange
            int number = -1;

            // Act
            number.ToExcelColumnName();

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberExtentions_ToExcelColumnNameThrowsErrorWhenZero()
        {
            // Arrange
            int number = 0;

            // Act
            number.ToExcelColumnName();

            // Assert
        }


        [TestMethod]
        public void NumberExtentions_ToExcelColumnNameOne()
        {
            // Arrange
            int number = 1;

            // Act
            var result = number.ToExcelColumnName();

            // Assert
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void NumberExtentions_ToExcelColumnNameTwientySix()
        {
            // Arrange
            int number = 26;

            // Act
            var result = number.ToExcelColumnName();

            // Assert
            Assert.AreEqual("Z", result);
        }

        [TestMethod]
        public void NumberExtentions_ToExcelColumnNameTwientySeven()
        {
            // Arrange
            int number = 27;

            // Act
            var result = number.ToExcelColumnName();

            // Assert
            Assert.AreEqual("AA", result);
        }

        [TestMethod]
        public void NumberExtentions_ToExcelColumnNameAZ()
        {
            // Arrange
            int number = 26 * 2;

            // Act
            var result = number.ToExcelColumnName();

            // Assert
            Assert.AreEqual("AZ", result);
        }

        [TestMethod]
        public void NumberExtentions_ToExcelColumnNameAAA()
        {
            // Arrange
            int number = (27 * 26) + 1;

            // Act
            var result = number.ToExcelColumnName();

            // Assert
            Assert.AreEqual("AAA", result);
        }

        #endregion

        #region ToRomainNumeral Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumberExtentions_ToRomanNumeralThrowsErrorIfLessThanZero()
        {
            // Arrange
            int number = -1;

            // Act
            number.ToRomanNumeral();

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumberExtentions_ToRomanNumeralThrowsErrorIfGreaterThan3999()
        {
            // Arrange
            int number = 4000;

            // Act
            number.ToRomanNumeral();

            // Assert
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral0()
        {
            // Arrange
            int number = 0;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("N", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral1()
        {
            // Arrange
            int number = 1;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("I", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral4()
        {
            // Arrange
            int number = 4;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("IV", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral5()
        {
            // Arrange
            int number = 5;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("V", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral9()
        {
            // Arrange
            int number = 9;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("IX", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral10()
        {
            // Arrange
            int number = 10;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("X", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral40()
        {
            // Arrange
            int number = 40;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("XL", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral50()
        {
            // Arrange
            int number = 50;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("L", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral90()
        {
            // Arrange
            int number = 90;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("XC", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral100()
        {
            // Arrange
            int number = 100;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("C", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral400()
        {
            // Arrange
            int number = 400;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("CD", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral500()
        {
            // Arrange
            int number = 500;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("D", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral900()
        {
            // Arrange
            int number = 900;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("CM", result);
        }

        [TestMethod]
        public void NumberExtentions_ToRomanNumeral1000()
        {
            // Arrange
            int number = 1000;

            // Act
            var result = number.ToRomanNumeral();

            // Assert
            Assert.AreEqual("M", result);
        }

        #endregion

        #region TimeInMilliseconsToHumanReadableString

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString0Sec()
        {
            // Arrange
            int number = 0;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("0 seconds", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString999MilliSec()
        {
            // Arrange
            int number = 999;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("0.999 seconds", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Sec()
        {
            // Arrange
            int number = aSecond;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 second", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString59Sec()
        {
            // Arrange
            int number = aMinute - aSecond;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("59 seconds", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Min()
        {
            // Arrange
            int number = aMinute;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 minute", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Min3Sec()
        {
            // Arrange
            int number = aMinute + (3 * aSecond);

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 minute and 3 seconds", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString59Min()
        {
            // Arrange
            int number = aHour - aMinute;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("59 minutes", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Hour()
        {
            // Arrange
            int number = aHour;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 hour", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Hour6Min()
        {
            // Arrange
            int number = aHour + (6 * aMinute);

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 hour and 6 minutes", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Hour6Sec()
        {
            // Arrange
            int number = aHour + (6 * aSecond);

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 hour and 6 seconds", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Hour39Min6Sec()
        {
            // Arrange
            int number = aHour + (39 * aMinute) + (6 * aSecond);

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 hour, 39 minutes, and 6 seconds", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString23Hour()
        {
            // Arrange
            int number = aDay - aHour;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("23 hours", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString1Day()
        {
            // Arrange
            int number = aDay;

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 day", result);
        }

        [TestMethod]
        public void NumberExtentions_TimeInMillisecondsToHumanReadableString5Days6Min()
        {
            // Arrange
            int number = aDay + (6 * aMinute);

            // Act
            var result = number.TimeInMillisecondsToHumanReadableString();

            // Assert
            Assert.AreEqual("1 day and 6 minutes", result);
        }

        #endregion
    }
}
