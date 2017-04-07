using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JBoyerLibaray.UnitTests
{
    public class FakeControllerContext : ControllerContext
    {
        public FakeControllerContext(Controller controller) : this(controller, false) { }

        public FakeControllerContext(Controller controller, bool isAjaxRequest) : base(new FakeHttpContext(controller, isAjaxRequest), new RouteData(), controller)
        {
            
        }

        public void AddRouteData(string key, string value)
        {
            RouteData.Values.Add(key, value);
        }

        public void AddFileToReqeust(byte[] fileBytes)
        {
            var fakeReqeust = HttpContext.Request as FakeHttpRequest;

            fakeReqeust.AddFileToRequest(fileBytes);
        }

        public void LoginUser(IPrincipal user)
        {
            if (user == null || user.Identity == null)
            {
                return;
            }

            if (user.Identity.IsAuthenticated)
            {
                (HttpContext as FakeHttpContext).LoginUser(user);
            }
        }
    }
}
