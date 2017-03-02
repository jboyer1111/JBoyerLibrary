using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{
    public class FakeUser : IPrincipal
    {
        private List<string> _roles = new List<string>();
        private IIdentity _identity;

        private FakeUser()
        {
            _identity = new FakeIdentity();
        }

        public FakeUser(string name)
        {
            _identity = new FakeIdentity(name);
        }

        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }

        public void AddRole(string role)
        {
            _roles.Add(role);
        }

        public void AddRoles(string[] roles)
        {
            _roles.AddRange(roles);
        }

        public static FakeUser Anonymous()
        {
            return new FakeUser();
        }
    }
}
