using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Used for storing result info for a stored procedure
    /// </summary>
    internal abstract class StoredProcedureInfo
    {

        protected Func<FakeDatabase, IDataParameterCollection, MultiResultSet> _multiResultSet;
        protected Func<FakeDatabase, IDataParameterCollection, IEnumerable<object>> _results;
        protected IEnumerable<string> _expectedParameters;

        public bool HasMulti { get { return _multiResultSet != null; } }

        public IEnumerable<object> GetResults(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            if (HasMulti)
            {
                throw new Exception("Stored Procedure is setup as a Mutilple Result set. You need to call GetStoredProcedureMultiResults.");
            }

            validationParameters(passedParams);

            return _results(fakeDatabase, passedParams);
        }

        public IEnumerable<IEnumerable<object>> GetMultiResulsts(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            if (!HasMulti)
            {
                throw new Exception("Stored Procedure is setup as a Single Result set. You need to call GetStoredProcedureResults.");
            }

            validationParameters(passedParams);

            return _multiResultSet(fakeDatabase, passedParams).ResultSets;
        }

        public IDataReader GetDataReader(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            validationParameters(passedParams);

            if (HasMulti)
            {
                return _multiResultSet(fakeDatabase, passedParams).ToDataReader();
            }

            return _results(fakeDatabase, passedParams).ToDataReader();
        }

        public object GetScalar(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            validationParameters(passedParams);

            IDataReader resultReader = GetDataReader(fakeDatabase, passedParams);

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
            if (!string.IsNullOrWhiteSpace(missingParam))
            {
                throw new InvalidOperationException($"Stored Procedure is missing a required Parameter with the name \"{missingParam}\".");
            }

            var extraParam = passedParams
                .Cast<IDataParameter>()
                .FirstOrDefault(p => !_expectedParameters.Contains(p.ParameterName, StringComparer.CurrentCultureIgnoreCase));

            if (extraParam != null)
            {
                throw new InvalidOperationException($"Stored Procedure was passed an extra Parameter with the name \"{extraParam.ParameterName}\".");
            }
        }

    }

}
