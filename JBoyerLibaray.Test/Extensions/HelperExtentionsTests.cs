using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HelperExtentionsTests
    {
        [TestMethod]
        public void HelperExtentions_ToSingleItemList_ReturnsNullWhenNull()
        {
            // Arrange
            string word = null;

            // Act
            var words = word.ToSingleItemList();

            // Arrange
            Assert.IsNull(words);
        }

        [TestMethod]
        public void HelperExtentions_ToSingleItemList_ReturnsItemBackAsAOneItemedList()
        {
            // Arrange
            int number = 0;

            // Act
            var numbers = number.ToSingleItemList();

            // Arrange
            CollectionAssert.AreEqual(new int[] { 0 }, numbers.ToArray());
        }
    }
}
