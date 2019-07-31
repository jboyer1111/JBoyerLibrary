using JBoyerLibaray.DeckOfCards;
using JBoyerLibaray.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
        public void DataResult_Constructor_VerfiyNoColumnNameBuildUp()
        {
            // Arrange
            int rowIndex = 0;

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(r => r.Read()).Returns(() => { rowIndex++; return rowIndex < 2; });
            mockReader.Setup(r => r.FieldCount).Returns(3);

            // Act
            var result = new DataResult(mockReader.Object);

            // Assert
            CollectionAssert.AreEqual(
                new string[] { "(No column name)", "(No column name 1)", "(No column name 2)" },
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
