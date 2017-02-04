using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JBoyerLibaray.ImageHelpers;
using System.Drawing;
using System.IO;
using JBoyerLibaray.Extensions;
using System.Drawing.Imaging;
using JBoyerLibaray.UnitTests;
using System.Web.Mvc;
using System.Web.Routing;

namespace JBoyerLibaray.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new FakeController();

            UnitTestHelper.SetUpForUnitTests(controller, new RouteCollection(), new FakeUser("Bob"), false);
        }


    }

    public class FakeController : Controller
    {

    }
}
