using System;

namespace JBoyerLibaray.Web
{
    public interface IWebPageCache
    {
        void AddPageToCache(string url, string htmlString, DateTime? experationDate = null);

        int Count { get; }

        void EmptyCache();

        string GetPageCacheDirectoryPath { get; }

        string GetPageCacheFileName { get; }

        string GetPageCacheFilePath { get; }

        bool TryGetPage(string url, out string htmlString);

        void RemoveUrlFormCache(string url);

        void AddIgnoredQueryParameter(string name);
    }
}
