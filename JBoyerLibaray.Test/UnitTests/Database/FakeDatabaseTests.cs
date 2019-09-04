using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;
using JBoyerLibaray.HelperClasses;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeDatabaseTests
    {

        #region Setup Table

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: tableName")]
        public void FakeDatabase_SetupTable_IEnumerable_ThrowsArgumentExceptionWhenTableNameIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable(null, rows);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: tableName")]
        public void FakeDatabase_SetupTable_IEnumerable_ThrowsArgumentExceptionWhenTableNameIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("", rows);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: tableName")]
        public void FakeDatabase_SetupTable_IEnumerable_ThrowsArgumentExceptionWhenTableNameIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("\t\r\n", rows);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupTable_IEnumerable_SetsUpTable()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("TableOne", rows);

            // Assert
            Assert.AreEqual(1, fakeDatabase.Tables.Count());
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupTable_IEnumerable_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("TableOne", rows);
            fakeDatabase.SetupTable("TableOne", rows);
            
            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeDatabase_SetupTable_IEnumerable_NullDefaultsToEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = null;

            // Act
            fakeDatabase.SetupTable("TableOne", rows);

            // Assert
            Assert.AreEqual(1, fakeDatabase.Tables.Count());
            Assert.AreEqual(0, fakeDatabase.GetTableResults(fakeDatabase.Tables.First()).Count());
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: tableName")]
        public void FakeDatabase_SetupTable_Func_ThrowsArgumentExceptionWhenTableNameIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<IEnumerable<TableRow>> rows = () => Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable(null, rows);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: tableName")]
        public void FakeDatabase_SetupTable_Func_ThrowsArgumentExceptionWhenTableNameIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<IEnumerable<TableRow>> rows = () => Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("", rows);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace.\r\nParameter name: tableName")]
        public void FakeDatabase_SetupTable_Func_ThrowsArgumentExceptionWhenTableNameIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<IEnumerable<TableRow>> rows = () => Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("\t\r\n", rows);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupTable_Func_SetsUpTable()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<IEnumerable<TableRow>> rows = () => Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("TableOne", rows);

            // Assert
            Assert.AreEqual(1, fakeDatabase.Tables.Count());
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupTable_Func_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<IEnumerable<TableRow>> rows = () => Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupTable("TableOne", rows);
            fakeDatabase.SetupTable("TableOne", rows);

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeDatabase_SetupTable_Func_NullDefaultsToEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<IEnumerable<TableRow>> rows = null;

            // Act
            fakeDatabase.SetupTable("TableOne", rows);

            // Assert
            Assert.AreEqual(1, fakeDatabase.Tables.Count());
            Assert.AreEqual(0, fakeDatabase.GetTableResults(fakeDatabase.Tables.First()).Count());
        }


        #endregion



    }

}
