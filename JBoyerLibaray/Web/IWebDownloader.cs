using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace JBoyerLibaray.Web
{
    public interface IWebDownloader
    {
        string DownloadHtml(string url, string requestMethodType = WebRequestMethods.Http.Get, params Tuple<string, string>[] formVariables);

        string DownloadHtml(string url, out bool usedCache, string requestMethodType = WebRequestMethods.Http.Get, params Tuple<string, string>[] formVariables);

        bool IsUsingDefaultProxy { get; set; }

        bool IsUsingProxy { get; set; }

        string ProxyIpAddress { get; set; }

        int ProxyPortNumber { get; set; }

        void PauseCache();

        void StartCache();

        WebDownloaderFile DownloadFile(string url);

        IWebPageCache WebPageCache { get; }
    }
}
