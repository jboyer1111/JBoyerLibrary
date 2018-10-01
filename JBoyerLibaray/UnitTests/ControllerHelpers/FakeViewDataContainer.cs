using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests.ControllerHelpers
{
    public class FakeViewDataContainer : IViewDataContainer
    {
        public ViewDataDictionary ViewData { get; set; }

    }
}
