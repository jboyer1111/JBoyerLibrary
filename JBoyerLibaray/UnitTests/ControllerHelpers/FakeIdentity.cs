using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{

    [ExcludeFromCodeCoverage]
    public class FakeIdentity : IIdentity
    {

        #region Private Variables

        private string _name;
        private bool _isAuthenticated;

        #endregion

        #region Public Properties

        public string AuthenticationType => "ApplicationCookie";

        public bool IsAuthenticated => _isAuthenticated;

        public string Name => _name;

        #endregion

        #region Constructor

        internal FakeIdentity()
        {
            _name = "";
            _isAuthenticated = false;
        }

        internal FakeIdentity(string name)
        {
            _name = name;
            _isAuthenticated = !String.IsNullOrWhiteSpace(name);
        }

        #endregion

    }

}
