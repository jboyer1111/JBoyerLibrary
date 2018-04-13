using JBoyerLibaray.Exceptions;
using JBoyerLibaray.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace JBoyerLibaray.Web
{
    public class WebPageCache : IWebPageCache
    {
        #region Private Variables

        private string _getPageCacheDirectoryPath;
        private string _getPageCacheFilePath;
        private Dictionary<string, PageCache> _urlToWebPageFileNameDict;
        private IFileSystemHelper _fileSystemHandler;
        private const string _getPageCacheFileName = "WebPageCache.xml";
        private List<string> _ignoreQueryParams;

        #endregion

        #region Public Variables

        public string GetPageCacheDirectoryPath { get { return _getPageCacheDirectoryPath; } }

        public string GetPageCacheFilePath { get { return _getPageCacheFilePath; } }

        public string GetPageCacheFileName { get { return _getPageCacheFileName; } }

        public int Count { get { return _urlToWebPageFileNameDict.Count; } }

        #endregion

        #region Constructor

        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.Exception"></exception>
        public WebPageCache(string cacheFolderPath) : this(cacheFolderPath, new FileSystemHelper()) { }

        public WebPageCache(string cacheFolderPath, IFileSystemHelper fileSystemHandler)
        {
            if (String.IsNullOrWhiteSpace(cacheFolderPath))
            {
                throw ExceptionHelper.CreateArgumentNullException(() => cacheFolderPath);
            }

            if (fileSystemHandler == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => fileSystemHandler);
            }

            _ignoreQueryParams = new List<string>();
            _fileSystemHandler = fileSystemHandler;
            _getPageCacheDirectoryPath = cacheFolderPath;
            _getPageCacheFilePath = Path.Combine(_getPageCacheDirectoryPath, _getPageCacheFileName);
            _urlToWebPageFileNameDict = new Dictionary<string, PageCache>();

            // Check to see if Directory Exists and if not create it
            if (!_fileSystemHandler.Directory.Exists(_getPageCacheDirectoryPath))
            {
                try
                {
                    _fileSystemHandler.Directory.CreateDirectory(_getPageCacheDirectoryPath);
                }
                catch (Exception e)
                {
                    throw new Exception("Unable to create directory", e);
                }
            }

            // If file exists load it into the webpage cache
            if (_fileSystemHandler.File.Exists(_getPageCacheFilePath))
            {
                string fileText = _fileSystemHandler.File.ReadAllText(_getPageCacheFilePath);
                XDocument xml = XDocument.Parse(fileText);
                var pages = xml.Root.Elements().ToList();
                foreach (var page in pages)
                {
                    var pageCache = new PageCache();
                    var url = GetXElmentText(page, "URL");
                    var filepath = GetXElmentText(page, "FileName");
                    var dateStr = GetXElmentText(page, "CacheDate");
                    if (String.IsNullOrWhiteSpace(url) || String.IsNullOrWhiteSpace(filepath) || String.IsNullOrWhiteSpace(dateStr))
                    {
                        continue;
                    }
                    pageCache.FilePath = filepath;
                    pageCache.CacheDate = DateTime.Parse(dateStr);

                    var experationDate = GetXElmentText(page, "ExperationDate");
                    if (!String.IsNullOrWhiteSpace(experationDate))
                    {
                        pageCache.ExperationDate = DateTime.Parse(experationDate);
                        if (pageCache.ExperationDate.Value < DateTime.Now)
                        {
                            if (_fileSystemHandler.File.Exists(pageCache.FilePath))
                            {
                                _fileSystemHandler.File.Delete(pageCache.FilePath);
                            }
                            continue;
                        }
                    }

                    _urlToWebPageFileNameDict.Add(url, pageCache);
                }
            }
        }

        private string GetXElmentText(XElement node, string name)
        {
            if (!node.HasElements)
            {
                throw new InvalidDataException(String.Format("Expecting child nodes to be under the node named '{0}' and there was none.", node.Name));
            }

            return node.Elements().Where(e => e.Name == name && !e.HasElements).Select(e => e.Value).FirstOrDefault();
        }

        #endregion

        #region Private Methods

        private void saveXmlFile()
        {
            XDocument xml = new XDocument(new XElement("Pages"));

            foreach (KeyValuePair<string, PageCache> kvPair in _urlToWebPageFileNameDict)
            {
                if (kvPair.Value.ExperationDate < DateTime.Now)
                {
                    continue;
                }

                xml.Root.Add(new XElement("Page",
                    new XElement("URL", kvPair.Key),
                    new XElement("FileName", kvPair.Value.FilePath),
                    new XElement("CacheDate", kvPair.Value.CacheDate),
                    new XElement("ExperationDate", kvPair.Value.ExperationDate)
                ));
            }

            try
            {
                string content = xml.ToString();
                _fileSystemHandler.File.WriteAllText(_getPageCacheFilePath, content);
            }
            catch
            {
                "Swallow this exception.".ToString();
            }
        }

        private string removeIgnoredQueryParams(string url)
        {
            foreach (string name in _ignoreQueryParams)
            {
                url = Regex.Replace(url, String.Format("{0}=.*?&|\\??{0}=.*?$", name), String.Empty);
            }

            return url;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add an item to the web page cache. If one already exists then update it.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void AddPageToCache(string url, string htmlString, DateTime? experationDate = null)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                throw ExceptionHelper.CreateArgumentException(() => url, "Url cannot be null, empty, or white space!");
            }
            if (htmlString == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => htmlString);
            }

            url = removeIgnoredQueryParams(url);

            if (_urlToWebPageFileNameDict.ContainsKey(url))
            {
                try
                {
                    PageCache pageCache = _urlToWebPageFileNameDict[url];
                    _fileSystemHandler.File.WriteAllText(pageCache.FilePath, htmlString);
                    pageCache.CacheDate = DateTime.Now;
                    if (experationDate.HasValue)
                    {
                        pageCache.ExperationDate = experationDate;
                    }
                }
                catch
                {
                    "Swallow this exception.".ToString();
                }
            }
            else
            {
                string filename = _fileSystemHandler.File.GetNewFileNamePath(_getPageCacheDirectoryPath);
                _fileSystemHandler.File.WriteAllText(filename, htmlString);
                var pageCache = new PageCache() { FilePath = filename, CacheDate = DateTime.Now };
                if (experationDate.HasValue)
                {
                    pageCache.ExperationDate = experationDate;
                }
                _urlToWebPageFileNameDict.Add(url, pageCache);
            }

            saveXmlFile();
        }

        /// <exception cref="System.ArgumentNullException"></exception>
        public bool TryGetPage(string url, out string htmlString)
        {
            url = removeIgnoredQueryParams(url);

            if (!_urlToWebPageFileNameDict.ContainsKey(url))
            {
                htmlString = null;
                return false;
            }
            try
            {
                var pageCache = _urlToWebPageFileNameDict[url];
                if (pageCache.ExperationDate.HasValue && pageCache.ExperationDate.Value < DateTime.Now)
                {
                    if (_fileSystemHandler.File.Exists(pageCache.FilePath))
                    {
                        _fileSystemHandler.File.Delete(pageCache.FilePath);
                    }
                    _urlToWebPageFileNameDict.Remove(url);
                    throw new Exception("Page Expired");
                }

                if (!_fileSystemHandler.File.Exists(pageCache.FilePath))
                {
                    _urlToWebPageFileNameDict.Remove(url);
                    throw new Exception("File does not exist");
                }

                htmlString = _fileSystemHandler.File.ReadAllText(pageCache.FilePath);
            }
            catch (Exception)
            {
                htmlString = null;
                return false;
            }
            finally
            {
                saveXmlFile();
            }

            return true;
        }

        public void EmptyCache()
        {
            string[] keys = _urlToWebPageFileNameDict.Keys.ToArray();

            for (int i = keys.Length - 1; i >= 0; i--)
            {
                string url = keys[i];
                PageCache pageCache = _urlToWebPageFileNameDict[url];
                if (_fileSystemHandler.File.Exists(pageCache.FilePath))
                {
                    _fileSystemHandler.File.Delete(pageCache.FilePath);
                }

                _urlToWebPageFileNameDict.Remove(url);
            }

            saveXmlFile();
        }

        public void RemoveUrlFormCache(string url)
        {
            url = removeIgnoredQueryParams(url);

            if (!_urlToWebPageFileNameDict.ContainsKey(url))
            {
                return;
            }

            try
            {
                PageCache pc = _urlToWebPageFileNameDict[url];

                if (_fileSystemHandler.File.Exists(pc.FilePath))
                {
                    _fileSystemHandler.File.Delete(pc.FilePath);
                }
            }
            catch (Exception)
            {
                // If unable to delete then just move on.
            }

            // As long as it is removed from dictionary it will be gone.
            _urlToWebPageFileNameDict.Remove(url);
        }

        public void AddIgnoredQueryParameter(string name)
        {
            _ignoreQueryParams.Add(name);
        }

        #endregion
    }
}