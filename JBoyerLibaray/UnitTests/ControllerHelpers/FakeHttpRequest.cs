using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace JBoyerLibaray.UnitTests
{

    [ExcludeFromCodeCoverage]
    public class FakeHttpRequest : HttpRequestBase
    {
        private HttpCookieCollection _cookieCollection = new HttpCookieCollection();
        private NameValueCollection _collection;
        private NameValueCollection _form;

        private byte[] _fileBytes = new byte[] { };

        public FakeHttpRequest(bool isAjaxRequest)
        {
            _form = new NameValueCollection();
            _collection = new NameValueCollection();
            _cookieCollection.Add(new HttpCookie("proxyId", String.Empty));
            _cookieCollection.Add(new HttpCookie("username", String.Empty));

            if (isAjaxRequest)
            {
                _collection.Add("X-Requested-With", "XMLHttpRequest");
            }
        }

        public override string this[string key]
        {
            get
            {
                return null;
            }
        }

        public override NameValueCollection Headers
        {

            get
            {
                return _collection;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return new NameValueCollection();
            }
        }

        public override string ApplicationPath
        {
            get
            {
                return "/Test/";
            }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                return "/Test/";
            }
        }

        public override NameValueCollection Form
        {
            get
            {
                return _form;
            }
        }

        public override string RequestType
        {
            get
            {
                return "";
            }
            set
            {
                var temp = value;
            }
        }

        public override Uri Url
        {
            get
            {
                return new Uri("http://localhost/", UriKind.Absolute);
            }
        }

        public override byte[] BinaryRead(int count)
        {
            if (_fileBytes.Length < count)
            {
                return _fileBytes;
            }

            return _fileBytes.Take(count).ToArray();
        }

        public override int ContentLength
        {
            get
            {
                return _fileBytes.Length;
            }
        }

        public void AddFileToRequest(byte[] fileBytes)
        {
            _fileBytes = fileBytes;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookieCollection;
            }
        }

        public override string UserAgent
        {
            get
            {
                return "Unit Test User Agent String";
            }
        }

        public override HttpBrowserCapabilitiesBase Browser
        {
            get
            {
                var factory = new BrowserCapabilitiesFactory();
                var browserCaps = new HttpBrowserCapabilities();
                var hashtable = new Hashtable(180, StringComparer.OrdinalIgnoreCase);
                hashtable[string.Empty] = UserAgent;
                browserCaps.Capabilities = hashtable;
                factory.ConfigureBrowserCapabilities(Headers, browserCaps);
                factory.ConfigureCustomCapabilities(Headers, browserCaps);

                return new HttpBrowserCapabilitiesWrapper(browserCaps);
            }
        }

        public override string RawUrl
        {
            get
            {
                return "/UnitTest/RawUrl";
            }
        }
    }

}
