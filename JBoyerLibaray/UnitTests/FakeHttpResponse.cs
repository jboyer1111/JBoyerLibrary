﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JBoyerLibaray.UnitTests
{
    public class FakeHttpResponse : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}
