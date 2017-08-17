using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.Exceptions;
using JBoyerLibaray.UnitTests;

namespace JBoyerLibaray.DnDDiceRoller
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DnDFormulaCalcTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentNullWhenRandomIsNull()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(null, 1, 6, 0, TopBottom.Not, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenNumOfDiceIsLessThanOne()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 0, 6, 0, TopBottom.Not, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenSideOfDiceIsLessThanTwo()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 1, 0, TopBottom.Not, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenInvalidEnumValueIsPassed()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, (TopBottom)1000, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenTopBottomIsNotButTopBottomNumberHasValue()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Not, 5, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentNullWhenTopBottomIsNotNotButTopBottomNumberDoesNotHaveValueTop()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Top, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentNullWhenTopBottomIsNotNotButTopBottomNumberDoesNotHaveValueBottom()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Bottom, null, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenTopBottomNumberIsGreaterThenNumOfDiceTop()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Top, 5, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenTopBottomNumberIsGreaterThenNumOfDiceBottom()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Bottom, 5, null, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenMinNumberIsGreaterThanNumOfSides()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Not, null, 10, null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenMaxNumberIsGreaterThanNumOfSides()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Not, null, null, 10);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentInvalidException))]
        public void DnDFormulaCalc_ConstructorThrowsArgumentInvalidWhenMinNumberIsGreaterThanMaxNumber()
        {
            // Arrange


            // Act
            new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Not, null, 3, 2);

            // Assert
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor1d6()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 1, 6, 0, TopBottom.Not, null, null, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor1d6Plus5()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 1, 6, 5, TopBottom.Not, null, null, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor3h4d6()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 4, 6, 0, TopBottom.Top, 3, null, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor3l4d6()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 4, 6, 0, TopBottom.Bottom, 3, null, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor3m4d6()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 4, 6, 0, TopBottom.Not, null, 3, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor3M4d6()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 4, 6, 0, TopBottom.Not, null, null, 3);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_Constructor3m3M4d6()
        {
            // Arrange


            // Act
            var result = new DnDFormulaCalc(new Random(), 4, 6, 0, TopBottom.Not, null, 3, 3);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_CalcReturnsCorrectValuesWhen3h4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1,2,3,4,5,6);
            var roller = new DnDFormulaCalc(random, 4, 6, 0, TopBottom.Top, 3, null, null);

            // Act
            var result = roller.Calc();

            // Assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void DnDFormulaCalc_CalcReturnsCorrectValuesWhen3l4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 4, 6, 0, TopBottom.Bottom, 3, null, null);

            // Act
            var result = roller.Calc();

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void DnDFormulaCalc_CalcReturnsCorrectValuesWhen3m4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 4, 6, 0, TopBottom.Not, null, 3, null);

            // Act
            var result = roller.Calc();

            // Assert
            Assert.AreEqual(18, result);
        }

        [TestMethod]
        public void DnDFormulaCalc_CalcReturnsCorrectValuesWhen5m7M4d10()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            var roller = new DnDFormulaCalc(random, 4, 10, 0, TopBottom.Not, null, 5, 7);

            // Act
            var result = roller.Calc();

            // Assert
            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void DnDFormulaCalc_StatsReturnsNullIfNotRolledYet()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 1, 6, 0, TopBottom.Not, null, null, null);

            // Act
            var result = roller.Stats(false);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DnDFormulaCalc_StatsReturnsInfo1d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 1, 6, 0, TopBottom.Not, null, null, null);
            roller.Calc();

            // Act
            var result = roller.Stats(false);

            // Assert
            Assert.AreEqual("1: 1", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_StatsReturnsInfo4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 4, 6, 0, TopBottom.Not, null, null, null);
            roller.Calc();

            // Act
            var result = roller.Stats(false);

            // Assert
            Assert.AreEqual("10: 1, 2, 3, 4", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_StatsReturnsInfo3h4d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 4, 6, 0, TopBottom.Top, 3, null, null);
            roller.Calc();

            // Act
            var result = roller.Stats(false);

            // Assert
            Assert.AreEqual("9: 4, 3, 2, [1]", result);
        }

        [TestMethod]
        public void DnDFormulaCalc_StatsReturnsInfo3h6d6()
        {
            // Arrange
            var random = new UnitTestableRandom(1, 2, 3, 4, 5, 6);
            var roller = new DnDFormulaCalc(random, 6, 6, 0, TopBottom.Top, 3, null, null);
            roller.Calc();

            // Act
            var result = roller.Stats(false);

            // Assert
            Assert.AreEqual("15: 6, 5, 4, [3, 2, 1]", result);
        }
    }
}
