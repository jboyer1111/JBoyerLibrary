using JBoyerLibaray.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CardTests : DeckStrings
    {

        #region Constructor Tests

        [TestMethod]
        public void Card_Constructor()
        {
            // Arrange

            // Act
            new Card("Spades", "Ace");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Constructor_ThrowsArugmentExceptionWhenSuitIsNull()
        {
            // Arrange

            // Act
            new Card(null, "Ace");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Constructor_ThrowsArugmentExceptionWhenSuitIsEmpty()
        {
            // Arrange

            // Act
            new Card("", "Ace");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Constructor_ThrowsArugmentExceptionWhenSuitIsWhitespace()
        {
            // Arrange

            // Act
            new Card("\t\r\n", "Ace");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Constructor_ThrowsArugmentExceptionWhenRankIsNull()
        {
            // Arrange

            // Act
            new Card("Spades", null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Constructor_ThrowsArugmentExceptionWhenRankIsEmpty()
        {
            // Arrange

            // Act
            new Card("Spades", "");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Constructor_ThrowsArugmentExceptionWhenRankIsWhitespace()
        {
            // Arrange

            // Act
            new Card("Spades", "\t\r\n");

            // Assert
        }

        #endregion

        #region Properties Tests

        [TestMethod]
        public void Card_Value_AceDefaultValueIsOne()
        {
            //Arrange
            Card card = new Card("Heart", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(1, card.Value);
        }

        [TestMethod]
        public void Card_Value_AceIsHighValueIsFourteen()
        {
            //Arrange
            Card card = new Card("Heart", "Ace");

            //Act
            card.SetAceIsHighSetting(true);

            //Assert
            Assert.AreEqual(14, card.Value);
        }

        [TestMethod]
        public void Card_Value_KingsValueIsThirteen()
        {
            //Arrange
            Card card = new Card("Heart", "King");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(13, card.Value);
        }

        [TestMethod]
        public void Card_Value_QueensValueIsTweleve()
        {
            //Arange
            Card card = new Card("Heart", "Queen");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(12, card.Value);
        }

        [TestMethod]
        public void Card_Value_JacksValueIsEleven()
        {
            //Arrange
            Card card = new Card("Heart", "Jack");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(11, card.Value);
        }


        [TestMethod]
        public void Card_Value_RandomTextIsZero()
        {
            //Arrange
            Card card = new Card("Heart", "RdmText");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(0, card.Value);
        }

        [TestMethod]
        public void Card_Value_HighNumberRankIsZero()
        {
            //Arrange
            Card card = new Card("Heart", "11");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(0, card.Value);
        }

        [TestMethod]
        public void Card_Value_10Is10()
        {
            //Arrange
            Card card = new Card("Heart", "10");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(10, card.Value);
        }

        #endregion

        #region Equals Tests

        [TestMethod]
        public void Card_Equals_OverrideMethodReturnsTrueWhenEqual()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            object card2 = new Card("Spades", "Ace");

            // Act

            // Assert
            Assert.IsTrue(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_OverrideMethodReturnsFalseWhenNotEqual()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            object card2 = new Card("Spades", "King");

            // Act

            // Assert
            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_OverrideMethodReturnsFalseWhenObjectIsNotCard()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            object card2 = "";

            // Act

            // Assert
            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_MethodReturnsTrueWhenEqual()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_MethodReturnsTrueWhenReferenceEquals()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(card1.Equals(card1));
        }

        [TestMethod]
        public void Card_Equals_MethodReturnsFalseWhenRankIsNotEqual()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "King");

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_MethodReturnsFalseWhenSuitIsNotEqual()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Hearts", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_MethodReturnsFalseWhenCardIsNull()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = null;

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_Equals_MethodReturnsFalseWhenCardIsSubtype()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            TestCard card2 = new TestCard("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(card1.Equals(card2));
        }

        [TestMethod]
        public void Card_EqualsOperator()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(card1 == card2);
        }

        [TestMethod]
        public void Card_NotEqualsOperator()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsFalse(card1 != card2);
        }

        [TestMethod]
        public void Card_Equals_StaticMethod()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(Card.Equals(card1, card2));
        }

        #endregion

        #region HashCode Tests

        [TestMethod]
        public void Card_GetHashCode_ReturnsSameHashCodeWhenEqual()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "Ace");

            // Act

            // Assert
            Assert.AreEqual(card1.GetHashCode(), card2.GetHashCode());
        }

        [TestMethod]
        public void Card_GetHashCode_ReturnsDifferentHashCodeWhenRankNotSame()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "10");

            // Act

            // Assert
            Assert.AreNotEqual(card1.GetHashCode(), card2.GetHashCode());
        }

        [TestMethod]
        public void Card_GetHashCode_ReturnsDifferentHashCodeWhenSuitNotSame()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Hearts", "Ace");

            // Act

            // Assert
            Assert.AreNotEqual(card1.GetHashCode(), card2.GetHashCode());
        }

        [TestMethod]
        public void Card_GetHashCode_ReturnsDifferentHashCodeWhenTypeNotSame()
        {
            // Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new TestCard("Hearts", "Ace");

            // Act

            // Assert
            Assert.AreNotEqual(card1.GetHashCode(), card2.GetHashCode());
        }

        #endregion

        #region Static Methods Test

        [TestMethod]
        public void Card_Parse_ParsesTextCorrectly()
        {
            // Arrange

            // Act
            Card card = Card.Parse("5 of Hearts");

            // Assert
            Assert.AreEqual(5, card.Value);
            Assert.AreEqual("Hearts", card.Suit);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Parse_ThrowErrorWhenArgIsNull()
        {
            // Arrange

            // Act
            Card.Parse(null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Parse_ThrowErrorWhenArgIsEmtpy()
        {
            // Arrange

            // Act
            Card.Parse("");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Parse_ThrowErrorWhenArgIsWhitespace()
        {
            // Arrange

            // Act
            Card.Parse("\t\r\n");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Parse_ThrowErrorWhenArgIsNotValidString()
        {
            // Arrange

            // Act
            Card.Parse("RdmText");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Card_Parse_ThrowErrorWhenArgHasTooManyOf()
        {
            // Arrange

            // Act
            Card.Parse("RdmText of RdmText of RdmText");

            // Assert
        }

        [TestMethod]
        public void Card_TryParse_AttemptsToParseText()
        {
            // Arrange
            Card card = null;
            // Act
            var result = Card.TryParse("5 of Hearts", out card);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(5, card.Value);
            Assert.AreEqual("Hearts", card.Suit);
        }

        [TestMethod]
        public void Card_TryParse_ReturnsFalseArgIsNull()
        {
            // Arrange
            Card card = null;
            // Act
            var result = Card.TryParse(null, out card);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(card);
        }

        [TestMethod]
        public void Card_TryParse_ReturnsFalseArgIsEmtpy()
        {
            // Arrange
            Card card = null;
            // Act
            var result = Card.TryParse("", out card);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(card);
        }

        [TestMethod]
        public void Card_TryParse_ReturnsFalseArgIsWhitespace()
        {
            // Arrange
            Card card = null;
            // Act
            var result = Card.TryParse("\r\t\n", out card);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(card);
        }

        [TestMethod]
        public void Card_TryParse_ReturnsFalseWhenStrNotValid()
        {
            // Arrange
            Card card = null;
            // Act
            var result = Card.TryParse("RdmText", out card);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(card);
        }

        [TestMethod]
        public void Card_TryParse_ReturnsFalseWhenArgHasTooManyOf()
        {
            // Arrange
            Card card = null;
            // Act
            var result = Card.TryParse("RdmText of RdmText of RdmText", out card);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(card);
        }

        #endregion
    }
}
