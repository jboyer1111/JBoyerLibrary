using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{
    [ExcludeFromCodeCoverage]
    public class NonQueryInfo
    {

        #region Private Variables

        private Action<FakeDatabase, IDataParameterCollection> _nonQueryCallback;
        private IEnumerable<string> _requiredParameters;

        #endregion

        #region Constructor

        public NonQueryInfo(IEnumerable<string> requiredParameters) : this((d, p) => { }, requiredParameters) { }

        public NonQueryInfo(Action<FakeDatabase, IDataParameterCollection> nonQueryCallback, IEnumerable<string> requiredParameters)
        {
            if (nonQueryCallback == null)
            {
                throw new ArgumentNullException(nameof(nonQueryCallback));
            }

            if (requiredParameters == null)
            {
                throw new ArgumentNullException(nameof(requiredParameters));
            }

            _nonQueryCallback = nonQueryCallback;
            _requiredParameters = requiredParameters;
        }

        #endregion

        #region Public Methods

        public void DoCallback(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            foreach (var param in _requiredParameters)
            {
                var paramater = passedParams[param];
                if (paramater == null)
                {
                    throw new InvalidOperationException("Sql is missing a required Parameter with the name \"" + param + "\"");
                }
            }

            _nonQueryCallback(fakeDatabase, passedParams);
        }

        #endregion

    }
}
