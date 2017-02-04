using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace JBoyerLibaray.UnitTests
{
    public static class UnitTestHelper
    {
        //public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, bool isAjax)
        //{   
        //    var controllerContext = new FakeControllerContext(controller, isAjax);

        //    controller.ControllerContext = controllerContext;
        //    controller.Url = new UrlHelper(new RequestContext(controllerContext.HttpContext, new RouteData()), routes);
        //}

        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, IPrincipal user, bool isAjax)
        {
            var controllerContext = new FakeControllerContext(controller, isAjax);

            controllerContext.LoginUser(user);

            controller.ControllerContext = controllerContext;
            controller.Url = new UrlHelper(new RequestContext(controllerContext.HttpContext, new RouteData()), routes);
        }

        public static IDataReader MockIDataReader<T>(IEnumerable<T> items)
        {
            // This var stores current position in 'ojectsToEmulate' list
            int pointer = -1;

            List<T> itemsIndexable = items.ToList();

            var mockDataReader = new Mock<IDataReader>();
            
            mockDataReader.Setup(x => x.Read())
                // Return 'True' while list still has an item
                .Returns(() => pointer < itemsIndexable.Count - 1)
                // Go to next position
                .Callback(() => pointer++);

            //mockDataReader.Setup(x => x[It.IsAny<string>()])
            //    // Again, use lazy initialization via lambda expression
            //    .Returns<string>(s => itemsIndexable[pointer]);

            return mockDataReader.Object;
        }
    }
}
