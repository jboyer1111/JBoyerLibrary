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
            RandomNumberGenerator rand = RandomNumberGenerator.Create("Billy");
        }
    }


    public class TestProp
    {

        private bool _test = false;
        public bool Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
            }
        }

        public override string ToString()
        {
            return Test.ToString();
        }
    }
}
