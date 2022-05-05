using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyer.Testing.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class ScalarResultSetTests
    {

        [TestMethod]
        public void ScalarResultSet_CreateResults_int_NullParamReturnsEmptyList()
        {
            // Arrange

            // Act
            var objs = ScalarResultSet.CreateResults<int>(null);

            // Assert
            Assert.AreEqual(0, objs.Count());
        }

        [TestMethod]
        public void ScalarResultSet_CreateResults_int_CreatesPerparesValueForDapperDataReader()
        {
            // Arrange

            // Act
            var objs = ScalarResultSet.CreateResults(new int[] { 1, 2, 3, 4, 5 });

            // Assert
            Assert.AreEqual(5, objs.Count());
            Assert.AreEqual(1, objs.First().Value);
        }

        [TestMethod]
        public void ScalarResultSet_CreateResults_string_CreatesPerparesValueForDapperDataReader()
        {
            // Arrange

            // Act
            var objs = ScalarResultSet.CreateResults(new string[] { "Test", "Two", "Three" });

            // Assert
            Assert.AreEqual(3, objs.Count());
            Assert.AreEqual("Test", objs.First().Value);
        }

        [TestMethod]
        public void ScalarResultSet_CreateResults_DateTime_CreatesPerparesValueForDapperDataReader()
        {
            // Arrange

            // Act
            var objs = ScalarResultSet.CreateResults(new DateTime[] { new DateTime(2020, 2, 2), DateTime.Today, DateTime.Today, DateTime.Today });

            // Assert
            Assert.AreEqual(4, objs.Count());
            Assert.AreEqual(new DateTime(2020, 2, 2), objs.First().Value);
        }

    }

}
