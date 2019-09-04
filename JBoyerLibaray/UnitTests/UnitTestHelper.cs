using JBoyerLibaray.UnitTests.ControllerHelpers;
using JBoyerLibaray.UnitTests.DataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public static class UnitTestHelper
    {

        #region Serialization/Deserialization

        public static Stream Serialize(object source)
        {
            IFormatter formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
        }

        public static T Deserialize<T>(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        public static T Clone<T>(object source)
        {
            return Deserialize<T>(Serialize(source));
        }

        #endregion

        #region Convert List Into Data Reader

        public static EnumerableDataReader ToDataReader<T>(this IEnumerable<T> collection)
        {
            return new EnumerableDataReader(collection);
        }

        public static EnumerableDataReader ToDataReader<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            var type = EnumerableDataReader.CalculateType(collection);

            return new EnumerableDataReader(collection.Where(predicate), type);
        }

        #endregion

        #region Set Up Controller For Unit Tests 

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

        #endregion

        #region Create ExceptionContext

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

        #endregion

        #region Sql Connection

        public static T GetParam<T>(this IDataParameterCollection paramters, string name) where T : class
        {
            return paramters
                .Cast<IDataParameter>()
                .Where(p => String.Equals(p.ParameterName, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault() as T;
        }

        #endregion

        #region Html Helper

        public static HtmlHelper CreateHtmlHelper(this Controller controller)
        {
            var viewContext = new ViewContext(
                controller.ControllerContext,
                new FakeView(),
                controller.ViewData,
                new TempDataDictionary(),
                new StreamWriter(new MemoryStream())
            );

            return new HtmlHelper(viewContext, new FakeViewDataContainer() { ViewData = controller.ViewData }, controller.Url.RouteCollection);
        }

        #endregion

    }
}
