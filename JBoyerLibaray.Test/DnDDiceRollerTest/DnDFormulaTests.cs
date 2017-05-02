using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.DnDDiceRoller;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Test.DnDDiceRollerTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DnDFormulaTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotValidFunctionsThrowsArugmentException()
        {
            //Arrange

            //Act
            DnDFormula result = new DnDFormula("3d4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManylsThrowsArugmentException()
        {
            //Arrange

            //Act
            DnDFormula result = new DnDFormula("3l3l4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManyhsThrowsArugmentException()
        {
            //Arrange

            //Act
            DnDFormula result = new DnDFormula("3h3h4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManymsThrowsArugmentException()
        {
            //Arrange

            //Act
            DnDFormula result = new DnDFormula("3m3m4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManyMsThrowsArugmentException()
        {
            //Arrange

            //Act
            DnDFormula result = new DnDFormula("3M3M4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FunctionHaslandhThrowsArugmentException()
        {
            //Arrange

            //Act
            DnDFormula result = new DnDFormula("3l3h4d6");

            //Assert
            //Expection is thrown
        }

        [TestMethod]
        public void AverageIsCloseEnoughToExpectedAverage()
        {
            //Arrange
            List<int> results = new List<int>(1000);
            DnDFormula formula = new DnDFormula("1d6");

            //Act
            for (int i = 0; i < 1000; i++)
            {
                results.Add(formula.Roll());
            }

            //Asert
            var mean = results.Average();
            Assert.IsTrue(3 < mean && mean < 4);
        }

        [TestMethod]
        public void CheckMultiDiceRoll()
        {
            //Arrange
            List<int> results = new List<int>(1000);
            DnDFormula formula = new DnDFormula("2d6");

            //Act
            for (int i = 0; i < 1000; i++)
            {
                results.Add(formula.Roll());
            }

            //Asert
            var mean = results.Average();
            Assert.IsTrue(6 < mean && mean < 8);
        }

        [TestMethod]
        public void CheckMultiDiceRoll2()
        {
            //Arrange
            List<int> results = new List<int>(1000);
            DnDFormula formula = new DnDFormula("1d6 + 1d4");

            //Act
            for (int i = 0; i < 1000; i++)
            {
                results.Add(formula.Roll());
            }

            //Asert
            var mean = results.Average();
            Assert.IsTrue(5 < mean && mean < 7);
        }
    }
}
