using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    internal abstract class SqlInfo
    {

        protected Func<FakeDatabase, IDataParameterCollection, IEnumerable<object>> _results;
        protected IEnumerable<string> _expectedParameters;

        public IEnumerable<object> GetResults(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            validationParameters(passedParams);

            return _results(fakeDatabase, passedParams);
        }

        public object GetScalar(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            validationParameters(passedParams);

            IDataReader resultReader = _results(fakeDatabase, passedParams).ToDataReader();

            object result;
            if (resultReader.Read())
            {
                result = resultReader.GetValue(0);
            }
            else
            {
                result = -1;
            }

            return result;
        }

        private void validationParameters(IDataParameterCollection passedParams)
        {
            var missingParam = _expectedParameters.FirstOrDefault(p => !passedParams.Contains(p));
            if (!String.IsNullOrWhiteSpace(missingParam))
            {
                throw new InvalidOperationException($"Sql is missing a required Parameter with the name \"{missingParam}\".");
            }

            var extraParam = passedParams
                .Cast<IDataParameter>()
                .FirstOrDefault(p => !_expectedParameters.Contains(p.ParameterName, StringComparer.CurrentCultureIgnoreCase));

            if (extraParam != null)
            {
                throw new InvalidOperationException($"Sql was passed an extra Parameter with the name \"{extraParam.ParameterName}\".");
            }
        }

    }

}
