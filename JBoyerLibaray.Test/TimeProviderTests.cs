using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{

    [TestClass, ExcludeFromCodeCoverage]
    public class TimeProviderTests
    {

        [TestMethod]
        public void TimeProvider_Constructor()
        {
            // Arrange

            // Act
            new TimeProvider();

            // Assert
        }

        [TestMethod]
        public void TimeProvider_Now()
        {
            // Arrange
            var timeProvider = new TimeProvider();

            DateTime expected = DateTime.Now;

            // Act
            var result = timeProvider.Now;

            // Assert
            // Converting time using TimeZone causes ticks to be different
            // I don't care about this much of a difference
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod]
        public void TimeProvider_Today()
        {
            // Arrange
            var timeProvider = new TimeProvider();

            DateTime expected = DateTime.Today;

            // Act
            var result = timeProvider.Today;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TimeProvider_UtcNow()
        {
            // Arrange
            var timeProvider = new TimeProvider();

            DateTime expected = DateTime.UtcNow;

            // Act
            var result = timeProvider.UtcNow;

            // Assert
            // Converting time using TimeZone causes ticks to be different
            // I don't care about this much of a difference
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TimeProvider_GetTimezoneTimeThrowsArgrumntNullExceptionWhenTimeZoneIsNull()
        {
            // Arrange
            var timeProvider = new TimeProvider();
            TimeZone timeZone = null;

            // Act
            timeProvider.GetTimezoneTime(timeZone);

            // Assert
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TimeProvider_GetTimezoneTimeThrowsArgrumntNullExceptionWhenTimeZoneInfoIsNull()
        {
            // Arrange
            var timeProvider = new TimeProvider();
            TimeZoneInfo timeZoneInfo = null;

            // Act
            timeProvider.GetTimezoneTime(timeZoneInfo);

            // Assert
        }

        [TestMethod]
        public void TimeProvider_GetTimezoneTimeGetsTimeFromTimeZone()
        {
            // Arrange
            var timeProvider = new TimeProvider();
            var expected = DateTime.Now;

            // Act
            var result = timeProvider.GetTimezoneTime(TimeZone.CurrentTimeZone);

            // Assert
            // Converting time using TimeZone causes ticks to be different
            // I don't care about this much of a difference
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod]
        public void TimeProvider_GetTimezoneTimeGetsTimeFromTimeZoneInfo()
        {
            // Arrange
            var timeProvider = new TimeProvider();
            var expected = DateTime.Now;

            // Act
            var result = timeProvider.GetTimezoneTime(TimeZoneInfo.Local);

            // Assert
            // Converting time using TimeZone causes ticks to be different
            // I don't care about this much of a difference
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

    }

}
