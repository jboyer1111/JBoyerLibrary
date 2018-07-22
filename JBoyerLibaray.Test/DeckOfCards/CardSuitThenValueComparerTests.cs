using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CardSuitThenValueComparerTests
    {

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_XAndYAreNull()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;

            // Act
            var result = comparer.Compare(null, null);

            // Assert
            Assert.AreEqual(0, result);
        }

        
        [TestMethod]
        public void CardSuitThenValueComparer_Compare_XIsNullAndYIsNot()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card yCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(null, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_YIsNullAndXIsNot()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card xCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(xCard, null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_XAndYAreEqual()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card xCard = new Card("Spades", "Ace");
            Card yCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_SuitXComesBeforeSuitY()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card xCard = new Card("A", "Ace");
            Card yCard = new Card("B", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_SuitYComesBeforeSuitX()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card xCard = new Card("B", "Ace");
            Card yCard = new Card("A", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_ValueXComesBeforeValueY()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card xCard = new Card("Spades", "Ace");
            Card yCard = new Card("Spades", "2");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_ValueYComesBeforeValueX()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
            Card xCard = new Card("Spades", "2");
            Card yCard = new Card("Spades", "Ace");

            // Act
            var result = comparer.Compare(xCard, yCard);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CardSuitThenValueComparer_Compare_AceIsHighAffectsSort()
        {
            // Arrange
            var comparer = CardComparer.SuitThenValue;
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
