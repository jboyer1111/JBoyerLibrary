using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.DeckOfCards;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Extensions
{

    [TestClass, ExcludeFromCodeCoverage]
    public class ObjectExtentionsTests
    {

        [TestMethod]
        public void ObjectExtentions_Is_ResturnsExptectedValueTrue()
        {
            // Arrange
            Card card = new Card("Heart", "Ace");

            // Act
            var result = card.Is<Card>();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ObjectExtentions_Is_ResturnsExptectedValueFalse()
        {
            // Arrange
            Card card = new Card("Heart", "Ace");

            // Act
            var result = card.Is<Deck>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ObjectExtentions_IsNot_ResturnsExptectedValueTrue()
        {
            // Arrange
            Card card = new Card("Heart", "Ace");

            // Act
            var result = card.IsNot<Card>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ObjectExtentions_IsNot_ResturnsExptectedValueFalse()
        {
            // Arrange
            Card card = new Card("Heart", "Ace");

            // Act
            var result = card.IsNot<Deck>();

            // Assert
            Assert.IsTrue(result);
        }

    }

}
