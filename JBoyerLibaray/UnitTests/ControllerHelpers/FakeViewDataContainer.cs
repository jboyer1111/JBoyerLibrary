using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests.ControllerHelpers
{

    [ExcludeFromCodeCoverage]
    public class FakeViewDataContainer : IViewDataContainer
    {

        public ViewDataDictionary ViewData { get; set; }

    }

}
