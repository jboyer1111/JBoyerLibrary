using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
