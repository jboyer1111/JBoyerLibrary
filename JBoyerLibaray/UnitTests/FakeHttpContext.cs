using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests
{
    public class FakeHttpContext : HttpContextBase
    {
        HttpRequestBase _request;
        HttpResponseBase _response;
        HttpServerUtilityBase _server;
        HttpSessionStateBase _session;

        public FakeHttpContext(bool isAjaxRequest)
        {
            _request = new FakeHttpRequest(isAjaxRequest);
            _response = new FakeHttpResponse();
            _server = new FakeHttpServerUtility();
            _session = new FakeHttpSessionState();
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
    }
}
