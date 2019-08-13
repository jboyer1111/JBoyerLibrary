using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{

    [ExcludeFromCodeCoverage]
    public class TableInfo<T> : TableInfo where T : class
    {
        #region Constructor

        public TableInfo(IEnumerable<T> results)
        {
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            _results = results;
        }

        public TableInfo(Func<IEnumerable<T>> tableResultResolver)
        {
            if (tableResultResolver == null)
            {
                throw new ArgumentNullException(nameof(tableResultResolver));
            }
            
            _tableResultResolver = tableResultResolver;
        }

        #endregion
    }

}
