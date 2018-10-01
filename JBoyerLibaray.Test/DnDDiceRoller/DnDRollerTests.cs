using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JBoyerLibaray.DnDDiceRoller
{
    [TestClass]
    public class DnDRollerTests
    {
        [TestMethod]
        public void DnDRoller_Roll_OnlyIntReturnsItslef()
        {
            // Arrange
            var dndRoller = new DnDRoller("1");
            
            // Act
            var result = dndRoller.Roll();

            // Assert
            Assert.AreEqual(1, result.Total);
            Assert.AreEqual("1", result.DiceResult);
        }

        //[TestMethod]
        //public void DnDRoller_Roll_ReturnsInt()
        //{
        //    // Act
        //    var result = DnDRoller.Roll();

        //    // Assert
        //    Assert.AreEqual(1, result.Total);
        //}

        [TestMethod]
        public void DnDRoller_Roll_()
        {

        }
    }
}
