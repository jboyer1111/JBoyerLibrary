using JBoyerLibaray.DeckOfCards;
using JBoyerLibaray.HelperClasses;
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
    public class DataReaderHelperTests
    {

        [TestMethod]
        public void DataReaderHelper_GetData_SingleResult()
        {
            // Arrange
            Deck deck = Deck.GetUnShuffledDeck();

            // Act
            var data = DataReaderHelper.GetData(deck.ToDataReader());

            // Assert
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(52, data.First().Rows.Count());
        }


        [TestMethod]
        public void DataReaderHelper_GetData_MultiResult()
        {
            // Arrange
            List<IDataReader> dataReaders = new List<IDataReader>();
            int readerIndex = 0;

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(r => r.Read()).Returns(() => dataReaders[readerIndex].Read());
            mockReader.Setup(r => r.FieldCount).Returns(() => dataReaders[readerIndex].FieldCount);
            mockReader.Setup(r => r.GetName(It.IsAny<int>())).Returns<int>(i => dataReaders[readerIndex].GetName(i));
            mockReader.Setup(r => r.GetValue(It.IsAny<int>())).Returns<int>(i => dataReaders[readerIndex].GetValue(i));
            mockReader.Setup(r => r.NextResult()).Returns(() => { readerIndex++; return readerIndex < dataReaders.Count; });

            dataReaders.Add(Deck.GetUnShuffledDeck().ToDataReader());
            dataReaders.Add(People.TestData.ToDataReader());

            // Act
            var data = DataReaderHelper.GetData(mockReader.Object);

            // Assert
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(52, data[0].Rows.Count());
            Assert.AreEqual(8, data[1].Rows.Count());
        }


        [TestMethod]
        public void DataReaderHelper_ExecuteReaderToData_GetsResultsFromCommandIntoDataHelperObjects()
        {
            // Arrange

            Mock<IDbCommand> mockCommand = new Mock<IDbCommand>();
            mockCommand.Setup(m => m.ExecuteReader()).Returns(Deck.GetUnShuffledDeck().ToDataReader());

            // Act
            var data = DataReaderHelper.ExecuteReaderToData(mockCommand.Object);

            // Assert
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(52, data.First().Rows.Count());
        }

    }

}
