using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace JBoyerLibaray.Web
{
    public class WebDownloader : IWebDownloader
    {
        #region Private Variables

        protected static readonly Regex _ipAddressRegex;
        protected static readonly Regex _errorHTMLheader;
        protected static int _maxWaitTimeInHalfSeconds;

        protected IWebPageCache _webPageCache;
        protected bool _isUsingDefaultProxy;
        protected bool _isUsingProxy;
        protected string _proxyIpAddress;
        protected int _proxyPortNumber;
        protected CookieContainer _cookieContainer;
        protected bool _useCache = true;

        #endregion

        #region Public Variables

        public static int MaxWaitTimeInHalfSeconds
        {
            get
            {
                return _maxWaitTimeInHalfSeconds;
            }
            set
            {
                if (value != _maxWaitTimeInHalfSeconds)
                {
                    _maxWaitTimeInHalfSeconds = value;
                }
            }
        }

        public const string HttpRequestUserAgentString = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
        public const string HttpRequestContentTypeForPost = "application/x-www-form-urlencoded";

        public bool IsUsingDefaultProxy
        {
            get
            {
                return _isUsingDefaultProxy;
            }
            set
            {
                if (_isUsingDefaultProxy == value) { return; }
                _isUsingDefaultProxy = value;
            }
        }

        public bool IsUsingProxy
        {
            get
            {
                return _isUsingProxy;
            }
            set
            {
                if (_isUsingProxy == value) { return; }
                _isUsingProxy = value;
            }
        }

        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public string ProxyIpAddress
        {
            get
            {
                return _proxyIpAddress;
            }
            set
            {
                if (value == null)
                {
                    throw ExceptionHelper.CreateArgumentNullException(() => ProxyIpAddress);
                }

                string newProxyIpAddress = value.Trim();

                if (String.IsNullOrWhiteSpace(newProxyIpAddress) == true)
                {
                    throw ExceptionHelper.CreateArgumentException(() => ProxyIpAddress, "Cannot be an empty string or only white space.");
                }

                bool isMatch = _ipAddressRegex.IsMatch(newProxyIpAddress);
                if (isMatch == false)
                {
                    throw ExceptionHelper.CreateArgumentException(() => ProxyIpAddress, "Can only be an IP address between 1.0.0.0 and 255.255.255.255.");
                }

                if (_proxyIpAddress == newProxyIpAddress) { return; }

                _proxyIpAddress = newProxyIpAddress;
            }
        }

        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public int ProxyPortNumber
        {
            get
            {
                return _proxyPortNumber;
            }
            set
            {
                if (value < 0 || value > 65535)
                {
                    throw ExceptionHelper.CreateArgumentOutOfRangeException(
                        () => ProxyPortNumber,
                        "Can only be in the range of 0 and 65535",
                        value
                    );
                }

                if (_proxyPortNumber == value) { return; }

                _proxyPortNumber = value;
            }
        }

        public IWebPageCache WebPageCache
        {
            get
            {
                return _webPageCache;
            }
        }

        #endregion

        #region Constructor

        static WebDownloader()
        {
            _ipAddressRegex = new Regex(
                @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])"
                + @"(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"
                , RegexOptions.Compiled);

            _errorHTMLheader = new Regex("<TITLE>Error</TITLE>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public WebDownloader() : this(null) { }

        public WebDownloader(IWebPageCache webPageCache)
        {
            _webPageCache = webPageCache;

            _isUsingDefaultProxy = false;
            _isUsingProxy = false;
            _proxyIpAddress = String.Empty;
            _proxyPortNumber = -1;
            _cookieContainer = new CookieContainer();
        }

        #endregion

        #region Private Methods

        /// <exception cref="System.ArgumentException"><paramref name="formVariables"/> contains duplicate form variable keys.</exception>
        private static string validateAndPrepareFormVariablesText(IEnumerable<Tuple<string, string>> formVariables, Func<string, string> formEncodeDelegate)
        {
            string result = "";

            if (formVariables != null)
            {
                formVariables = formVariables.Where(item => item != null).Where(item => String.IsNullOrWhiteSpace(item.Item1) == false);

                var areAnyFormVariablesDuplicated = formVariables.GroupBy(item => item.Item1).Where(item => item.Count() > 1).Any();
                if (areAnyFormVariablesDuplicated == true)
                {
                    throw ExceptionHelper.CreateArgumentException(() => formVariables, "Contains duplicate form variable keys.");
                }

                if (formEncodeDelegate == null)
                {
                    formEncodeDelegate = value => value;
                }

                var encodedFormVariables =
                    formVariables
                    .Select(item => new { Key = formEncodeDelegate(item.Item1), Value = formEncodeDelegate(item.Item2), });

                var formVariablePairs = encodedFormVariables.Select(item => String.Format("{0}={1}", item.Key, item.Value));

                result = String.Join("&", formVariablePairs);
            }

            return result;
        }

        /// <summary>
        ///     Download the web page code from a HTTP request.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Exception"></exception>
        private string downloadHtml(HttpWebRequest httpWebRequest, string requestMethodType, params Tuple<string, string>[] formVariables)
        {
            if (httpWebRequest == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => httpWebRequest);
            }

            string result = null;
            // The USPTO web site will return a 403 Forbidden error is this isn't set to a value.
            httpWebRequest.UserAgent = HttpRequestUserAgentString;

            httpWebRequest.CookieContainer = _cookieContainer;
            httpWebRequest.AllowAutoRedirect = true;

            bool wasRequesetMethodTypeSpecified = String.IsNullOrWhiteSpace(requestMethodType) == false;
            if (wasRequesetMethodTypeSpecified == true)
            {
                httpWebRequest.Method = requestMethodType.Trim();

                bool isPostRequest = String.Equals(httpWebRequest.Method, WebRequestMethods.Http.Post, StringComparison.OrdinalIgnoreCase);
                if (isPostRequest == true)
                {
                    httpWebRequest.ContentType = HttpRequestContentTypeForPost;

                    string formVariablesText = validateAndPrepareFormVariablesText(formVariables, value => Uri.EscapeDataString(value));

                    bool wereFormVariablesSpecified = String.IsNullOrWhiteSpace(formVariablesText) == false;
                    if (wereFormVariablesSpecified == true)
                    {
                        byte[] formVariablesBytes = Encoding.ASCII.GetBytes(formVariablesText);

                        httpWebRequest.ContentLength = formVariablesBytes.Length;

                        using (Stream stream = httpWebRequest.GetRequestStream())
                        {
                            stream.Write(formVariablesBytes, 0, formVariablesBytes.Length);
                            stream.Flush();
                            stream.Close();
                        }
                    }
                }
            }

            // Set timeout to 5min.
            httpWebRequest.Timeout = 300000;

            // Execute the request.
            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var stream = httpWebResponse.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                {
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                    stream.Close();
                }

                httpWebResponse.Close();

                if (_useCache && _webPageCache != null)
                {
                    // Using Original String gives us what we put into the url and Not what the URI cleaned up to.
                    // Some urls may get cleaned up. If they do then if we try them later they will not use cache
                    // because they don't know they are in it.
                    _webPageCache.AddPageToCache(httpWebRequest.RequestUri.OriginalString, result);
                }
            }


            return result;
        }

        private string cleanHTMLEscapedIssuesDifferences(string html)
        {
            Regex replacer = new Regex("&amp;|&quot;|&apos;|&lt;|&gt;");
            return replacer.Replace(html, m =>
            {
                switch (m.ToString())
                {
                    case "&amp;":
                        return "&";
                    case "&quot;":
                        return "\"";
                    case "&apos;":
                        return "'";
                    case "&lt;":
                        return "<";
                    case "&gt;":
                        return ">";
                    default:
                        break;
                }

                return m.ToString();
            });
        }

        #endregion

        #region Public Methods

        public string DownloadHtml(string url, string requestMethodType = WebRequestMethods.Http.Get, params Tuple<string, string>[] formVariables)
        {
            bool usedCache = false;
            return DownloadHtml(url, out usedCache, requestMethodType, formVariables);
        }

        /// <summary>
        ///     Download the web page code for a URL and return it.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.PtoadException"></exception>
        /// <exception cref="System.Exception"></exception>
        public string DownloadHtml(string url, out bool usedCache, string requestMethodType = WebRequestMethods.Http.Get, params Tuple<string, string>[] formVariables)
        {
            if (url == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => url);
            }

            string result = String.Empty;

            url = url.TrimStart();

            // Downloading the web page code from the URL.
            try
            {
                usedCache = false;
                if (_useCache && _webPageCache != null)
                {
                    usedCache = _webPageCache.TryGetPage(url, out result);
                }

                if (!usedCache)
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                    if (url.Substring(0, 5).ToLower() != "https") // Proxy cannot handle HTTPS or SSL requests.
                    {
                        if (_isUsingDefaultProxy == true)
                        {
                            httpWebRequest.Proxy = HttpWebRequest.DefaultWebProxy;
                            httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                        }
                        else if (_isUsingProxy == true)
                        {
                            if (String.IsNullOrEmpty(_proxyIpAddress))
                            {
                                throw new Exception(
                                    String.Format("The {0} property has not been set.", ExceptionHelper.GetMemberName(() => ProxyIpAddress))
                                );
                            }
                            if (_proxyPortNumber == -1)
                            {
                                throw new Exception(
                                    String.Format("The {0} property has not been set.", ExceptionHelper.GetMemberName(() => ProxyPortNumber))
                                );
                            }

                            WebProxy webProxy = new WebProxy(_proxyIpAddress, _proxyPortNumber) { BypassProxyOnLocal = true };
                            httpWebRequest.Proxy = webProxy;

                            // This will automatically get username and password.
                            httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                        }
                    }

                    result = downloadHtml(httpWebRequest, requestMethodType, formVariables);
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Proxy Authentication Required") == true)
                {
                    throw new Exception(
                        String.Format("Could not download the web page code from the URL {0} because proxy authentication is needed.", url),
                        e
                    );
                }
                else
                {
                    throw new Exception(
                        String.Format("Could not download the web page code from the URL {0}.", url),
                        e
                    );
                }
            }

            return result;
        }
        
        public void PauseCache()
        {
            _useCache = false;
        }

        public void StartCache()
        {
            _useCache = true;
        }

        public WebDownloaderFile DownloadFile(string url)
        {
            MemoryStream memoryStream = new MemoryStream();

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            WebResponse response = httpWebRequest.GetResponse();

            response.GetResponseStream().CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new WebDownloaderFile(response.ContentType, memoryStream);
        }

        #endregion
    }
}
