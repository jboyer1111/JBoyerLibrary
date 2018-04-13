using System;

namespace JBoyerLibaray.Web
{
    public class PageCache
    {
        public string FilePath { get; set; }

        public DateTime CacheDate { get; set; }

        public DateTime? ExperationDate { get; set; }
    }
}