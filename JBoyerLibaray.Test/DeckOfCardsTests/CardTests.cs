using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using JBoyerLibaray.DeckOfCards;

namespace JBoyerLibaray.Test.DeckOfCardsTests
{
    [TestClass]
    public class CardTests : JBoyerLibaray.Test.DeckOfCardsTests.SetupFunctions
    {
        [TestMethod]
        public void CardEqualsMethod()
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
        public void CardEqualsOperator()
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
        public void CardEqualsStaticMethod()
        {
            //Arrange
            Card card1 = new Card("Spades", "Ace");
            Card card2 = new Card("Spades", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(Card.Equals(card1, card2));
        }

        [TestMethod]
        public void AceDefaultValueIsOne()
        {
            //Arrange
            Card card = new Card("Heart", "Ace");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(1, card.Value);
        }

        [TestMethod]
        public void AceIsHighValueIsFourteen()
        {
            //Arrange
            Card card = new Card("Heart", "Ace");

            //Act
            card.SetAceIsHighSetting(true);

            //Assert
            Assert.AreEqual(14, card.Value);
        }

        [TestMethod]
        public void KingsValueIsThirteen()
        {
            //Arrange
            Card card = new Card("Heart", "King");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(13, card.Value);
        }

        [TestMethod]
        public void QueensValueIsTweleve()
        {
            //Arange
            Card card = new Card("Heart", "Queen");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(12, card.Value);
        }

        [TestMethod]
        public void JacksValueIsEleven()
        {
            //Arrange
            Card card = new Card("Heart", "Jack");

            //Act
            //No action is required

            //Assert
            Assert.AreEqual(11, card.Value);
        }

    }
}
