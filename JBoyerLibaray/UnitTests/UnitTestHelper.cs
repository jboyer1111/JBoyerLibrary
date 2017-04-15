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
        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes)
        {
            SetUpForUnitTests(controller, routes, false);
        }

        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, bool isAjax)
        {
            SetUpForUnitTests(controller, routes, null, isAjax);
        }

        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, IPrincipal user, bool isAjax)
        {
            var controllerContext = new FakeControllerContext(controller, isAjax);

            controllerContext.LoginUser(user);

            controller.ControllerContext = controllerContext;
            controller.Url = new UrlHelper(new RequestContext(controllerContext.HttpContext, new RouteData()), routes);
        }

        public static EnumerableDataReader ToDataReader<T>(this IEnumerable<T> collection)
        {
            return new EnumerableDataReader(collection);
        }

        public static EnumerableDataReader ToDataReader<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            Type type = EnumerableDataReader.CalculateType(collection);

            return new EnumerableDataReader(collection.Where(predicate), type);
        }

        public static ExceptionContext CreateExceptionContext(this Controller controller, string action, Exception exception)
        {
            return CreateExceptionContext(controller, action, exception, false);
        }

        public static ExceptionContext CreateExceptionContext(this Controller controller, string action, Exception exception, bool isAjax)
        {
            var fakeControllerContext = new FakeControllerContext(controller, isAjax);
            fakeControllerContext.AddRouteData("controller", controller.GetType().Name.Replace("Controller", ""));
            fakeControllerContext.AddRouteData("action", action);

            return new ExceptionContext(fakeControllerContext, exception);
        }
    }
}
