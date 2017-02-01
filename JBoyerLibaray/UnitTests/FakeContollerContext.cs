using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JBoyerLibaray.UnitTests
{
    public class FakeContollerContext : ControllerContext
    {
        public FakeContollerContext(ControllerBase controller) : this(controller, false) { }

        public FakeContollerContext(ControllerBase controller, bool isAjaxRequest) : base(new FakeHttpContext(isAjaxRequest), new RouteData(), controller)
        {
            
        }
    }
}
