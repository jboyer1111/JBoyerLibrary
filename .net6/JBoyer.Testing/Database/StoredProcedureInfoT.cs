using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Used for storing result info for a stored procedure that returns a single result set
    /// </summary>
    internal class StoredProcedureInfo<T> : StoredProcedureInfo where T : class
    {

        public StoredProcedureInfo(T result, IEnumerable<string> expectedParameters)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (expectedParameters == null)
            {
                throw new ArgumentNullException(nameof(expectedParameters));
            }

            _results = (d, p) => new List<T>() { result };
            _expectedParameters = expectedParameters;
        }

        public StoredProcedureInfo(IEnumerable<T> results, IEnumerable<string> expectedParameters)
        {
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            if (expectedParameters == null)
            {
                throw new ArgumentNullException(nameof(expectedParameters));
            }

            _results = (d, p) => results;
            _expectedParameters = expectedParameters;
        }

        public StoredProcedureInfo(Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> storedProcedureResultResolver, IEnumerable<string> expectedParameters)
        {
            if (storedProcedureResultResolver == null)
            {
                throw new ArgumentNullException(nameof(storedProcedureResultResolver));
            }

            if (expectedParameters == null)
            {
                throw new ArgumentNullException(nameof(expectedParameters));
            }

            _results = storedProcedureResultResolver;
            _expectedParameters = expectedParameters;
        }

    }

}
