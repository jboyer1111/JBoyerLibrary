using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using JBoyerLibaray.DeckOfCards;

namespace JBoyerLibaray.Test.DeckOfCardsTests
{
    [TestClass]
    public class MultiDeckTests : JBoyerLibaray.Test.DeckOfCardsTests.SetupFunctions
    {
        [TestMethod]
        public void MulitDeckHasMoreCardsThanADeck()
        {
            //Arrange
            Deck deck = new Deck();
            Deck mulitDeck = new MultiDeck(2);

            //Act
            //No action is required

            //Assert
            Assert.IsTrue(deck.CardCount < mulitDeck.CardCount);
        }


    }
}
