using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace JBoyerLibaray
{
    public static class WebUtilHelper
    {

        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            // Not run by a website

            return path;
        }

        public static string ResolveUrl(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return VirtualPathUtility.ToAbsolute(path);
            }

            // Not run by a website

            return path;
        }

    }
}
