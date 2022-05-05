using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class FakeParameterCollectionTests
    {

        [TestMethod]
        public void FakeParameterCollection_Contains_ReturnsTrueIfContainsParameterName()
        {
            // Arrange
            var collection = new FakeParameterCollection();
            collection.Add(new FakeParameter("Param", "Value"));

            // Act
            var result = collection.Contains("Param");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FakeParameterCollection_Contains_IsCaseInsensative()
        {
            // Arrange
            var collection = new FakeParameterCollection();
            collection.Add(new FakeParameter("Param", "Value"));

            // Act
            var result = collection.Contains("paRam");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FakeParameterCollection_Contains_ReturnsFalseIfDoesNotContainsParameterName()
        {
            // Arrange
            var collection = new FakeParameterCollection();

            // Act
            var result = collection.Contains("Param");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FakeParameterCollection_IndexOf_ReturnsIndexNegativeOneWhenParamIsNotInList()
        {
            // Arrange
            var collection = new FakeParameterCollection();

            // Act
            var result = collection.IndexOf("Missing");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void FakeParameterCollection_IndexOf_ReturnsIndexOfParamname()
        {
            // Arrange
            var collection = new FakeParameterCollection();
            collection.Add(new FakeParameter("Billy", "Value"));
            collection.Add(new FakeParameter("CaveJohnson", "Value"));
            collection.Add(new FakeParameter("Mike", "Value"));

            // Act
            var result = collection.IndexOf("CaveJohnson");

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void FakeParameterCollection_RemoveAt_RemovesParamWithName()
        {
            // Arrange
            var collection = new FakeParameterCollection();
            collection.Add(new FakeParameter("Billy", "Value"));
            collection.Add(new FakeParameter("CaveJohnson", "Value"));
            collection.Add(new FakeParameter("Mike", "Value"));

            // Act
            collection.RemoveAt("CaveJohnson");

            // Assert
            CollectionAssert.AreEqual(new string[] { "Billy", "Mike" }, collection.Select(p => p.ParameterName).ToArray());
        }

        [TestMethod]
        public void FakeParameterCollection_Indexer_IDataParameterCollection_Get_GetsParameterByName()
        {
            // Arrange
            IDataParameterCollection collection = new FakeParameterCollection() { new FakeParameter("Billy", 1) };

            // Act
            var result = collection["Billy"] as IDataParameter;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Billy", result.ParameterName);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void FakeParameterCollection_Indexer_IDataParameterCollection_Get_ThrowsErrorWhenNameIsNotInList()
        {
            // Arrange
            IDataParameterCollection collection = new FakeParameterCollection() { };

            // Act
            var result = collection["Billy"];

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeParameterCollection_Indexer_IDataParameterCollection_Set_SetsParameterByName()
        {
            // Arrange
            IDataParameterCollection collection = new FakeParameterCollection() { new FakeParameter("Billy", 1) };

            // Act
            collection["Billy"] = new FakeParameter("Billy", 33);

            // Assert
            Assert.AreEqual(33, collection.Cast<IDataParameter>().First().Value);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void FakeParameterCollection_Indexer_IDataParameterCollection_Set_ThrowsErrorWhenNameIsNotInList()
        {
            // Arrange
            IDataParameterCollection collection = new FakeParameterCollection() { };

            // Act
            collection["Billy"] = new FakeParameter("Billy", 33);

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeParameterCollection_Indexer_Get_GetsParameterByName()
        {
            // Arrange
            FakeParameterCollection collection = new FakeParameterCollection() { new FakeParameter("Billy", 1) };

            // Act
            var result = collection["Billy"] as IDataParameter;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Billy", result.ParameterName);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void FakeParameterCollection_Indexer_Get_ThrowsErrorWhenNameIsNotInList()
        {
            // Arrange
            FakeParameterCollection collection = new FakeParameterCollection() { };

            // Act
            var result = collection["Billy"];

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void FakeParameterCollection_Indexer_Set_SetsParameterByName()
        {
            // Arrange
            FakeParameterCollection collection = new FakeParameterCollection() { new FakeParameter("Billy", 1) };

            // Act
            collection["Billy"] = new FakeParameter("Billy", 33);

            // Assert
            Assert.AreEqual(33, collection.Cast<IDataParameter>().First().Value);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void FakeParameterCollection_Indexer_Set_ThrowsErrorWhenNameIsNotInList()
        {
            // Arrange
            FakeParameterCollection collection = new FakeParameterCollection() { };

            // Act
            collection["Billy"] = new FakeParameter("Billy", 33);

            // Assert
            // Throws Error
        }


    }

}
