using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{
    public class FakeIdentity : IIdentity
    {
        #region Private Region

        private string _name;
        private bool _isAuthenticated;

        #endregion

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

        public string AuthenticationType
        {
            get
            {
                return "ApplicationCookie";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
