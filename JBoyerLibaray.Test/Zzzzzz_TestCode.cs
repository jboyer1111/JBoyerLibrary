using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.Exceptions;
using JBoyerLibaray.UnitTests;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using JBoyerLibaray.Extensions;
using System.Security.Cryptography;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Zzzzzz_TestCode
    {
        [TestMethod]
        public void Zzzzzz_TestMethodOne()
        {
            JBoyerRandom.SetRandomMode(JBoyerRandomMode.CryptographyRandomNumberGenerator);

            var rand = new JBoyerRandom();

            var test = new List<object>();

            test.Add(rand.Next());
            test.Add(rand.Next(5));
            test.Add(rand.Next(3, 4));
            test.Add(rand.NextDouble());
        }
    }
}
