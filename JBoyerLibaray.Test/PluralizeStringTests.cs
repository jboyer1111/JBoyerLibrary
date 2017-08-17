using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PluralizeStringTests
    {
        [TestMethod]
        public void PluralizeString_PluraizeAConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A}dorito ...", 1);

            // Assert
            Assert.AreEqual("I ate a dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeAConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A}dorito ...", 2);

            // Assert
            Assert.AreEqual("I ate dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeANumberConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A-#} dorito ...", 1);

            // Assert
            Assert.AreEqual("I ate a dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeANumberConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A-#} dorito ...", 2);

            // Assert
            Assert.AreEqual("I ate 2 dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeAnConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN}int...", 1);

            // Assert
            Assert.AreEqual("I ate an int...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeAnConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN}int...", 2);

            // Assert
            Assert.AreEqual("I ate int...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeAnNumberConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN-#} int...", 1);

            // Assert
            Assert.AreEqual("I ate an int...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeAnNumberConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN-#} int...", 2);

            // Assert
            Assert.AreEqual("I ate 2 int...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeIsAreConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("There {IS-ARE} problem...", 1);

            // Assert
            Assert.AreEqual("There is problem...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeIsAreConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("There {IS-ARE} problem...", 2);

            // Assert
            Assert.AreEqual("There are problem...", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeSConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Card{S}", 1);

            // Assert
            Assert.AreEqual("Card", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeSConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Card{S}", 2);

            // Assert
            Assert.AreEqual("Cards", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeEsConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Class{ES}", 1);

            // Assert
            Assert.AreEqual("Class", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeEsConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Class{ES}", 2);

            // Assert
            Assert.AreEqual("Classes", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeYiesConvertsOneCorrectly()
        {
            // Arrange

            // Act
            var result = PluralizeString.Pluralize("Part{Y-IES}", 1);

            // Assert
            Assert.AreEqual("Party", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeYiesConvertsTwoCorrectly()
        {
            // Arrange

            // Act
            var result = PluralizeString.Pluralize("Part{Y-IES}", 2);

            // Assert
            Assert.AreEqual("Parties", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeFvesConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Wol{F-VES}", 1);

            // Assert
            Assert.AreEqual("Wolf", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeFvesConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Wol{F-VES}", 2);

            // Assert
            Assert.AreEqual("Wolves", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeCustomConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Die-Dice}", 1);

            // Assert
            Assert.AreEqual("Die", result);
        }

        [TestMethod]
        public void PluralizeString_PluraizeCustomConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Die-Dice}", 2);

            // Assert
            Assert.AreEqual("Dice", result);
        }
    }
}
