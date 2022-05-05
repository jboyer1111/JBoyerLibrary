using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeConnectionTests
    {

        [TestMethod]
        public void FakeConnection_BeginTransaction_CreatesTransaction()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                // Act
                var result = connection.BeginTransaction();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(FakeTrasaction));
                Assert.AreEqual(System.Data.IsolationLevel.Unspecified, result.IsolationLevel);
            }
        }

        [TestMethod]
        public void FakeConnection_BeginTransaction_IsolationLevel_CreatesTransaction()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                // Act
                var result = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(FakeTrasaction));
                Assert.AreEqual(System.Data.IsolationLevel.ReadUncommitted, result.IsolationLevel);
            }
        }

        [TestMethod]
        public void FakeConnection_Database_VerifyDefaultValue()
        {
            // Arrange

            // Act
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                // Assert
                Assert.AreEqual("FakeDatabase", connection.Database);
            }
        }

        [TestMethod]
        public void FakeConnection_ChangeDatabase_ChangesDatabaseString()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                var before = connection.Database;

                // Act
                connection.ChangeDatabase("Changed");

                // Assert
                Assert.IsFalse(before == connection.Database, "Database string did not update!");
            }
        }

        [TestMethod]
        public void FakeConnection_State_VerifyDefaultValue()
        {
            // Arrange

            // Act
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                // Assert
                Assert.AreEqual(ConnectionState.Closed, connection.State);
            }
        }

        [TestMethod]
        public void FakeConnection_Open_ChangesStateToOpen()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                // Act
                connection.Open();

                // Assert
                Assert.AreEqual(ConnectionState.Open, connection.State);
            }
        }

        [TestMethod]
        public void FakeConnection_Close_ChangesStateToClose()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                connection.Open();

                // Act
                connection.Close();

                // Assert
                Assert.AreEqual(ConnectionState.Closed, connection.State);
            }
        }

        [TestMethod]
        public void FakeConnection_Dispose_UpdatesFlag()
        {
            // Arrange
            FakeConnection connection;

            // Act
            using (connection = new FakeConnection(new FakeDatabase())) { }

            // Assert
            Assert.IsTrue(connection.IsDisposed, "IsDisposed did not update on dispose.");
        }

        [TestMethod]
        public void FakeConnection_Dispose_ClosesConnection()
        {
            // Arrange
            FakeConnection connection;

            // Act
            using (connection = new FakeConnection(new FakeDatabase()))
            {
                connection.Open();
            }

            // Assert
            Assert.AreEqual(ConnectionState.Closed, connection.State, "State did not update on dispose.");
        }

        [TestMethod]
        public void FakeConnection_CreateCommand_CreateFakeCommand()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            {
                // Act
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    // Assert
                    Assert.IsInstanceOfType(command, typeof(FakeCommand));
                }
            }
        }

    }

}
