using JBoyerLibaray.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeCommandTests
    {

        #region Constructor Tests

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FakeCommand_Constructor_NullFakeDatabaseThrowError()
        {
            // Arragne

            // Act 
            using (new FakeCommand(null)) { }

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeCommand_Constructor_DoesNotHaveAConnectionWhenNotPassedIn()
        {
            // Arragne

            // Act 
            using (var command = new FakeCommand(new FakeDatabase()))
            {
                // Assert
                Assert.IsNull(command.Connection);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FakeCommand_Constructor_IDbConnection_NullFakeDatabaseThrowError()
        {
            // Arragne

            // Act 
            using (new FakeCommand(null, new FakeConnection(new FakeDatabase()))) { }

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeCommand_Constructor_HasAConnectionWhenPassedIn()
        {
            // Arragne

            // Act 
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var command = new FakeCommand(new FakeDatabase(), connection))
            {
                // Assert
                Assert.IsTrue(Object.ReferenceEquals(command.Connection, connection), "Connection objects are not the same refernce.");
            }
        }

        #endregion

        #region ExecuteNonQuery Tests

        [TestMethod]
        public void FakeCommand_ExecuteNonQuery_DetectsDapperUpdateSql()
        {
            // Arrange
            using (var fakeCommand = new FakeCommand(new FakeDatabase()))
            {
                fakeCommand.CommandText = "Update TableName Set ColumnName = 'New Value' Where Id = @Id";

                // Act
                var result = fakeCommand.ExecuteNonQuery();

                // Assert
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteNonQuery_NonDapperUpdateStatement()
        {
            // Arrange
            string updateSql = "Update TableName Set ColumnName = 'New Value' Where Name = @Name";

            var database = new FakeDatabase();
            database.SetupNonQuerySql(updateSql);

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = updateSql;

                // Act
                var result = fakeCommand.ExecuteNonQuery();

                // Assert
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteNonQuery_DetectsDapperDeleteSql()
        {
            // Arrange
            using (var fakeCommand = new FakeCommand(new FakeDatabase()))
            {
                fakeCommand.CommandText = "Delete From TableName Where Id = @Id";

                // Act
                var result = fakeCommand.ExecuteNonQuery();

                // Assert
                Assert.AreEqual(2, result);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteNonQuery_NonDapperDeleteStatement()
        {
            // Arrange
            string updateSql = "Delete From TableName Where Where Name = @Name";

            var database = new FakeDatabase();
            database.SetupNonQuerySql(updateSql);

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = updateSql;

                // Act
                var result = fakeCommand.ExecuteNonQuery();

                // Assert
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteNonQuery_NonQuerySql()
        {
            // Arrange
            string sql = "Drop Table Billy;";

            var database = new FakeDatabase();
            database.SetupNonQuerySql(sql);

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = sql;

                // Act
                var result = fakeCommand.ExecuteNonQuery();

                // Assert
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteNonQuery_NonQueryStoredProcedure()
        {
            // Arrange
            string sql = "Drop Table Billy;";

            var database = new FakeDatabase();
            database.SetupNonQueryStoredProcedure(sql);

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = sql;
                fakeCommand.CommandType = CommandType.StoredProcedure;

                // Act
                var result = fakeCommand.ExecuteNonQuery();

                // Assert
                Assert.AreEqual(0, result);
            }
        }

        #endregion

        #region Execute Reader Tests

        [TestMethod]
        public void FakeCommand_ExecuteReader_DapperSelectAll()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupTable("TableName", TestData.GetTeams());

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = "Select * From TableName";

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                var results = new List<Team>();
                while (dataReader.Read())
                {
                    results.Add(new Team
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        Name = dataReader.GetString(dataReader.GetOrdinal("Name"))
                    });
                }

                CollectionAssert.AreEqual(new Team[] { new Team { Id = 1, Name = "Billy" }, new Team { Id = 2, Name = "Bob" } }, results);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteReader_NonDapperSelectAll()
        {
            // Arrange
            string sql = "Select Name From TableName";

            var database = new FakeDatabase();
            database.SetupSql(sql, TestData.GetTeams());

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = sql;

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                var results = new List<string>();
                while (dataReader.Read())
                {
                    results.Add(dataReader.GetString(dataReader.GetOrdinal("Name")));
                }

                CollectionAssert.AreEqual(new string[] { "Billy", "Bob" }, results);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteReader_DapperSelectSingle()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupTable("TableName", TestData.GetTeams());

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = "Select * From TableName Where Id = @Id";
                var parameter = fakeCommand.CreateParameter();
                parameter.ParameterName = "Id";
                parameter.Value = 1;
                parameter.DbType = DbType.Int32;

                fakeCommand.Parameters.Add(parameter);

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                var results = new List<Team>();
                while (dataReader.Read())
                {
                    results.Add(new Team
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        Name = dataReader.GetString(dataReader.GetOrdinal("Name"))
                    });
                }

                CollectionAssert.AreEqual(new Team[] { new Team { Id = 1, Name = "Billy" } }, results);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteReader_NonDapperSelectSingle()
        {
            // Arrange
            string sql = "Select Name From TableName Where Id = @Id";

            var database = new FakeDatabase();
            database.SetupSql(
                sql,
                (d, p) =>
                {
                    int id = (int)p.GetParam<FakeParameter>("Id").Value;
                    
                    return TestData.GetTeams().Where(r => r.Id == id).ToArray();
                },
                new string[] { "Id" }
            );

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = sql;
                var parameter = fakeCommand.CreateParameter();
                parameter.ParameterName = "Id";
                parameter.Value = 1;
                parameter.DbType = DbType.Int32;

                fakeCommand.Parameters.Add(parameter);

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                var results = new List<string>();
                while (dataReader.Read())
                {
                    results.Add(dataReader.GetString(dataReader.GetOrdinal("Name")));
                }

                CollectionAssert.AreEqual(new string[] { "Billy" }, results);
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteReader_DapperInsert()
        {
            // Arrange
            var database = new FakeDatabase();

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = "Insert Into Table (Col1, Col2) Values ('Col1', 'Col2');Select scope_identity() Text";

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                dataReader.Read();
                Assert.AreEqual(0, dataReader.GetInt32(dataReader.GetOrdinal("SCOPE_IDENTITY")));
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteReader_NonDapperInsert()
        {
            // Arrange
            string sql = "Insert Into Table (Col1, Col2) Values ('Col1', 'Col2');";
            var database = new FakeDatabase();
            database.SetupSql(
                sql,
                (d, p) =>
                {
                    return new[] { new { SCOPE_IDENTITY = 0 } };
                },
                new string[] { }
            );

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = sql;

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                dataReader.Read();
                Assert.AreEqual(0, dataReader.GetInt32(dataReader.GetOrdinal("SCOPE_IDENTITY")));
            }
        }

        [TestMethod]
        public void FakeCommand_ExecuteReader_StoredProcedure()
        {
            // Arrange
            var database = new FakeDatabase();
            database.SetupStoredProcedure("USP_SomeName", TestData.GetTeams());

            using (var fakeCommand = new FakeCommand(database))
            {
                fakeCommand.CommandText = "USP_SomeName";
                fakeCommand.CommandType = CommandType.StoredProcedure;

                // Act
                var dataReader = fakeCommand.ExecuteReader();

                // Assert
                var results = new List<Team>();
                while (dataReader.Read())
                {
                    results.Add(new Team
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        Name = dataReader.GetString(dataReader.GetOrdinal("Name"))
                    });
                }

                CollectionAssert.AreEqual(new Team[] { new Team { Id = 1, Name = "Billy" }, new Team { Id = 2, Name = "Bob" } }, results);
            }
        }

        #endregion

    }

}