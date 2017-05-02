﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DifferentOrderEquatorTests
    {

        [TestMethod]
        public void DifferentOrderEquator_EqualReturnsTrue()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = new Deck();

            var deckEquator = DeckEquator.DifferentOrder;

            // Act
            var result = deckEquator.Equals(deck1, deck2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DifferentOrderEquator_EqualReturnsFalse()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            var deckEquator = DeckEquator.DifferentOrder;

            // Act
            deck2.Draw();
            var result = deckEquator.Equals(deck1, deck2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DifferentOrderEquator_GetHashCodeIsSame()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = new Deck();

            var deckEquator = DeckEquator.DifferentOrder;

            // Act

            // Assert
            Assert.AreEqual(deckEquator.GetHashCode(deck1), deckEquator.GetHashCode(deck2));
        }

        [TestMethod]
        public void DifferentOrderEquator_GetHashCodeIsNotTheSame()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = new Deck();

            var deckEquator = DeckEquator.DifferentOrder;

            // Act
            deck2.Draw();

            // Assert
            Assert.AreNotEqual(deckEquator.GetHashCode(deck1), deckEquator.GetHashCode(deck2));
        }
    }
}
