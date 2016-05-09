using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JBoyerLibaray.ImageHelpers;
using System.Drawing;
using System.IO;
using JBoyerLibaray.Extensions;
using System.Drawing.Imaging;

namespace JBoyerLibaray.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> tests = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                tests.Add(ThueMorse.GetSquenceLengthOf(i));
            }
        }

        [TestMethod]
        public void TestColors()
        {
            ImageTransparencyHelper imageHelper = new ImageTransparencyHelper();
            string imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ScrumbleBeeOnTransparentBackground.png");

            Image image = Image.FromFile(imagePath);
            var newImage = ImageTransparencyHelper.RemoveHalfTransparency(image);
            newImage.Save(imagePath.AddToEndOfFilename(".Trans"));
        }

        [TestMethod]
        public void TestCreateImage()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sponge.png");
            MengasSponge.Spongeify.CreateImage(8).Save(filePath, ImageFormat.Png);
        }
    }
}
