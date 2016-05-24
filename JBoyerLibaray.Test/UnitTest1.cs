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
            List<int> tests = new List<int>()
            {
                1,
                1,
                1,
                2,
                2,
                2,
                2,
                3,
                3,
                3,
                3
            };
            

            var shuffled = tests.Shuffle();

            var mode = shuffled.Mode();
        }
    }
}
