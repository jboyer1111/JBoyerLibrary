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
        public void PluralizeString_Pluraize_AConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A}dorito ...", 1);

            // Assert
            Assert.AreEqual("I ate a dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_AConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A}dorito ...", 2);

            // Assert
            Assert.AreEqual("I ate dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_ANumberConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A-#} dorito ...", 1);

            // Assert
            Assert.AreEqual("I ate a dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_ANumberConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {A-#} dorito ...", 2);

            // Assert
            Assert.AreEqual("I ate 2 dorito ...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_AnConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN}int...", 1);

            // Assert
            Assert.AreEqual("I ate an int...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_AnConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN}int...", 2);

            // Assert
            Assert.AreEqual("I ate int...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_AnNumberConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN-#} int...", 1);

            // Assert
            Assert.AreEqual("I ate an int...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_AnNumberConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("I ate {AN-#} int...", 2);

            // Assert
            Assert.AreEqual("I ate 2 int...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_IsAreConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("There {IS-ARE} problem...", 1);

            // Assert
            Assert.AreEqual("There is problem...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_IsAreConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("There {IS-ARE} problem...", 2);

            // Assert
            Assert.AreEqual("There are problem...", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_SConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Card{S}", 1);

            // Assert
            Assert.AreEqual("Card", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_SConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Card{S}", 2);

            // Assert
            Assert.AreEqual("Cards", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_EsConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Class{ES}", 1);

            // Assert
            Assert.AreEqual("Class", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_EsConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Class{ES}", 2);

            // Assert
            Assert.AreEqual("Classes", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_YiesConvertsOneCorrectly()
        {
            // Arrange

            // Act
            var result = PluralizeString.Pluralize("Part{Y-IES}", 1);

            // Assert
            Assert.AreEqual("Party", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_YiesConvertsTwoCorrectly()
        {
            // Arrange

            // Act
            var result = PluralizeString.Pluralize("Part{Y-IES}", 2);

            // Assert
            Assert.AreEqual("Parties", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_FvesConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Wol{F-VES}", 1);

            // Assert
            Assert.AreEqual("Wolf", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_FvesConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("Wol{F-VES}", 2);

            // Assert
            Assert.AreEqual("Wolves", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_CustomConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Die-Dice}", 1);

            // Assert
            Assert.AreEqual("Die", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_CustomConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Die-Dice}", 2);

            // Assert
            Assert.AreEqual("Dice", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NumberLogicConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{# Die-# Dice}", 1);

            // Assert
            Assert.AreEqual("1 Die", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NumberLogicConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{# Die-# Dice}", 2);

            // Assert
            Assert.AreEqual("2 Dice", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NumberSignEscapedConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{## Die-## Dice}", 1);

            // Assert
            Assert.AreEqual("# Die", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NumberSignEscapedConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{## Die-## Dice}", 2);

            // Assert
            Assert.AreEqual("# Dice", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_ZeroLogicConvertsZeroCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Zero-Single-Multi}", 0);

            // Assert
            Assert.AreEqual("Zero", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_ZeroLogicConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Zero-Single-Multi}", 1);

            // Assert
            Assert.AreEqual("Single", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_ZeroLogicConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Zero-Single-Multi}", 2);

            // Assert
            Assert.AreEqual("Multi", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_CustomNumberZeroLogicConvertsZeroCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Zero logic occured actual count (#)-Single logic occured actual count (#)-Multi logic occured actual count (#)}", 0);

            // Assert
            Assert.AreEqual("Zero logic occured actual count (0)", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_CustomNumberZeroLogicConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Zero logic occured actual count (#)-Single logic occured actual count (#)-Multi logic occured actual count (#)}", 1);

            // Assert
            Assert.AreEqual("Single logic occured actual count (1)", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_CustomNumberZeroLogicConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{Zero logic occured actual count (#)-Single logic occured actual count (#)-Multi logic occured actual count (#)}", 2);

            // Assert
            Assert.AreEqual("Multi logic occured actual count (2)", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NoNumberNumberLogicConvertsZeroCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{No-#-#} Federal Tax Lien{S}", 0);

            // Assert
            Assert.AreEqual("No Federal Tax Liens", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NoNumberNumberLogicLogicConvertsOneCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{No-#-#} Federal Tax Lien{S}", 1);

            // Assert
            Assert.AreEqual("1 Federal Tax Lien", result);
        }

        [TestMethod]
        public void PluralizeString_Pluraize_NoNumberNumberLogicLogicConvertsTwoCorrectly()
        {
            // Arrange


            // Act
            var result = PluralizeString.Pluralize("{No-#-#} Federal Tax Lien{S}", 2);

            // Assert
            Assert.AreEqual("2 Federal Tax Liens", result);
        }
    }
}
