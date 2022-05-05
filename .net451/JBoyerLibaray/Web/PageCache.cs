using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Web
{

    [ExcludeFromCodeCoverage]
    public class PageCache
    {

        public string FilePath { get; set; }

        public DateTime CacheDate { get; set; }

        public DateTime? ExperationDate { get; set; }

    }

}