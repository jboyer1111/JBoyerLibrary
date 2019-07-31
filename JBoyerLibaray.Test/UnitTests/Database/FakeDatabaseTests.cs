using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeDatabaseTests
    {

        [TestMethod]
        public void FakeDatabase_Constructor()
        {
            // Arrange

            // Act
            new FakeDatabase();

            // Assert
        }

        [TestMethod]
        public void FakeDatabase_SetupTable_AddsTableToObject()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupTable("Table", new string[] { "Test" });

            // Assert
            CollectionAssert.AreEqual(new string[] { "Table" }, fakeDatabase.Tables);
        }

        [TestMethod]
        public void FakeDatabase_SetupSql_AddsSqlToSqlWithObjectResolver()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupSql("Select * From Table Order By A Desc", (d, p) =>
            {
                return fakeDatabase.GetTableResults("Table").OrderByDescending(o => o as string);
            });

            // Assert
            CollectionAssert.AreEqual(new string[] { "Select * From Table Order By A Desc" }, fakeDatabase.SqlScripts);
        }

    }

}
