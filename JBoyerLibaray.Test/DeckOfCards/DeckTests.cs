using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    public class DeckTests : DeckStrings
    {

        #region Constructor Tests
        
        [TestMethod]
        public void Deck_Constructor()
        {
            // Arrange

            // Act
            new Deck();

            // Assert
        }

        [TestMethod]
        public void Deck_DecksAreAutoMaticlyShuffled()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            //No action is required

            //Assert
            Assert.AreNotEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Deck_AceIsHighIsFalseByDefault()
        {
            // Arrange

            // Act
            var result = new Deck().AceIsHigh;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Deck_AceIsHighIsUpdateable()
        {
            // Arrange
            var deck = new Deck();

            // Act
            deck.AceIsHigh = true;

            // Assert
            Assert.IsTrue(deck.AceIsHigh);
        }

        [TestMethod]
        public void Deck_AceIsHighUpdatesCardSettingsAsWell()
        {
            // Arrange
            var deck = Deck.GetUnShuffledDeck();
            var card = deck.First();

            // Act
            var before = card.Value;

            deck.AceIsHigh = true;

            var after = card.Value;

            // Assert
            Assert.AreNotEqual(before, after);
            Assert.AreEqual(1, before);
            Assert.AreEqual(14, after);
        }

        [TestMethod]
        public void Deck_CardCountGivesCountOne()
        {
            // Arrange

            // Act
            var result = new Deck();

            // Assert
            Assert.AreEqual(52, result.CardCount);
            Assert.AreEqual(result.Count(), result.CardCount);
        }

        [TestMethod]
        public void Deck_CardCountGivesCountTwo()
        {
            // Arrange
            var result = new Deck();

            // Act
            result.DrawCards(10);

            // Assert
            Assert.AreEqual(42, result.CardCount);
            Assert.AreEqual(result.Count(), result.CardCount);
        }

        [TestMethod]
        public void Deck_CardsReturnsDeckAsIEnumerable()
        {
            // Arrange
            var deck = Deck.GetUnShuffledDeck();

            // Act
            IEnumerable<Card> cards = Deck.GetUnShuffledDeck();

            // Assert
            Assert.IsNotNull(cards);
            Assert.IsTrue(cards.SequenceEqual(deck.Cards));
        }

        #endregion

        #region Equals Tests

        [TestMethod]
        public void Deck_EqualsOverrideMethodReturnsTrueWhenEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            object deck2 = Deck.GetUnShuffledDeck();

            // Act

            // Assert
            Assert.IsTrue(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_EqualsOverrideMethodReturnsFalseWhenNotEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            object deck2 = new Deck();

            // Act

            // Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_EqualsOverrideMethodReturnsFalseWhenObjectIsNotDeck()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            object deck2 = "";

            // Act

            // Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_EqualsMethodReturnsTrueWhenEqual()
        {
            //Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_EqualsMethodReturnsTrueWhenReferenceEquals()
        {
            //Arrange
            Deck deck1 = new Deck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(deck1.Equals(deck1));
        }

        [TestMethod]
        public void Deck_EqualsMethodReturnsFalseWhenOrderIsDifferenct()
        {
            //Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = new Deck();

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_EqualsMethodReturnsFalseWhenDeckIsNull()
        {
            //Arrange
            Deck deck1 = new Deck();
            Deck deck2 = null;

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_EqualsOpperator()
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
        public void Deck_NotEqualsOpperator()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(deck != deck2);
        }

        [TestMethod]
        public void Deck_EqualsStaticMethod()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(Deck.Equals(deck, deck2));
        }

        #endregion

        #region HashCode Tests

        [TestMethod]
        public void Deck_GetHashCodeReturnsSameHashCodeWhenEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            // Act

            // Assert
            Assert.AreEqual(deck1.GetHashCode(), deck2.GetHashCode());
        }

        [TestMethod]
        public void Deck_GetHashCodeReturnsDifferenctHashCodeWhenNotEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            // Act
            deck2.Shuffle();

            // Assert
            Assert.AreNotEqual(deck1.GetHashCode(), deck2.GetHashCode());
        }

        #endregion

        #region Public Method Tests

        [TestMethod]
        public void Deck_SuffleDeckAcutallyShuffles()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            string before = deck.ToString();

            //Act
            deck.Shuffle();

            string after = deck.ToString();

            //Assert
            Assert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void Deck_DrawCardRemovesCardFromDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            var before = deck.CardCount;

            //Act
            deck.Draw();

            var after = deck.CardCount;

            //Assert
            Assert.AreEqual(52, before);
            Assert.IsTrue(before > after);
            Assert.AreEqual(51, after);
        }

        [TestMethod]
        public void Deck_DrawCardRemovesCardFromTheTopOfTheDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.Draw();

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECKFIRSTCARDMISSING, deck.ToString());
        }

        [TestMethod]
        public void Deck_DrawCardsRemovesCardsFormDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            var before = deck.CardCount;

            //Act
            deck.DrawCards(4);

            var after = deck.CardCount;

            //Assert
            Assert.AreEqual(52, before);
            Assert.IsTrue(before > after);
            Assert.AreEqual(48, after);
        }

        [TestMethod]
        public void DrawCardsRemocesCardsFromTopOfTheDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.DrawCards(4);

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECKFIRSTFOURCARDMISSING, deck.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deck_DrawCardsThrowsExceptionIfArgIsLessThan1()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawCards(0);

            //Assert Throws Exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughCardsException))]
        public void Deck_DrawsCardsThrowsErrorIfNotEnoughCardsInDeck()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawCards(53);

            //Assert Throws Exception
        }



        #endregion

        #region Static Method Tests

        #endregion








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
        [ExpectedException(typeof(ArgumentException), "")]
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
