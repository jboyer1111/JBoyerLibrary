using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JBoyerLibaray.DeckOfCards;

namespace JBoyerLibaray.Test.DeckOfCardsTests
{
    [TestClass]
    public class DeckTests : SetupFunctions
    {
        [TestMethod]
        public void DeckEqualsMethod()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(deck.Equals(deck2));
        }

        [TestMethod]
        public void DeckEqualsOpperator()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(deck == deck2);
        }

        [TestMethod]
        public void DeckEqualsStaticMethod()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(Deck.Equals(deck, deck2));
        }

        [TestMethod]
        public void SuffleDeckAcutallyShuffles()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            deck2.Shuffle();

            //Assert
            Assert.IsFalse(deck2.Equals(deck));
        }

        [TestMethod]
        public void DecksAreAutoMaticlyShuffled()
        {
            //Arrange
            Deck deck = new Deck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(deck2.Equals(deck));
        }

        [TestMethod]
        public void DrawCardRemovesCardFromDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.Draw();

            //Assert
            Assert.AreNotEqual<int>(52, deck.CardCount);
        }

        [TestMethod]
        public void DrawCardRemovesOnlyOneCard()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.Draw();

            //Assert
            Assert.AreEqual<int>(51, deck.CardCount);
        }

        [TestMethod]
        public void DrawCardRemovesCardFromTheTopOfTheDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            
            //Act
            deck.Draw();

            //Assert
            Assert.AreEqual<string>(UNSHUFFLEDDECKFIRSTCARDMISSING, deck.ToString());
        }

        [TestMethod]
        public void DrawCardsRemovesCardsFormDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.DrawCards(2);

            //Assert
            Assert.AreNotEqual<int>(52, deck.CardCount);
        }

        [TestMethod]
        public void DrawCardsRemoveCorrectAmountOfCards()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.DrawCards(4);

            //Assert
            Assert.AreEqual<int>(48, deck.CardCount);
        }


        [TestMethod]
        public void DrawCardsRemocesCardsFromTopOfTheDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            
            //Act
            deck.DrawCards(4);
            
            //Assert
            Assert.AreEqual<string>(UNSHUFFLEDDECKFIRSTFOURCARDMISSING, deck.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This function is made to draw more than 1 card if you wish to draw 1 card use Draw() to do so.")]
        public void DrawCardsCorrectlyThrowsErrorToNotAllowOneCardsDraw()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawCards(1);

            //Assert Throws Exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughCardsException), "You do not have enough cards in the deck to draw 53 cards")]
        public void DrawsCardsThrowsErrorIfNotEnoughCardsInDeck()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawCards(53);

            //Assert Throws Exception
        }

        [TestMethod]
        public void CopyDeckMakesCopiesDeckToANewReference()
        {
            // Arrange
            Deck deck1 = new Deck();
            
            // Act
            Deck deck2 = Deck.Copy(deck1);

            //Assert
            Assert.IsFalse(Deck.ReferenceEquals(deck1, deck2));
            Assert.IsTrue(deck1 == deck2);
        }

        [TestMethod]
        public void GetUnShuffledDeckReturnsAnUnShffuledDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            //No action required

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        [TestMethod]
        public void SuitThenValueSortIsAnUnSuffledDeck()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.Sort(CardComparer.SuitThenValue);

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        [TestMethod]
        public void ValueThenSuitSortsAsExpected()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.Sort(CardComparer.ValueThenSuit);

            //Assert
            Assert.AreEqual(VALUETHENSUITSORTEDDECK, deck.ToString());
        }

        [TestMethod]
        public void DeckParseTurnsAStringIntoADeck()
        {
            //Arrange
            Deck deck = Deck.Parse(UNSHUFFLEDDECK);

            //Act
            //No Action Req

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "")]
        public void ParseDeckThrowsErrorWhenInvalidString()
        {
            //Arrange
            Deck deck = Deck.Parse("Test");

            //Act

            //Assert Throws Exception
        }

        [TestMethod]
        public void DeckTryParseTurnsAStringIntoADeck()
        {
            //Arrange
            Deck deck;
            

            //Act
            var result = Deck.TryParse(UNSHUFFLEDDECK, out deck);

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        [TestMethod]
        public void TryParseDeckThrowsErrorWhenInvalidString()
        {
            //Arrange
            Deck deck;

            //Act
            var result = Deck.TryParse("Test", out deck);

            //Assert
            Assert.IsFalse(result);
            Assert.IsNull(deck);
        }

        [TestMethod]
        public void DeckEquatorDiffOrderWorks()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.Copy(deck);
            deck2.Shuffle();

            //Act
            var result = Deck.Equals(deck, deck2, DeckEquator.DifferentOrder);

            //Assert
            Assert.IsTrue(result);
        }

    }
}
