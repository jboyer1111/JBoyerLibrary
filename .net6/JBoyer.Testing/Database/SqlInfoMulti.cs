using System;
using System.Collections.Generic;
using System.Data;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Used for storing result info for a sql statement that returns multiple result sets
    /// </summary>
    internal class SqlInfoMulti : SqlInfo
    {

        public SqlInfoMulti(Func<FakeDatabase, IDataParameterCollection, MultiResultSet> multiResultSetResolver, IEnumerable<string> expectedParameters)
        {
            if (multiResultSetResolver == null)
            {
                throw new ArgumentNullException(nameof(multiResultSetResolver));
            }

            if (expectedParameters == null)
            {
                throw new ArgumentNullException(nameof(expectedParameters));
            }

            _multiResultSet = multiResultSetResolver;
            _expectedParameters = expectedParameters;
        }

    }

}
