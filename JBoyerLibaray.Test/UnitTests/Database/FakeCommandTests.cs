using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeCommandTests
    {

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FakeCommand_Constructor_NullFakeDatabaseThrowError()
        {
            // Arragne

            // Act 
            new FakeCommand(null);

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeCommand_Constructor_DoesNotHaveAConnectionWhenNotPassedIn()
        {
            // Arragne

            // Act 
            var command = new FakeCommand(new FakeDatabase());

            // Assert
            Assert.IsNull(command.Connection);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FakeCommand_Constructor_IDbConnection_NullFakeDatabaseThrowError()
        {
            // Arragne

            // Act 
            new FakeCommand(null, new FakeConnection(new FakeDatabase()));

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeCommand_Constructor_HasAConnectionWhenPassedIn()
        {
            // Arragne
            var connection = new FakeConnection(new FakeDatabase());

            // Act 
            var command = new FakeCommand(new FakeDatabase(), connection);

            // Assert
            Assert.IsTrue(Object.ReferenceEquals(command.Connection, connection), "Connection objects are not the same refernce.");
        }

        [TestMethod]
        public void TestMethod()
        {
            // Arrange

            // Act

            // Assert
        }

    }

}