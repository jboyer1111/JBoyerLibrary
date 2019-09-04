using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests
{

    [ExcludeFromCodeCoverage]
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
