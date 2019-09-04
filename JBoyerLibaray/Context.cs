using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{

    [ExcludeFromCodeCoverage]
    public class Context<T> : IDisposable
    {

        #region Public Properties

        public bool IsDisposed { get; private set; }

        public T Value { get; set; }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            IsDisposed = true;
        }

        #endregion

    }

}
