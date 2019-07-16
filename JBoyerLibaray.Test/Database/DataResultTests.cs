using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.DeckOfCards;
using JBoyerLibaray.UnitTests;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace JBoyerLibaray.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class DataResultTests
    {

        [TestMethod]
        public void DataResult_Constructor()
        {
            // Arrange
            IDataReader reader = Deck.GetUnShuffledDeck().ToDataReader();

            // Act
            new DataResult(reader);

            // Assert
            // This test is only verifing no excption was thrown
        }

        [TestMethod]
        public void DataResult_Constructor_VerfiyColumnNameBuildUp()
        {
            // Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            
            // Act
            var result = new DataResult(deck.ToDataReader());

            // Assert
            CollectionAssert.AreEqual(
                new string[] { "Rank", "Suit", "Value" },
                result.ColumnNames.ToArray()
            );
        }

        [TestMethod]
        public void DataResult_GetEnumerator_OutputsItemsIncorrectOrder()
        {
            // Arrange
            Deck deck = Deck.GetUnShuffledDeck();
            List<string> expected = deck.Select(c => c.ToString()).ToList();

            // Act
            var result = new DataResult(deck.ToDataReader());

            // Assert
            List<string> resultCollection = new List<string>();
            Assert.AreEqual(52, result.Rows.Count());
            foreach (dynamic row in result)
            {
                resultCollection.Add($"{row.Rank} of {row.Suit}");
            }

            CollectionAssert.AreEqual(expected, resultCollection);
        }

    }

}
