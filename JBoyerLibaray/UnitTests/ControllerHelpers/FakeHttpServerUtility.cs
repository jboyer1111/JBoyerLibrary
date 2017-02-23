using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JBoyerLibaray.UnitTests
{
    public class FakeHttpServerUtility : HttpServerUtilityBase
    {
        public override string MapPath(string path)
        {
            return base.MapPath(path);
        }
    }
}
