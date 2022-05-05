using System;
using System.Collections.Generic;
using System.Data;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Used for storing result info for a sql statement that returns a single result set
    /// </summary>
    internal class SqlInfo<T> : SqlInfo where T : class
    {

        public SqlInfo(T result, IEnumerable<string> expectedParameters)
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

        public SqlInfo(IEnumerable<T> results, IEnumerable<string> expectedParameters)
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

        public SqlInfo(Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> sqlResultResolver, IEnumerable<string> expectedParameters)
        {
            if (sqlResultResolver == null)
            {
                throw new ArgumentNullException(nameof(sqlResultResolver));
            }

            if (expectedParameters == null)
            {
                throw new ArgumentNullException(nameof(expectedParameters));
            }

            _results = sqlResultResolver;
            _expectedParameters = expectedParameters;
        }

    }

}
