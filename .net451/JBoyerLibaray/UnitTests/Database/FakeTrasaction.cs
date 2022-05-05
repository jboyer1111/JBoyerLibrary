using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Class for testing Transaction code.
    /// 07/20/2020: Currently unsure if code is competly correct
    /// </summary>
    public class FakeTrasaction : IDbTransaction
    {

        #region Private Variables

        private bool _disposed = false;
        private bool _commited = false;
        private bool _rolledback = false;

        private IDbConnection _connection = null;
        private IsolationLevel _isolationLevel = IsolationLevel.Unspecified;

        #endregion

        #region Public Properties

        [ExcludeFromCodeCoverage]
        public IDbConnection Connection => _connection;

        [ExcludeFromCodeCoverage]
        public IsolationLevel IsolationLevel => _isolationLevel;
        
        #endregion

        #region Constructor

        public FakeTrasaction(IDbConnection connection, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _connection = connection;
            _isolationLevel = isolationLevel;
        }

        #endregion

        #region Public Methods

        public void Commit()
        {
            if (_disposed || _commited || _rolledback || _connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Transaction in an invalid state.");
            }

            _commited = true;
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public void Rollback()
        {
            if (_disposed || _commited || _rolledback || _connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Transaction in an invalid state.");
            }

            _rolledback = true;
        }

        #endregion

    }

}
