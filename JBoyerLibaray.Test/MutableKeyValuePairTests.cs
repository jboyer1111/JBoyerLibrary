using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MutableKeyValuePairTests
    {

        [TestMethod]
        public void MutableKeyValuePair_ConstructorOne()
        {
            // Arrange

            // Act
            new MutableKeyValuePair<string, string>();

            // Assert
        }

        [TestMethod]
        public void MutableKeyValuePair_ConstructorTwo()
        {
            // Arrange

            // Act
            new MutableKeyValuePair<string, string>("Key", "Value");

            // Assert
        }

        [TestMethod]
        public void MutableKeyValuePair_ConstructorSetsKeyArgToKey()
        {
            // Arrange


            // Act
            var mutKeyValPair = new MutableKeyValuePair<string, string>("Key", null);

            // Assert
            Assert.AreEqual("Key", mutKeyValPair.Key);
        }

        [TestMethod]
        public void MutableKeyValuePair_ConstructorSetsValueArgToValue()
        {
            // Arrange


            // Act
            var mutKeyValPair = new MutableKeyValuePair<string, string>(null, "Value");

            // Assert
            Assert.AreEqual("Value", mutKeyValPair.Value);
        }


        [TestMethod]
        public void MutableKeyValuePair_CanSetKeyAfterConstruction()
        {
            // Arrange
            var mutKeyValPair = new MutableKeyValuePair<string, string>();

            // Act
            mutKeyValPair.Key = "Key";

            // Assert
            Assert.AreEqual("Key", mutKeyValPair.Key);
        }

        [TestMethod]
        public void MutableKeyValuePair_CanSetValueAfterConstruction()
        {
            // Arrange
            var mutKeyValPair = new MutableKeyValuePair<string, string>();

            // Act
            mutKeyValPair.Value = "Value";

            // Assert
            Assert.AreEqual("Value", mutKeyValPair.Value);
        }


    }
}
