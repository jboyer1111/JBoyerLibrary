using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyer.Testing.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeDatabaseTests
    {

        #region Callback Logic

        // ================================================================
        // Delete Callbacks
        // ================================================================

        [TestMethod]
        public void FakeDatabase_CallDeleteCallback_DoesNothingWhenNoCallbackSetup()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.CallDeleteCallback("Billy");

            // Assert
        }

        [TestMethod]
        public void FakeDatabase_CallDeleteCallback_CallsSetupCallback()
        {
            // Arrange
            bool called = false;
            var database = new FakeDatabase();
            database.SetupDeleteCallback("Billy", () => { called = true; });

            // Act
            database.CallDeleteCallback("Billy");

            // Assert
            Assert.IsTrue(called, "Delete callback was not called");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupDeleteCallback_ThrowsErrorWhenTableNameIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupDeleteCallback(null, () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupDeleteCallback_ThrowsErrorWhenTableNameIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupDeleteCallback("", () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupDeleteCallback_ThrowsErrorWhenTableNameIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupDeleteCallback("\t\r\n", () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'deleteCallback')")]
        public void FakeDatabase_SetupDeleteCallback_ThrowsErrorWhenCallbackIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupDeleteCallback("TableName", null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The delete callback for table \"TableName\" has already been setup.")]
        public void FakeDatabase_SetupDeleteCallback_ThrowsErrorWhenIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupDeleteCallback("TableName", () => { });

            // Act
            database.SetupDeleteCallback("TableName", () => { });

            // Assert
        }

        // ================================================================
        // Insert Callbacks
        // ================================================================

        [TestMethod]
        public void FakeDatabase_CallInsertCallback_DoesNothingWhenNoCallbackSetup()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.CallInsertCallback("Billy");

            // Assert
        }

        [TestMethod]
        public void FakeDatabase_CallInsertCallback_CallsSetupCallback()
        {
            // Arrange
            bool called = false;
            var database = new FakeDatabase();
            database.SetupInsertCallback("Billy", () => { called = true; });

            // Act
            database.CallInsertCallback("Billy");

            // Assert
            Assert.IsTrue(called, "Insert callback was not called");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupInsertCallback_ThrowsErrorWhenTableNameIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupInsertCallback(null, () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupInsertCallback_ThrowsErrorWhenTableNameIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupInsertCallback("", () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupInsertCallback_ThrowsErrorWhenTableNameIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupInsertCallback("\t\r\n", () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'insertCallback')")]
        public void FakeDatabase_SetupInsertCallback_ThrowsErrorWhenCallbackIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupInsertCallback("TableName", null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The insert callback for table \"TableName\" has already been setup.")]
        public void FakeDatabase_SetupInsertCallback_ThrowsErrorWhenIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupInsertCallback("TableName", () => { });

            // Act
            database.SetupInsertCallback("TableName", () => { });

            // Assert
        }

        // ================================================================
        // Update Callbacks
        // ================================================================

        [TestMethod]
        public void FakeDatabase_CallUpdateCallback_DoesNothingWhenNoCallbackSetup()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.CallUpdateCallback("Billy");

            // Assert
        }

        [TestMethod]
        public void FakeDatabase_CallUpdateCallback_CallsSetupCallback()
        {
            // Arrange
            bool called = false;
            var database = new FakeDatabase();
            database.SetupUpdateCallback("Billy", () => { called = true; });

            // Act
            database.CallUpdateCallback("Billy");

            // Assert
            Assert.IsTrue(called, "Update callback was not called");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupUpdateCallback_ThrowsErrorWhenTableNameIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupUpdateCallback(null, () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupUpdateCallback_ThrowsErrorWhenTableNameIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupUpdateCallback("", () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_SetupUpdateCallback_ThrowsErrorWhenTableNameIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupUpdateCallback("\t\r\n", () => { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'updateCallback')")]
        public void FakeDatabase_SetupUpdateCallback_ThrowsErrorWhenCallbackIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupUpdateCallback("TableName", null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The update callback for table \"TableName\" has already been setup.")]
        public void FakeDatabase_SetupUpdateCallback_ThrowsErrorWhenIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupUpdateCallback("TableName", () => { });

            // Act
            database.SetupUpdateCallback("TableName", () => { });

            // Assert
        }

        #endregion

        #region Non Query Logic

        // ================================================================
        // Sql Scripts
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The sql \"Drop Table TableName;\" was not setup for non query in the database.")]
        public void FakeDatabase_CallNonQuerySql_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.CallNonQuerySql("Drop Table TableName;", new FakeParameterCollection());

            // Assert
        }

        [TestMethod]
        public void FakeDatabase_CallNonQuerySql_CallsSetupCallback()
        {
            // Arrange
            bool called = false;
            var database = new FakeDatabase();
            database.SetupNonQuerySql("Drop Table TableName;", (d, p) =>
            {
                called = true;
            });

            // Act
            database.CallNonQuerySql("Drop Table TableName;", new FakeParameterCollection());

            // Assert
            Assert.IsTrue(called, "Non Query callback was not called");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupNonQuerySql_NoCallback_ThrowExceptionWhenSqlIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql(null, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupNonQuerySql_NoCallback_ThrowExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql("", new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupNonQuerySql_NoCallback_ThrowExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql("\t\r\n", new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The Sql \"Drop Table TableName;\" has already been setup.")]
        public void FakeDatabase_SetupNonQuerySql_NoCallback_ThrowExceptionWhenSqlIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupNonQuerySql("Drop Table TableName;", new string[] { "Param1" });

            // Act
            database.SetupNonQuerySql("Drop Table TableName;", new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupNonQuerySql_Callback_ThrowExceptionWhenSqlIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql(null, (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupNonQuerySql_Callback_ThrowExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql("", (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupNonQuerySql_Callback_ThrowExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql("\t\r\n", (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'nonQueryCallback')")]
        public void FakeDatabase_SetupNonQuerySql_Callback_ThrowExceptionWhenCallbackIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQuerySql("Drop Table TableName;", null, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The Sql \"Drop Table TableName;\" has already been setup.")]
        public void FakeDatabase_SetupNonQuerySql_Callback_ThrowExceptionWhenSqlIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupNonQuerySql("Drop Table TableName;", (d, p) => { }, new string[] { "Param1" });

            // Act
            database.SetupNonQuerySql("Drop Table TableName;", (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        // ================================================================
        // Stored Procedure
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The stored procedure \"USP_DropDatabase\" was not setup for non query in the database.")]
        public void FakeDatabase_CallNonQueryStoredProcedure_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.CallNonQueryStoredProcedure("USP_DropDatabase", new FakeParameterCollection());

            // Assert
        }

        [TestMethod]
        public void FakeDatabase_CallNonQueryStoredProcedure_CallsSetupCallback()
        {
            // Arrange
            bool called = false;
            var database = new FakeDatabase();
            database.SetupNonQueryStoredProcedure("USP_DropDatabase", (d, p) =>
            {
                called = true;
            });

            // Act
            database.CallNonQueryStoredProcedure("USP_DropDatabase", new FakeParameterCollection());

            // Assert
            Assert.IsTrue(called, "Non Query callback was not called");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_NoCallback_ThrowExceptionWhenSqlIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure(null, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_NoCallback_ThrowExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure("", new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_NoCallback_ThrowExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure("\t\r\n", new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The Stored Procedure \"USP_DropDatabase\" has already been setup.")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_NoCallback_ThrowExceptionWhenSqlIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupNonQueryStoredProcedure("USP_DropDatabase", new string[] { "Param1" });

            // Act
            database.SetupNonQueryStoredProcedure("USP_DropDatabase", new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_Callback_ThrowExceptionWhenSqlIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure(null, (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_Callback_ThrowExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure("", (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_Callback_ThrowExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure("\t\r\n", (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'nonQueryCallback')")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_Callback_ThrowExceptionWhenCallbackIsNull()
        {
            // Arrange
            var database = new FakeDatabase();

            // Act
            database.SetupNonQueryStoredProcedure("USP_DropDatabase", null, new string[] { "Param1" });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "The Stored Procedure \"USP_DropDatabase\" has already been setup.")]
        public void FakeDatabase_SetupNonQueryStoredProcedure_Callback_ThrowExceptionWhenSqlIsAlreadySetup()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupNonQueryStoredProcedure("USP_DropDatabase", (d, p) => { }, new string[] { "Param1" });

            // Act
            database.SetupNonQueryStoredProcedure("USP_DropDatabase", (d, p) => { }, new string[] { "Param1" });

            // Assert
        }

        #endregion

        #region Dapper Table Logic

        // ================================================================
        // Get Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_GetTableResults_ThrowsErrorWhenTableIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetTableResults(null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_GetTableResults_ThrowsErrorWhenTableIsEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetTableResults("");

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
        public void FakeDatabase_GetTableResults_ThrowsErrorWhenTableIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetTableResults("\t\r\n");

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Table \"TableOne\" is not setup in the database.")]
        public void FakeDatabase_GetTableResults_ThrowsErrorWhenTableNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetTableResults("TableOne");

            // Assert
            // Throws Exception
        }

        // ================================================================
        // Static Lists
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
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

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
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

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
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

        // ================================================================
        // Dynamic return function
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
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

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
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

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'tableName')")]
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

        #region Sql Statement Logic

        // ================================================================
        // Get Scalar Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptScalar_ThrowsErrorWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptScalar(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptScalar_ThrowsErrorWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptScalar("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptScalar_ThrowsErrorWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptScalar("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Sql \"SQLSTATEMENT\" is not setup in the database.")]
        public void FakeDatabase_GetSqlScriptScalar_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptScalar("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_GetSqlScriptScalar_ReturnsScalarValue()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.SetupSql("SQLSTATEMENT", new TableRow(1), null);

            // Act
            var result = fakeDatabase.GetSqlScriptScalar("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Scalar regardless of sql is first row first column data.
            Assert.IsInstanceOfType(result, typeof(DateTime));
        }

        [TestMethod]
        public void FakeDatabase_GetSqlScriptScalar_ReturnsScalarValueWhenSqlWasSetupWithMultipleRows()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = new TableRow[] { new TableRow(1), new TableRow(2) };
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);

            // Act
            var result = fakeDatabase.GetSqlScriptScalar("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Scalar regardless of sql is first row first column data.
            Assert.IsInstanceOfType(result, typeof(DateTime));
        }

        [TestMethod]
        public void FakeDatabase_GetSqlScriptScalar_ReturnsScalarValueWhenSqlWasSetupWithMultipleResultSets()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var resultSet = new MultiResultSet();
            resultSet.Add(new TableRow[] { new TableRow(1) });
            resultSet.Add(new Team[] { new Team() { Name = "Billy" } });

            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => resultSet, null);

            // Act
            var result = fakeDatabase.GetSqlScriptScalar("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Scalar regardless of sql is first row first column data.
            Assert.IsInstanceOfType(result, typeof(DateTime));
        }

        // ================================================================
        // Get Results Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptResults_ThrowsErrorWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptResults(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptResults_ThrowsErrorWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptResults("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptResults_ThrowsErrorWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptResults("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Sql \"SQLSTATEMENT\" is not setup in the database.")]
        public void FakeDatabase_GetSqlScriptResults_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_GetSqlScriptResults_ReturnsResultsWhenSetupAsAScalarResult()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.SetupSql("SQLSTATEMENT", new TableRow(1), null);

            // Act
            var result = fakeDatabase.GetSqlScriptResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void FakeDatabase_GetSqlScriptResults_ReturnsResults()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = new TableRow[] { new TableRow(1), new TableRow(2) };
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);

            // Act
            var result = fakeDatabase.GetSqlScriptResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Sql is setup as a Mutilple Result set. You need to call GetSqlScriptMultiResults.")]
        public void FakeDatabase_GetSqlScriptResults_ThrowsErrorWhenSetupAsAMultiResultSet()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var results = new MultiResultSet();
            results.Add(new TableRow[] { new TableRow(1), new TableRow(2) });
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => results, null);

            // Act
            fakeDatabase.GetSqlScriptResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws error
        }

        // ================================================================
        // Get Multi Results Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptMultiResults_ThrowsErrorWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptMultiResults(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptMultiResults_ThrowsErrorWhenSqlIsEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptMultiResults("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_GetSqlScriptMultiResults_ThrowsErrorWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptMultiResults("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Sql \"SQLSTATEMENT\" is not setup in the database.")]
        public void FakeDatabase_GetSqlScriptMultiResults_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetSqlScriptMultiResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Sql is setup as a Single Result set. You need to call GetSqlScriptResults.")]
        public void FakeDatabase_GetSqlScriptMultiResults_ThrowsErrowWhenSetupAsScalarResult()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.SetupSql("SQLSTATEMENT", new TableRow(1), null);

            // Act
            fakeDatabase.GetSqlScriptMultiResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Sql is setup as a Single Result set. You need to call GetSqlScriptResults.")]
        public void FakeDatabase_GetSqlScriptMultiResults_ThrowsErrowWhenSetupAsSingleResultSet()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = new TableRow[] { new TableRow(1), new TableRow(2) };
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);

            // Act
            fakeDatabase.GetSqlScriptMultiResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws error
        }

        [TestMethod]
        public void FakeDatabase_GetSqlScriptMultiResults_GetsResults()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var results = new MultiResultSet();
            results.Add(new TableRow[] { new TableRow(1), new TableRow(2) });
            results.Add(new Team[] { new Team() { Name = "Billy" }, new Team() { Name = "Reader" }, new Team() { Name = "AHHHHH" } });
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => results, null);

            // Act
            var result = fakeDatabase.GetSqlScriptMultiResults("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.First().Count());
            Assert.AreEqual(3, result.Last().Count());
        }

        // ================================================================
        // Data reader method sets
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_getSqlScriptResultsReader_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getSqlScriptResultsReader(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_getSqlScriptResultsReader_ThrowsArgumentExceptionWhenSqlIsEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getSqlScriptResultsReader("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_getSqlScriptResultsReader_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getSqlScriptResultsReader("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Sql \"SQLSTATEMENT\" is not setup in the database.")]
        public void FakeDatabase_getSqlScriptResultsReader_ThrowsInvalidOperationExceptionWhenSqlIsNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getSqlScriptResultsReader("SQLSTATEMENT", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        // ================================================================
        // Setup Scalar
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Scalar_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupSql(null, new object { }, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Scalar_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupSql("", new object { }, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Scalar_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupSql("\t\r\n", new object { }, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupSql_Scalar_SetsUpSql()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", new object { }, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.SqlScripts.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupSql_Scalar_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", new object { }, null);
            fakeDatabase.SetupSql("SQLSTATEMENT", new object { }, null);

            // Assert
            // Throws Error
        }

        // ================================================================
        // Static List
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_IEnumerable_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql(null, rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_IEnumerable_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("", rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_IEnumerable_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("\t\r\n", rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupSql_IEnumerable_SetsUpSql()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.SqlScripts.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupSql_IEnumerable_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeDatabase_SetupSql_IEnumerable_NullDefaultsToEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = null;

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", rows, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.SqlScripts.Length);
            Assert.AreEqual(0, fakeDatabase.GetSqlScriptResults(fakeDatabase.SqlScripts.First(), new FakeParameterCollection()).Count());
        }

        // ================================================================
        // Dynamic return function
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Func_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql(null, (d, p) => rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Func_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("", (d, p) => rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Func_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("\t\r\n", (d, p) => rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'objectResultResolver')")]
        public void FakeDatabase_SetupSql_Func_ThrowsArgumentExceptionWhenObjectResultResolverIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> func = null;

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", func, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupSql_Func_SetsUpSql()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => rows, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.SqlScripts.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupSql_Func_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => rows, null);
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => rows, null);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'objectResultResolver')")]
        public void FakeDatabase_SetupSql_Func_ThrowsExceptionWhenFuncIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> func = null;

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", func, null);

            // Assert
            // Throws Exception
        }

        // ================================================================
        // Multiresult Set
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Multiresult_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupSql(null, (d, p) => sets, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Multiresult_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupSql("", (d, p) => sets, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'sql')")]
        public void FakeDatabase_SetupSql_Multiresult_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupSql("\t\r\n", (d, p) => sets, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'multiResultSet')")]
        public void FakeDatabase_SetupSql_Multiresult_ThrowsArgumentExceptionWhenMultiResultFuncIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<FakeDatabase, IDataParameterCollection, MultiResultSet> func = null;

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", func, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupSql_Multiresult_SetsUpSql()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => sets, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.SqlScripts.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupSql_Multiresult_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => sets, null);
            fakeDatabase.SetupSql("SQLSTATEMENT", (d, p) => sets, null);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'result')")]
        public void FakeDatabase_SetupSql_Multiresult_ThrowsExceptionWhenFuncIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            MultiResultSet sets = null;

            // Act
            fakeDatabase.SetupSql("SQLSTATEMENT", sets, null);

            // Assert
            // Throws Exception
        }

        #endregion

        #region Stored Procedure Logic

        // ================================================================
        // Get Scalar Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureScalar_ThrowsErrorWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureScalar(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureScalar_ThrowsErrorWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureScalar("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureScalar_ThrowsErrorWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureScalar("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure \"USP_Boop\" is not setup in the database.")]
        public void FakeDatabase_GetStoredProcedureScalar_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureScalar("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_GetStoredProcedureScalar_ReturnsScalarValue()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.SetupStoredProcedure("USP_Boop", new TableRow(1), null);

            // Act
            var result = fakeDatabase.GetStoredProcedureScalar("USP_Boop", new FakeParameterCollection());

            // Assert
            // Scalar regardless of sql is first row first column data.
            Assert.IsInstanceOfType(result, typeof(DateTime));
        }

        [TestMethod]
        public void FakeDatabase_GetStoredProcedureScalar_ReturnsScalarValueWhenSqlWasSetupWithMultipleRows()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = new TableRow[] { new TableRow(1), new TableRow(2) };
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);

            // Act
            var result = fakeDatabase.GetStoredProcedureScalar("USP_Boop", new FakeParameterCollection());

            // Assert
            // Scalar regardless of sql is first row first column data.
            Assert.IsInstanceOfType(result, typeof(DateTime));
        }

        [TestMethod]
        public void FakeDatabase_GetStoredProcedureScalar_ReturnsScalarValueWhenSqlWasSetupWithMultipleResultSets()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var resultSet = new MultiResultSet();
            resultSet.Add(new TableRow[] { new TableRow(1) });
            resultSet.Add(new Team[] { new Team() { Name = "Billy" } });

            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => resultSet, null);

            // Act
            var result = fakeDatabase.GetStoredProcedureScalar("USP_Boop", new FakeParameterCollection());

            // Assert
            // Scalar regardless of sql is first row first column data.
            Assert.IsInstanceOfType(result, typeof(DateTime));
        }

        // ================================================================
        // Get Results Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureResults_ThrowsErrorWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureResults(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureResults_ThrowsErrorWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureResults("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureResults_ThrowsErrorWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureResults("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure \"USP_Boop\" is not setup in the database.")]
        public void FakeDatabase_GetStoredProcedureResults_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureResults("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_GetStoredProcedureResults_ReturnsResultsWhenSetupAsAScalarResult()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.SetupStoredProcedure("USP_Boop", new TableRow(1), null);

            // Act
            var result = fakeDatabase.GetStoredProcedureResults("USP_Boop", new FakeParameterCollection());

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void FakeDatabase_GetStoredProcedureResults_ReturnsResults()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = new TableRow[] { new TableRow(1), new TableRow(2) };
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);

            // Act
            var result = fakeDatabase.GetStoredProcedureResults("USP_Boop", new FakeParameterCollection());

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Mutilple Result set. You need to call GetStoredProcedureMultiResults.")]
        public void FakeDatabase_GetStoredProcedureResults_ThrowsErrorWhenSetupAsAMultiResultSet()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var results = new MultiResultSet();
            results.Add(new TableRow[] { new TableRow(1), new TableRow(2) });
            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => results, null);

            // Act
            fakeDatabase.GetStoredProcedureResults("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws error
        }

        // ================================================================
        // Get Multi Results Logic
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureMultiResults_ThrowsErrorWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureMultiResults(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureMultiResults_ThrowsErrorWhenSqlIsEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureMultiResults("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_GetStoredProcedureMultiResults_ThrowsErrorWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureMultiResults("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure \"USP_Boop\" is not setup in the database.")]
        public void FakeDatabase_GetStoredProcedureMultiResults_ThrowsErrorWhenSqlNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.GetStoredProcedureMultiResults("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Single Result set. You need to call GetStoredProcedureResults.")]
        public void FakeDatabase_GetStoredProcedureMultiResults_ThrowsErrowWhenSetupAsScalarResult()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.SetupStoredProcedure("USP_Boop", new TableRow(1), null);

            // Act
            fakeDatabase.GetStoredProcedureMultiResults("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Single Result set. You need to call GetStoredProcedureResults.")]
        public void FakeDatabase_GetStoredProcedureMultiResults_ThrowsErrowWhenSetupAsSingleResultSet()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = new TableRow[] { new TableRow(1), new TableRow(2) };
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);

            // Act
            fakeDatabase.GetStoredProcedureMultiResults("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws error
        }

        [TestMethod]
        public void FakeDatabase_GetStoredProcedureMultiResults_GetsResults()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var results = new MultiResultSet();
            results.Add(new TableRow[] { new TableRow(1), new TableRow(2) });
            results.Add(new Team[] { new Team() { Name = "Billy" }, new Team() { Name = "Reader" }, new Team() { Name = "AHHHHH" } });
            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => results, null);

            // Act
            var result = fakeDatabase.GetStoredProcedureMultiResults("USP_Boop", new FakeParameterCollection());

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.First().Count());
            Assert.AreEqual(3, result.Last().Count());
        }

        // ================================================================
        // Data reader method sets
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_getStoredProcedureResultsReader_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getStoredProcedureResultsReader(null, new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_getStoredProcedureResultsReader_ThrowsArgumentExceptionWhenSqlIsEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getStoredProcedureResultsReader("", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedure')")]
        public void FakeDatabase_getStoredProcedureResultsReader_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getStoredProcedureResultsReader("\t\r\n", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure \"USP_Boop\" is not setup in the database.")]
        public void FakeDatabase_getStoredProcedureResultsReader_ThrowsInvalidOperationExceptionWhenSqlIsNotSetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.getStoredProcedureResultsReader("USP_Boop", new FakeParameterCollection());

            // Assert
            // Throws Exception
        }

        // ================================================================
        // Setup Scalar
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Scalar_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupStoredProcedure(null, new object { }, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Scalar_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupStoredProcedure("", new object { }, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Scalar_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupStoredProcedure("\t\r\n", new object { }, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupStoredProcedure_Scalar_SetsUpStoredProcedure()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", new object { }, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.StoredProcedures.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupStoredProcedure_Scalar_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", new object { }, null);
            fakeDatabase.SetupStoredProcedure("USP_Boop", new object { }, null);

            // Assert
            // Throws Error
        }

        // ================================================================
        // Static List
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_IEnumerable_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure(null, rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_IEnumerable_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("", rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_IEnumerable_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("\t\r\n", rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupStoredProcedure_IEnumerable_SetsUpStoredProcedure()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.StoredProcedures.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupStoredProcedure_IEnumerable_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeDatabase_SetupStoredProcedure_IEnumerable_NullDefaultsToEmtpy()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            IEnumerable<TableRow> rows = null;

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", rows, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.StoredProcedures.Length);
            Assert.AreEqual(0, fakeDatabase.GetStoredProcedureResults(fakeDatabase.StoredProcedures.First(), new FakeParameterCollection()).Count());
        }

        // ================================================================
        // Dynamic return function
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Func_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure(null, (d, p) => rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Func_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("", (d, p) => rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Func_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("\t\r\n", (d, p) => rows, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'objectResultResolver')")]
        public void FakeDatabase_SetupStoredProcedure_Func_ThrowsArgumentExceptionWhenObjectResultResolverIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> func = null;

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", func, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupStoredProcedure_Func_SetsUpStoredProcedure()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => rows, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.StoredProcedures.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupStoredProcedure_Func_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var rows = Enumerable.Empty<TableRow>();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => rows, null);
            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => rows, null);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'objectResultResolver')")]
        public void FakeDatabase_SetupStoredProcedure_Func_ThrowsExceptionWhenFuncIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> func = null;

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", func, null);

            // Assert
            // Throws Exception
        }

        // ================================================================
        // Multiresult Set
        // ================================================================

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_ThrowsArgumentExceptionWhenSqlIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupStoredProcedure(null, (d, p) => sets, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_ThrowsArgumentExceptionWhenSqlIsEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupStoredProcedure("", (d, p) => sets, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentException), "Cannot be null, empty, or whitespace. (Parameter 'storedProcedureName')")]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_ThrowsArgumentExceptionWhenSqlIsWhitespace()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupStoredProcedure("\t\r\n", (d, p) => sets, null);

            // Assert
            // Throws Exception
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'multiResultSet')")]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_ThrowsArgumentExceptionWhenMultiResultSetIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            Func<FakeDatabase, IDataParameterCollection, MultiResultSet> func = null;

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", func, null);

            // Assert
            // Throws Exception
        }

        [TestMethod]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_SetsUpStoredProcedure()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupStoredProcedure("USP_Boop", (d, p) => sets, null);

            // Assert
            Assert.AreEqual(1, fakeDatabase.StoredProcedures.Length);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_ThrowsErrorWhenTableAlreadySetup()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var sets = new MultiResultSet();

            // Act
            fakeDatabase.SetupStoredProcedure("SQLSTATEMENT", (d, p) => sets, null);
            fakeDatabase.SetupStoredProcedure("SQLSTATEMENT", (d, p) => sets, null);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null. (Parameter 'result')")]
        public void FakeDatabase_SetupStoredProcedure_Multiresult_ThrowsExceptionWhenFuncIsNull()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            MultiResultSet sets = null;

            // Act
            fakeDatabase.SetupStoredProcedure("SQLSTATEMENT", sets, null);

            // Assert
            // Throws Exception
        }

        #endregion

    }

}
