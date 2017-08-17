using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.DnDDiceRoller;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.Exceptions;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.Test.DnDDiceRollerTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DnDFormulaTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArgumentInvalidWhenFormulaIsNull()
        {
            // Arrange

            // Act
            new DnDFormula(null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArgumentInvalidWhenFormulaIsEmpty()
        {
            // Arrange

            // Act
            new DnDFormula("");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArgumentInvalidWhenFormulaIsWhitespace()
        {
            // Arrange

            // Act
            new DnDFormula("\t\r\n");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArgumentInvalidWhenFormulaHasInvalidChar()
        {
            // Arrange
            var expected = String.Join(Environment.NewLine, new string[] {
                "Formula contains an invalid character: \"",
                "Parameter name: formula",
                "Actual value was 5d6\".",
            });

            // Act
            try
            {
                new DnDFormula("5d6\"");
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual(expected, e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArgumentInvalidWhenFormulaHasInvalidCharTwo()
        {
            // Arrange
            var expected = String.Join(Environment.NewLine, new string[] {
                "Formula contains invalid characters: abcefgijknopqurstuvwxyzABCDEFGHIJKLNOPQRSTUVWXYZ!@#$%^&*()[]{}\\|;:,<.>/?\"'",
                "Parameter name: formula",
                "Actual value was abcdefghijklmnopqurstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()[]{}\\|;:,<.>/?\"'.",
            });

            // Act
            try
            {
                new DnDFormula("abcdefghijklmnopqurstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()[]{}\\|;:,<.>/?\"'");
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual(expected, e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DnDFormula_ConstructorThrowsArgumentNullWhenRandomIsNull()
        {
            // Arrange

            // Act
            new DnDFormula("1d6", null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArugmentInvalidExceptionWhenDoesNotMatchRegex()
        {
            //Arrange

            //Act
            new DnDFormula("3d4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArugmentInvalidExceptionWhenBothHAndLAreInFormula()
        {
            //Arrange

            //Act
            new DnDFormula("3l3h4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArugmentInvalidExceptionWhenHaveToManyOfSameCharLowChar()
        {
            //Arrange

            //Act
            new DnDFormula("3l3l4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArugmentInvalidExceptionWhenHaveToManyOfSameCharHighChar()
        {
            //Arrange

            //Act
            new DnDFormula("3h3h4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArugmentInvalidExceptionWhenHaveToManyOfSameCharMaxChar()
        {
            //Arrange

            //Act
            new DnDFormula("3M3M4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormula_ConstructorThrowsArugmentInvalidExceptionWhenHaveToManyOfSameCharMinChar()
        {
            //Arrange

            //Act
            new DnDFormula("3m3m4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpetsNumber()
        {
            // Arrange

            // Act
            var result = new DnDFormula("1");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets1D6()
        {
            // Arrange

            // Act
            var result = new DnDFormula("1d6");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets1D6Plus5()
        {
            // Arrange

            // Act
            var result = new DnDFormula("1d6+5");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets3h4d6()
        {
            // Arrange

            // Act
            var result = new DnDFormula("3h4d6");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets3l4d6()
        {
            // Arrange

            // Act
            var result = new DnDFormula("3l4d6");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets3m4d6()
        {
            // Arrange

            // Act
            var result = new DnDFormula("3m4d6");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets3M4d6()
        {
            // Arrange

            // Act
            var result = new DnDFormula("3M4d6");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_ConstructorAccpets2PartItems()
        {
            // Arrange

            // Act
            var result = new DnDFormula("1d6 + 1d4");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsNullIfNotRolled()
        {
            // Arrange
            var roller = new DnDFormula("1d6");

            // Act
            var result = roller.Stats;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats1d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("1d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("1: 1", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("4d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("10: 1, 2, 3, 4", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats3h4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("3h4d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("9: 4, 3, 2, [1]", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats3h6d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("3h6d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("15: 6, 5, 4, [3, 2, 1]", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats3l4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("3l4d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("6: 1, 2, 3, [4]", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats3M4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("3M4d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("7: 1, 2, 3, 1", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats3m4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("3m4d6", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("18: 3, 4, 5, 6", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats5m7M4d10()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            var roller = new DnDFormula("5m7M4d10", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("23: 5, 6, 7, 5", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats1d6Plus2d5()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("1d6 + 2d5", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("6: (1), 5:(2, 3)", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats2d6Plus1d5()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("2d6 + 1d5", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("6: 3:(1, 2), (3)", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats2d6Plus1d5Plus2()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("2d6 + 1d5 + 2", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("8: 3:(1, 2), (3), (2)", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats1d6Plus3h4d6Plus2()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("1d6 + 3h4d6 + 2", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("15: (1), 12:(5, 4, 3, [2]), (2)", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats1d6Minus2d5()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("1d6 - 2d5", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("-4: (1), -5:(2, 3)", result);
        }

        [TestMethod]
        public void DnDFormula_StatsReturnsStats2d6Minus1d5()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormula("2d6 - 1d5", random);
            roller.Roll();

            // Act
            var result = roller.Stats;

            // Assert
            Assert.AreEqual("0: 3:(1, 2), -(3)", result);
        }
    }
}
