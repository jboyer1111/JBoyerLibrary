//using JBoyerLibaray.UnitTests.ControllerHelpers;
//using JBoyerLibaray.UnitTests.DataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace JBoyer.Testing
//{
//    [ExcludeFromCodeCoverage]
//    public static class UnitTestHelper
//    {

//        #region Serialization/Deserialization

//        public static Stream Serialize(object source)
//        {
//            IFormatter formatter = new BinaryFormatter();
//            var stream = new MemoryStream();
//            formatter.Serialize(stream, source);
//            return stream;
//        }

//        public static T Deserialize<T>(Stream stream)
//        {
//            IFormatter formatter = new BinaryFormatter();
//            stream.Position = 0;
//            return (T)formatter.Deserialize(stream);
//        }

//        public static T Clone<T>(object source)
//        {
//            return Deserialize<T>(Serialize(source));
//        }

//        #endregion

//        #region Set Up Controller For Unit Tests 

//        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes)
//        {
//            controller.SetUpForUnitTests(routes, false);
//        }

//        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, bool isAjax)
//        {
//            controller.SetUpForUnitTests(routes, null, isAjax);
//        }

//        public static void SetUpForUnitTests(this Controller controller, RouteCollection routes, IPrincipal user, bool isAjax)
//        {
//            var controllerContext = new FakeControllerContext(controller, isAjax);

//            controllerContext.LoginUser(user);

//            controller.ControllerContext = controllerContext;
//            controller.Url = new UrlHelper(new RequestContext(controllerContext.HttpContext, new RouteData()), routes);
//        }

//        #endregion

//        #region Create ExceptionContext

//        public static ExceptionContext CreateExceptionContext(this Controller controller, string action, Exception exception)
//        {
//            return controller.CreateExceptionContext(action, exception, false);
//        }

//        public static ExceptionContext CreateExceptionContext(this Controller controller, string action, Exception exception, bool isAjax)
//        {
//            var fakeControllerContext = new FakeControllerContext(controller, isAjax);
//            fakeControllerContext.AddRouteData("controller", controller.GetType().Name.Replace("Controller", ""));
//            fakeControllerContext.AddRouteData("action", action);

//            return new ExceptionContext(fakeControllerContext, exception);
//        }

//        #endregion

//        #region Html Helper

//        public static HtmlHelper CreateHtmlHelper(this Controller controller)
//        {
//            var viewContext = new ViewContext(
//                controller.ControllerContext,
//                new FakeView(),
//                controller.ViewData,
//                new TempDataDictionary(),
//                new StreamWriter(new MemoryStream())
//            );

//            return new HtmlHelper(viewContext, new FakeViewDataContainer() { ViewData = controller.ViewData }, controller.Url.RouteCollection);
//        }

//        #endregion

//    }
//}
