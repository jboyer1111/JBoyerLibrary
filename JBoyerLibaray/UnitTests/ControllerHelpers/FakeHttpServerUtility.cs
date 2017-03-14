using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests
{
    public class FakeHttpServerUtility : HttpServerUtilityBase
    {
        private Controller _controller;
        
        public FakeHttpServerUtility(Controller controller)
        {
            _controller = controller;
        }
        
        public override string MapPath(string path)
        {
            var root = _controller.Url.Content("~");

            path = Regex.Replace(path, "^" + root, "").Replace("/", "\\");

            return Path.Combine(Environment.CurrentDirectory, path);
        }
    }
}
