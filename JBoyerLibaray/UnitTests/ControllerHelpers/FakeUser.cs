using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace JBoyerLibaray.UnitTests
{

    [ExcludeFromCodeCoverage]
    public class FakeUser : IPrincipal
    {

        #region Private Variables

        private List<string> _roles = new List<string>();
        private IIdentity _identity;

        #endregion

        #region Public Properties

        public IIdentity Identity => _identity;

        #endregion

        #region Constructor

        private FakeUser()
        {
            _identity = new FakeIdentity();
        }

        public FakeUser(string name)
        {
            _identity = new FakeIdentity(name);
        }

        #endregion

        #region Public Methods

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

        public static FakeUser Anonymous() => new FakeUser();

        #endregion

    }

}
