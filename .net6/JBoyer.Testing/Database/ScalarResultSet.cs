using System.Collections.Generic;
using System.Linq;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// This is for setting up the database to properly return result sets for dapper calls like Query&lt;string&gt;
    /// </summary>
    public static class ScalarResultSet
    {

        /// <summary>
        /// Perpares lists like IEnumerable&lt;string&gt; to be processed by UnitTests.Common.DataReader.EnumerableDataReader class
        /// </summary>
        public static IEnumerable<ScalarValue<T>> CreateResults<T>(params T[] scalarResults)
        {
            if (scalarResults == null)
            {
                return Enumerable.Empty<ScalarValue<T>>();
            }

            return scalarResults.Select(s => new ScalarValue<T>(s)).ToArray();
        }

    }

}
