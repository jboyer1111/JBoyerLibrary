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
        public FakeControllerContext(ControllerBase controller) : this(controller, false) { }

        public FakeControllerContext(ControllerBase controller, bool isAjaxRequest) : base(new FakeHttpContext(isAjaxRequest), new RouteData(), controller)
        {
            
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
