using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Holds info for NonQuery statements
    /// </summary>
    internal class NonQueryInfo
    {

        #region Private Variables

        private Action<FakeDatabase, IDataParameterCollection> _nonQueryCallback;
        private IEnumerable<string> _expectedParameters;

        #endregion

        #region Constructor

        public NonQueryInfo(IEnumerable<string> expectedParameters) : this((d, p) => { }, expectedParameters) { }

        public NonQueryInfo(Action<FakeDatabase, IDataParameterCollection> nonQueryCallback, IEnumerable<string> expectedParameters)
        {
            if (nonQueryCallback == null)
            {
                throw new ArgumentNullException(nameof(nonQueryCallback));
            }

            if (expectedParameters == null)
            {
                throw new ArgumentNullException(nameof(expectedParameters));
            }

            _nonQueryCallback = nonQueryCallback;
            _expectedParameters = expectedParameters;
        }

        #endregion

        #region Public Methods

        public void DoCallback(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            validationParameters(passedParams);

            _nonQueryCallback(fakeDatabase, passedParams);
        }

        #endregion

        #region Private Methods

        private void validationParameters(IDataParameterCollection passedParams)
        {
            var missingParam = _expectedParameters.FirstOrDefault(p => !passedParams.Contains(p));
            if (!String.IsNullOrWhiteSpace(missingParam))
            {
                throw new InvalidOperationException($"Non query is missing a required Parameter with the name \"{missingParam}\".");
            }

            var extraParam = passedParams
                .Cast<IDataParameter>()
                .FirstOrDefault(p => !_expectedParameters.Contains(p.ParameterName, StringComparer.CurrentCultureIgnoreCase));

            if (extraParam != null)
            {
                throw new InvalidOperationException($"Non query was passed an extra Parameter with the name \"{extraParam.ParameterName}\".");
            }
        }

        #endregion

    }
}
