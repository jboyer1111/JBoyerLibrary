using System;
using System.Collections.Generic;
using System.Data;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Used for storing result info for a stored procedure that returns multiple result sets
    /// </summary>
    internal class StoredProcedureInfoMulti : StoredProcedureInfo
    {

        public StoredProcedureInfoMulti(Func<FakeDatabase, IDataParameterCollection, MultiResultSet> multiResultSetResolver, IEnumerable<string> expectedParameters)
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
