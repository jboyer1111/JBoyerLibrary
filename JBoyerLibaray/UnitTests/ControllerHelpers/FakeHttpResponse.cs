using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class FakeHttpResponse : HttpResponseBase
    {
        private int _statusCode = (int)HttpStatusCode.OK;
        private bool _trySkipIISCutomErrors = false;

        public override int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        public override bool TrySkipIisCustomErrors
        {
            get
            {
                return _trySkipIISCutomErrors;
            }

            set
            {
                _trySkipIISCutomErrors = value;
            }
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }

        public override void Clear()
        {
            // Do nothing -- YEA!!!!
        }
    }
}
