using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.UnitTests;
using JBoyerLibaray.Exceptions;

namespace JBoyerLibaray.DnDDiceRoller
{

    [TestClass, ExcludeFromCodeCoverage]
    public class DnDFormulaCalcTests
    {

        #region Construction Tests

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: rand")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentNullExceptionWhenRandomIsNull()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(null, 1, 6, 0, TopBottom.None, null, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "You need at least one die!\r\nParameter name: numberOfDice\r\nActual value was 0.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenNumOfDiceIsLessThanOne()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 0, 6, 0, TopBottom.None, null, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "A die needs at least two sides.\r\nParameter name: numberOfSides\r\nActual value was 1.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenNumOfSidesIsLessThanTwo()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 1, 0, TopBottom.None, null, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "An invalid Enum value has been passed.\r\nParameter name: topBottom\r\nActual value was 99.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenTopBottomIsInvalid()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, (TopBottom)99, null, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Have a top bottom number but does not specify if is the top 1 rolls or bottom 1 rolls.\r\nParameter name: topBottomNumber\r\nActual value was 1.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenTopBottomIsNoneButHasTopBottomNumber()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, TopBottom.None, 1, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Top-Bottom number cannot be null when you specify to take the top or bottom amount of numbers.\r\nParameter name: topBottomNumber")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentNullExceptionWhenTopBottomIsNotNoneButDoesNotHaveATopBottomNumber()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, TopBottom.Top, null, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Cannot take Top 2 rolls only have 1 dice.\r\nParameter name: topBottomNumber\r\nActual value was 2.")]
        public void DnDFormulaCalc_Constructor_Top_ThrowsArugmentInvalidExceptionWhenTopBottomNumberIsGreaterThanNumberOfDice()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, TopBottom.Top, 2, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Cannot take Bottom 2 rolls only have 1 dice.\r\nParameter name: topBottomNumber\r\nActual value was 2.")]
        public void DnDFormulaCalc_Constructor_Bottom_ThrowsArugmentInvalidExceptionWhenTopBottomNumberIsGreaterThanNumberOfDice()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, TopBottom.Bottom, 2, null, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Min number is greater than number of sides.\r\nParameter name: minNumber\r\nActual value was 7.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenMinValueIsGreaterThanNumberOfSides()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, TopBottom.None, null, 7, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Max number is greater than number of sides.\r\nParameter name: maxNumber\r\nActual value was 7.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenMaxValueIsGreaterThanNumberOfSides()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 6, 0, TopBottom.None, null, null, 7);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentInvalidException), "Min number is greater than max number.\r\nParameter name: minNumber\r\nActual value was 10.")]
        public void DnDFormulaCalc_Constructor_ThrowsArugmentInvalidExceptionWhenMaxValueIsLessThanMinValue()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 1, 20, 0, TopBottom.None, null, 10, 7);

            // Assert
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor_CanCreateObject()
        {
            // Arrange

            // Act
            new DnDFormulaCalc(new UTRandom(1), 4, 20, 0, TopBottom.Top, 3, 2, 18);

            // Assert
        }

        #endregion

        #region Calc Tests

        [TestMethod]
        public void DnDFormulaCalc_Calc_RollsD6()
        {
            // Arrange
            var rand = new UTRandom(1, 2, 3, 4, 5, 6);
            var calc = new DnDFormulaCalc(rand, 1, 6, 0, TopBottom.None, null, null, null);

            // Act
            var result = calc.Calc();

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Calc_RollsD20WithMinAndMaxValues()
        {
            // Arrange
            var rand = new UTRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);
            var calc = new DnDFormulaCalc(rand, 1, 20, 0, TopBottom.None, null, 10, 15);

            // Act
            var result = calc.Calc();

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Calc_Roll4D6KeepTopThree()
        {
            // Arrange
            var rand = new UTRandom(6, 3, 4, 5, 2, 3, 4, 5);
            var calc = new DnDFormulaCalc(rand, 4, 6, 0, TopBottom.Top, 3, null, null);

            // Act
            var result = calc.Calc();

            // Assert
            Assert.AreEqual(result, 15);
        }

        [TestMethod]
        public void DnDFormulaCalc_Calc_Roll4D6KeepBottomThree()
        {
            // Arrange
            var rand = new UTRandom(6, 3, 4, 5, 2, 3, 4, 5);
            var calc = new DnDFormulaCalc(rand, 4, 6, 0, TopBottom.Bottom, 3, null, null);

            // Act
            var result = calc.Calc();

            // Assert
            Assert.AreEqual(result, 12);
        }

        #endregion

        #region Stat Tests

        [TestMethod]
        public void DnDFormulaCalc_Stat_ReturnsNullWhenHasNotRolled()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 1, 6, 0, TopBottom.None, null, null, null);

            // Act
            var result = calc.Stats(false);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_ReturnsInfoOfOutcome()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 1, 6, 0, TopBottom.None, null, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(false);

            // Assert
            Assert.AreEqual("5: 5", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_ReturnsInfoOfOutcomeOfMultipleRolls()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 2, 6, 0, TopBottom.None, null, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(false);

            // Assert
            Assert.AreEqual("8: 5, 3", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_ReturnsInfoOfOutcomeOfTopThree()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 4, 6, 0, TopBottom.Top, 3, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(false);

            // Assert
            Assert.AreEqual("14: 6, 5, 3, [2]", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_ReturnsInfoOfOutcomeOfBottomThreeOutOf4()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 4, 6, 0, TopBottom.Bottom, 3, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(false);

            // Assert
            Assert.AreEqual("10: 2, 3, 5, [6]", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_ReturnsInfoOfOutcomeOfBottomThreeOutOf5()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5, 1, 2, 4, 1, 3, 2, 2);
            var calc = new DnDFormulaCalc(rand, 5, 6, 0, TopBottom.Bottom, 3, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(false);

            // Assert
            Assert.AreEqual("10: 2, 3, 5, [5, 6]", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_OutcomeIsPartofList()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 1, 6, 0, TopBottom.None, null, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(true);

            // Assert
            Assert.AreEqual("(5)", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_OutcomeIsPartofListMulitpleRolls()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 2, 6, 0, TopBottom.None, null, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(true);

            // Assert
            Assert.AreEqual("8: (5, 3)", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Stat_OutcomeIsPartofListOfTopThreeRoll()
        {
            // Arrange
            var rand = new UTRandom(5, 3, 6, 2, 5);
            var calc = new DnDFormulaCalc(rand, 4, 6, 0, TopBottom.Top, 3, null, null);
            calc.Calc();

            // Act
            var result = calc.Stats(true);

            // Assert
            Assert.AreEqual("14: (6, 5, 3, [2])", result);
        }

        #endregion

    }

}
