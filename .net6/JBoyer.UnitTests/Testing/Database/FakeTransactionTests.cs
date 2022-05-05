using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeTransactionTests
    {

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FakeTransaction_Constructor_ThrowArgumentNullExceptionWhenConnectionIsNull()
        {
            // Arrange

            // Act
            new FakeTrasaction(null);

            // Assert
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeTransaction_Commit_ThrowInvalidOperaionExceptionWhenCommitIsCalledTwice()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var transaction = new FakeTrasaction(connection))
            {
                connection.Open();

                // Act
                transaction.Commit();
                transaction.Commit();

                // Assert
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeTransaction_Commit_ThrowInvalidOperaionExceptionWhenCommitIsCalledAfterRollback()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var transaction = new FakeTrasaction(connection))
            {
                connection.Open();

                // Act
                transaction.Rollback();
                transaction.Commit();

                // Assert
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeTransaction_Commit_ThrowInvalidOperaionExceptionWhenCommitIsCalledAfterConnectionNoLongerOpen()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var transaction = new FakeTrasaction(connection))
            {
                connection.Open();

                // Act
                connection.Close();
                transaction.Commit();

                // Assert
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeTransaction_Rollback_ThrowInvalidOperaionExceptionWhenRollbackIsCalledTwice()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var transaction = new FakeTrasaction(connection))
            {
                connection.Open();

                // Act
                transaction.Rollback();
                transaction.Rollback();

                // Assert
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeTransaction_Rollback_ThrowInvalidOperaionExceptionWhenRollbackIsCalledAfterCommit()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var transaction = new FakeTrasaction(connection))
            {
                connection.Open();

                // Act
                transaction.Commit();
                transaction.Rollback();

                // Assert
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void FakeTransaction_Rollback_ThrowInvalidOperaionExceptionWhenRollbackIsCalledAfterConnectionNoLongerOpen()
        {
            // Arrange
            using (var connection = new FakeConnection(new FakeDatabase()))
            using (var transaction = new FakeTrasaction(connection))
            {
                connection.Open();

                // Act
                connection.Close();
                transaction.Rollback();

                // Assert
            }
        }

    }

}
