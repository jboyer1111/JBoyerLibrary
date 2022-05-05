using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web.Mvc;

namespace JBoyerLibaray.UnitTests.ControllerHelpers
{

    [ExcludeFromCodeCoverage]
    public class FakeView : IView
    {

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            // Do nothing
        }

    }

}
