using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    public class CardValueThenSuitComparerTests
    {

        [TestMethod]
        public void CardValueThenSuitComparer_XAndYAreNull()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;

            // Act
            var result = comparer.Compare(null, null);

            // Assert
            Assert.AreEqual(0, result);
        }

        
        [TestMethod]
        public void CardValueThenSuitComparer_XIsNullAndYIsNot()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card yCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(null, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_YIsNullAndXIsNot()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(xCard, null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_XAndYAreEqual()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("Spades", "Ace");
            Card yCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_SuitXComesBeforeSuitY()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("A", "Ace");
            Card yCard = new Card("B", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_SuitYComesBeforeSuitX()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("B", "Ace");
            Card yCard = new Card("A", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_ValueXComesBeforeValueY()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("Spades", "Ace");
            Card yCard = new Card("Spades", "2");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_ValueYComesBeforeValueX()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("Spades", "2");
            Card yCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CardValueThenSuitComparer_AceIsHighAffectsSort()
        {
            // Arrange
            var comparer = CardComparer.ValueThenSuit;
            Card xCard = new Card("Spades", "Ace");
            Card yCard = new Card("Spades", "Ace");
            yCard.SetAceIsHighSetting(true);

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }
    }
}
