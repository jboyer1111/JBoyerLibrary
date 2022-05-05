using JBoyerLibaray.Exceptions;
using JBoyerLibaray.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.DnDDiceRoller
{

    [TestClass, ExcludeFromCodeCoverage]
    public class DnDFormulaTests
    {

        #region Construction Tests

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Cannot be null, empty, or whitespace.\r\nParameter name: formula")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaIsNull()
        {
            // Arrange

            // Act
            new DnDFormula(null, new UTRandom(1));

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Cannot be null, empty, or whitespace.\r\nParameter name: formula\r\nActual value was .")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaIsEmpty()
        {
            // Arrange

            // Act
            new DnDFormula("", new UTRandom(1));

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Cannot be null, empty, or whitespace.\r\nParameter name: formula\r\nActual value was \t\r\n.")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaIsWhiteSpace()
        {
            // Arrange

            // Act
            new DnDFormula("\t\r\n", new UTRandom(1));

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: rand")]
        public void DnDFormula_Constructor_ThrowsArugmentNullExceptionWhenRandomIsNull()
        {
            // Arrange

            // Act
            new DnDFormula("1d6", null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Formula contains invalid characters: Test\r\nParameter name: formula\r\nActual value was Te1d6st.")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaHasInvalidChars()
        {
            // Arrange

            // Act
            new DnDFormula("Te1d6st", new UTRandom(1));

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Formula is invalid. Invalid part is '50d50d50'\r\nParameter name: formula\r\nActual value was 50d50d50.")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaHasInvalidStructure()
        {
            // Arrange

            // Act
            new DnDFormula("50d50d50", new UTRandom(1));

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Cannot have l and h in the same calculation.\r\nParameter name: formula\r\nActual value was 3h3l4d6.")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaHasIncomplatableSpeicalChars()
        {
            // Arrange

            // Act
            new DnDFormula("3h3l4d6", new UTRandom(1));

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Formula is invalid. Has too many 'h' characters.\r\nParameter name: formula\r\nActual value was 3h3h4d6.")]
        public void DnDFormula_Constructor_ThrowsArugmentInvalidExceptionWhenFormulaHasMultipleSpeicalChars()
        {
            // Arrange

            // Act
            new DnDFormula("3h3h4d6", new UTRandom(1));

            // Assert
        }

        [TestMethod]
        public void DnDFormula_Constructor_CanCreateObject()
        {
            // Arrange

            // Act
            new DnDFormula("1d6",  new UTRandom(1));

            // Assert
        }

        #endregion

        #region Roll Tests

        [TestMethod]
        public void DnDFormula_Roll()
        {
            // Arrange
            var formula = new DnDFormula("1d6", new UTRandom(1));

            // Act
            int result = formula.Roll();

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DnDFormula_Roll_SumHighestThreeDie()
        {
            // Arrange
            var formula = new DnDFormula("3h4d6", new UTRandom(1, 4, 3, 2));

            // Act
            int result = formula.Roll();

            // Assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void DnDFormula_Roll_SumLowestThreeDie()
        {
            // Arrange
            var formula = new DnDFormula("3l4d6", new UTRandom(1, 4, 3, 2));

            // Act
            int result = formula.Roll();

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void DnDFormula_Roll_SumMinValue()
        {
            // Arrange
            var formula = new DnDFormula("3m4d6", new UTRandom(1, 4, 3, 2));

            // Act
            int result = formula.Roll();

            // Assert
            Assert.AreEqual(14, result);
        }

        [TestMethod]
        public void DnDFormula_Roll_SumMaxValue()
        {
            // Arrange
            var formula = new DnDFormula("3M4d6", new UTRandom(1, 4, 3, 2));

            // Act
            int result = formula.Roll();

            // Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void DnDFormula_Roll_AddPerDie()
        {
            // Arrange
            var formula = new DnDFormula("4d6+1", new UTRandom(1));

            // Act
            int result = formula.Roll();

            // Assert
            Assert.AreEqual(8, result);
        }


        #endregion

        #region Stat Tests

        [TestMethod]
        public void DnDFormula_Stats_NeverRolledReturnsNull()
        {
            // Arrange
            var formula = new DnDFormula("1d6", new UTRandom(1));

            // Act
            var result = formula.Stats;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DnDFormula_Stats_RolledReturnsLastStatInfo()
        {
            // Arrange
            var formula = new DnDFormula("1d6", new UTRandom(1));
            formula.Roll();

            // Act
            var result = formula.Stats;

            // Assert
            Assert.AreEqual("1: 1", result);
        }

        [TestMethod]
        public void DnDFormula_Stats_MultiPartPlus()
        {
            // Arrange
            var formula = new DnDFormula("1d6 + 1d6", new UTRandom(1));
            formula.Roll();

            // Act
            var result = formula.Stats;

            // Assert
            Assert.AreEqual("2: (1), (1)", result);
        }

        [TestMethod]
        public void DnDFormula_Stats_MultiPartMinus()
        {
            // Arrange
            var formula = new DnDFormula("1d6 - 1d6", new UTRandom(1));
            formula.Roll();

            // Act
            var result = formula.Stats;

            // Assert
            Assert.AreEqual("0: (1), -(1)", result);
        }

        [TestMethod]
        public void DnDFormula_Stats_MultiPartMinusConstant()
        {
            // Arrange
            var formula = new DnDFormula("1d6 - 1", new UTRandom(1));
            formula.Roll();

            // Act
            var result = formula.Stats;

            // Assert
            Assert.AreEqual("0: (1), -(1)", result);
        }

        #endregion

    }

}
