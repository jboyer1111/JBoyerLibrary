using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    public class TableInfo<T> : TableInfo where T : class
    {
        #region Constructor

        public TableInfo(IEnumerable<T> results)
        {
            if (results == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => results);
            }

            _results = results;
        }

        public TableInfo(Func<IEnumerable<T>> tableResultResolver)
        {
            if (tableResultResolver == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => tableResultResolver);
            }
            
            _tableResultResolver = tableResultResolver;
        }

        #endregion
    }
}
