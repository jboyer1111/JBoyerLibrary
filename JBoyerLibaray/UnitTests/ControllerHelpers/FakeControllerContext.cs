using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class FakeControllerContext : ControllerContext
    {
        public FakeControllerContext(Controller controller) : this(controller, false) { }

        public FakeControllerContext(Controller controller, bool isAjaxRequest)
            : base(new FakeHttpContext(controller, isAjaxRequest), new RouteData(), controller)
        {
            
        }

        public void AddRouteData(string key, string value)
        {
            RouteData.Values.Add(key, value);
        }

        public void AddFileToReqeust(byte[] fileBytes)
        {
            // Make sure HttpRequest is expected type at the moment
            if (HttpContext.Request is FakeHttpRequest)
            {
                (HttpContext.Request as FakeHttpRequest).AddFileToRequest(fileBytes);
            }
        }

        public void LoginUser(IPrincipal user)
        {
            // Make sure HttpContext is expected type at the moment
            if (user?.Identity?.IsAuthenticated ?? false && HttpContext is FakeHttpContext)
            {
                (HttpContext as FakeHttpContext).LoginUser(user);
            }
        }
    }
}
