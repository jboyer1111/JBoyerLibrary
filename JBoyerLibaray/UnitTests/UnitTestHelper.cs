using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitTests
{
    public static class UnitTestHelper
    {
        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, bool isAjax = false)
        {   
            var controllerContext = new JBoyerLibaray.UnitTests.FakeContollerContext(controller, isAjax);

            controller.ControllerContext = controllerContext;
            controller.Url = new UrlHelper(new RequestContext(controllerContext.HttpContext, new RouteData()), routes);
        }
    }
}
