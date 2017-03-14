using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests
{
    public class FakeHttpContext : HttpContextBase
    {
        private HttpRequestBase _request;
        private HttpResponseBase _response;
        private HttpServerUtilityBase _server;
        private HttpSessionStateBase _session;
        private IPrincipal _user;

        public FakeHttpContext(Controller controller, bool isAjaxRequest)
        {
            _request = new FakeHttpRequest(isAjaxRequest);
            _response = new FakeHttpResponse();
            _server = new FakeHttpServerUtility(controller);
            _session = new FakeHttpSessionState();
            _user = FakeUser.Anonymous();
        }

        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return base.Response;
            }
        }

        public override HttpServerUtilityBase Server
        {
            get
            {
                return _server;
            }
        }

        public override HttpSessionStateBase Session
        {
            get
            {
                return _session;
            }
        }

        public override object GetService(Type serviceType)
        {
            var returnObj = DependencyResolver.Current.GetService(serviceType);
            return returnObj;
        }

        public override IPrincipal User
        {
            get
            {
                return _user;
            }
        }

        public void LoginUser(IPrincipal user)
        {
            if (user == null || user.Identity == null)
            {
                return;
            }

            if (user.Identity.IsAuthenticated)
            {
                _user = user;
            }
        }
    }
}
