using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
    }
}
