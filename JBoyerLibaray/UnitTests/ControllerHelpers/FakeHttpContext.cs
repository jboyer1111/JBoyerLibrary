using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class FakeHttpContext : HttpContextBase
    {
        #region Private variables
        
        private HttpRequestBase _request;
        private HttpResponseBase _response;
        private HttpServerUtilityBase _server;
        private HttpSessionStateBase _session;
        private IPrincipal _user;
		private bool _isCustomErrorEnabled = true;

        #endregion

        #region Constructor

        public FakeHttpContext(Controller controller, bool isAjaxRequest)
        {
            _request = new FakeHttpRequest(isAjaxRequest);
            _response = new FakeHttpResponse();
            _server = new FakeHttpServerUtility(controller);
            _session = new FakeHttpSessionState();
            _user = FakeUser.Anonymous();
        }

        #endregion

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
                return _response;
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
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                _user = user;
            }
        }

        public override bool IsCustomErrorEnabled
        {
            get
            {
                return _isCustomErrorEnabled;
            }
        }

        public void SetCustomErrorEnabled(bool value)
        {
            _isCustomErrorEnabled = value;
        }

        public override IDictionary Items
        {
            get
            {
                return new Dictionary<object, object>();
            }
        }

    }
}
