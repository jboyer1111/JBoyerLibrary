using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JBoyerLibaray.Extensions;

namespace JBoyerLibaray.PasswordGenerator
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PasswordGeneratorTests
    {

        [TestMethod]
        public void PasswordGenerator_ConstructorNoArg()
        {
            // Arrange

            // Act
            new PasswordGenerator();

            // Assert
        }

        [TestMethod]
        public void PasswordGenerator_Constructor4Arg()
        {
            // Arrange

            // Act
            new PasswordGenerator(10, 1, 1, 1);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void PasswordGenerator_ConstructorThrowsExceptionWhenLengthIsNotLongEnough()
        {
            // Arrange

            // Act
            new PasswordGenerator(5, 5, 5, 5);

            // Assert
        }

        [TestMethod]
        public void PasswordGenerator_GenerateContainsRightAmountOfNonAlphaNumeric()
        {
            // Arrange
            var passGen = new PasswordGenerator(10, 2, 3, 3);

            // Act
            var result = passGen.Generate();

            // Assert
            Assert.AreEqual(2, result.Count(c => CharacterLists.NonAlphaNumericCharacters.Contains(c)));
        }

        [TestMethod]
        public void PasswordGenerator_GenerateContainsRightAmountOfUpperCase()
        {
            // Arrange
            var passGen = new PasswordGenerator(10, 3, 2, 3);

            // Act
            var result = passGen.Generate();

            // Assert
            Assert.AreEqual(2, result.Count(c => c == c.ToUpper() && CharacterLists.Characters.Contains(c.ToLower())));
        }

        [TestMethod]
        public void PasswordGenerator_GenerateContainsRightAmountOfNumbers()
        {
            // Arrange
            var passGen = new PasswordGenerator(10, 3, 3, 2);

            // Act
            var result = passGen.Generate();

            // Assert
            Assert.AreEqual(2, result.Count(c => CharacterLists.NumbericCharacters.Contains(c)));
        }
    }
}
