using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.HelperClasses;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
        public void Deck_Constructor_DecksAreAutoMaticlyShuffled()
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
        public void Deck_AceIsHigh_IsFalseByDefault()
        {
            // Arrange

            // Act
            var result = new Deck().AceIsHigh;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Deck_AceIsHigh_IsUpdateable()
        {
            // Arrange
            var deck = new Deck();

            // Act
            deck.AceIsHigh = true;

            // Assert
            Assert.IsTrue(deck.AceIsHigh);
        }

        [TestMethod]
        public void Deck_AceIsHigh_UpdatesCardSettingsAsWell()
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
        public void Deck_AceIsHigh_DoesNothingWhenSameValue()
        {
            // Arrange
            var deck = Deck.GetUnShuffledDeck();
            var card = deck.First();

            // Act
            var before = card.Value;

            deck.AceIsHigh = false;

            var after = card.Value;

            // Assert
            Assert.AreEqual(before, after);
        }

        [TestMethod]
        public void Deck_CardCount_GivesCountOne()
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
        public void Deck_Cards_ReturnsDeckAsIEnumerable()
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
        public void Deck_Equals_OverrideMethodReturnsTrueWhenEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            object deck2 = Deck.GetUnShuffledDeck();

            // Act

            // Assert
            Assert.IsTrue(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_Equals_OverrideMethodReturnsFalseWhenNotEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            object deck2 = new Deck();

            // Act

            // Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_Equals_OverrideMethodReturnsFalseWhenObjectIsNotDeck()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            object deck2 = "";

            // Act

            // Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_Equals_MethodReturnsTrueWhenEqual()
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
        public void Deck_Equals_MethodReturnsTrueWhenReferenceEquals()
        {
            //Arrange
            Deck deck1 = new Deck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(deck1.Equals(deck1));
        }

        [TestMethod]
        public void Deck_Equals_MethodReturnsFalseWhenOrderIsDifferenct()
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
        public void Deck_Equals_MethodReturnsFalseWhenNotSameCountOfCardsLeft()
        {
            //Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            deck2.Draw();

            //Assert
            Assert.IsFalse(deck1.Equals(deck2));
        }

        [TestMethod]
        public void Deck_Equals_MethodReturnsFalseWhenDeckIsNull()
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
        public void Deck_Equals_Opperator()
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
        public void Deck_Equals_StaticMethod()
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
        public void Deck_Equals_WithCompare()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(Deck.Equals(deck, deck2, EqualityComparer<Deck>.Default));
        }


        #endregion

        #region HashCode Tests

        [TestMethod]
        public void Deck_GetHashCode_ReturnsSameHashCodeWhenEqual()
        {
            // Arrange
            Deck deck1 = Deck.GetUnShuffledDeck();
            Deck deck2 = Deck.GetUnShuffledDeck();

            // Act

            // Assert
            Assert.AreEqual(deck1.GetHashCode(), deck2.GetHashCode());
        }

        [TestMethod]
        public void Deck_GetHashCode_ReturnsDifferenctHashCodeWhenNotEqual()
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
        public void Deck_Suffle_DeckAcutallyShuffles()
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
        public void Deck_DrawCard_RemovesCardFromDeck()
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
        public void Deck_DrawCard_RemovesCardFromTheTopOfTheDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.Draw();

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECKFIRSTCARDMISSING, deck.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyDeckException))]
        public void Deck_Draw_ThrowsExceptionWhenEmtpy()
        {
            // Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            deck.DrawCards(52);

            // Act
            deck.Draw();

            // Assert
        }

        [TestMethod]
        public void Deck_DrawCards_RemovesCardsFormDeck()
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
        public void Deck_DrawCards_RemocesCardsFromTopOfTheDeck()
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
        public void Deck_DrawCards_ThrowsExceptionIfArgIsLessThan1()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawCards(0);

            //Assert Throws Exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughCardsException))]
        public void Deck_DrawsCards_ThrowsErrorIfNotEnoughCardsInDeck()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawCards(53);

            //Assert Throws Exception
        }

        [TestMethod]
        public void Deck_DrawUpToCards_RemovesCardsFormDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            var before = deck.CardCount;

            //Act
            deck.DrawUpToCards(4);

            var after = deck.CardCount;

            //Assert
            Assert.AreEqual(52, before);
            Assert.IsTrue(before > after);
            Assert.AreEqual(48, after);
        }

        [TestMethod]
        public void Deck_DrawUpToCards_RemocesCardsFromTopOfTheDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            deck.DrawUpToCards(4);

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECKFIRSTFOURCARDMISSING, deck.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deck_DrawUpToCards_ThrowsExceptionIfArgIsLessThan1()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.DrawUpToCards(0);

            //Assert Throws Exception
        }

        [TestMethod]
        public void Deck_DrawsUpToCards_ReturnsAsManyCardsAsItCanIfNumberIsGreaterThenAmountOfCardsLeft()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            var cards = deck.DrawUpToCards(53);

            //Assert
            Assert.AreEqual(52, cards.Length);
        }

        [TestMethod]
        public void Deck_Sort_BySuitThenValueSortIsAnUnSuffledDeck()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.Sort(CardComparer.SuitThenValue);

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        [TestMethod]
        public void Deck_Sort_ByValueThenSuitSortsAsExpected()
        {
            //Arrange
            Deck deck = new Deck();

            //Act
            deck.Sort(CardComparer.ValueThenSuit);

            //Assert
            Assert.AreEqual(VALUETHENSUITSORTEDDECK, deck.ToString());
        }

        #endregion

        #region Static Method Tests

        [TestMethod]
        public void Deck_Copy_MakesCopiesDeckToANewReference()
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
        public void Deck_GetUnShuffledDeck_ReturnsAnUnShffuledDeck()
        {
            //Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            //Act
            //No action required

            //Assert
            Assert.AreEqual(UNSHUFFLEDDECK, deck.ToString());
        }

        [TestMethod]
        public void Deck_Parse_TurnsAStringIntoADeck()
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
        public void Deck_Parse_DeckThrowsErrorWhenInvalidString()
        {
            //Arrange
            Deck deck = Deck.Parse("Test");

            //Act

            //Assert Throws Exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void Deck_Parse_ThrowsArgumentExceptionWhenStringIsNull()
        {
            //Arrange
            Deck deck = Deck.Parse(null);

            //Act

            //Assert Throws Exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void Deck_Parse_ThrowsArgumentExceptionWhenStringIsEmpty()
        {
            //Arrange
            Deck deck = Deck.Parse("");

            //Act

            //Assert Throws Exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void Deck_Parse_ThrowsArgumentExceptionWhenStringIsWhitespace()
        {
            //Arrange
            Deck deck = Deck.Parse("   ");

            //Act

            //Assert Throws Exception
        }

        [TestMethod]
        public void Deck_TryParse_TurnsAStringIntoADeck()
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
        public void Deck_TryParse_ReturnsFalseAndNullWhenStringInvalid()
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
        public void Deck_TryParse_ReturnsFalseAndNullWhenArgIsNull()
        {
            //Arrange
            Deck deck;

            //Act
            var result = Deck.TryParse(null, out deck);

            //Assert
            Assert.IsFalse(result);
            Assert.IsNull(deck);
        }

        [TestMethod]
        public void Deck_TryParse_ReturnsFalseAndNullWhenArgIsEmpty()
        {
            //Arrange
            Deck deck;

            //Act
            var result = Deck.TryParse("", out deck);

            //Assert
            Assert.IsFalse(result);
            Assert.IsNull(deck);
        }

        [TestMethod]
        public void Deck_TryParse_ReturnsFalseAndNullWhenArgIsWhitespace()
        {
            //Arrange
            Deck deck;

            //Act
            var result = Deck.TryParse("   ", out deck);

            //Assert
            Assert.IsFalse(result);
            Assert.IsNull(deck);
        }

        #endregion


        [TestMethod]
        public void Deck_GetEnumerator()
        {
            // Arrange
            IEnumerable deck = new Deck();

            // Act
            var result = deck.GetEnumerator();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
