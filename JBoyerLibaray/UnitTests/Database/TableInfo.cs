using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    [ExcludeFromCodeCoverage]
    public abstract class TableInfo
    {
        #region Private Variables

        protected Func<IEnumerable<object>> _tableResultResolver;
        protected IEnumerable<object> _results;

        #endregion

        #region Public Mehtods

        public IEnumerable<object> GetResults()
        {
            if (_tableResultResolver != null)
            {
                return _tableResultResolver();
            }

            return _results;
        }

        #endregion
    }
}
