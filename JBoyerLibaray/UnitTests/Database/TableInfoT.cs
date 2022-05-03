using System;
using System.Collections.Generic;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Class for storing Dapper table data.
    /// </summary>
    internal class TableInfo<T> : TableInfo where T : class
    {

        public TableInfo(IEnumerable<T> results)
        {
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            _results = () => results;
        }

        public TableInfo(Func<IEnumerable<T>> tableResultResolver)
        {
            if (tableResultResolver == null)
            {
                throw new ArgumentNullException(nameof(tableResultResolver));
            }

            _results = tableResultResolver;
        }

    }

}
