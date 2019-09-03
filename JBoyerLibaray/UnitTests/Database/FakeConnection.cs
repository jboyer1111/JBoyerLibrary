using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{

    
    public class FakeConnection : IDbConnection
    {

        #region Private Variables

        private ConnectionState _state = ConnectionState.Closed;
        private string _database;
        private IDbTransaction _transaction = null;
        private FakeDatabase _fakeDatabase;
        private bool _isDisposed = false;

        #endregion

        #region Public Properties

        public string ConnectionString { get; set; }

        public ConnectionState State => _state;

        public string Database => _database;

        public int ConnectionTimeout => -1;

        public bool IsDisposed => _isDisposed;

        #endregion

        #region Constructor

        public FakeConnection(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
            _database = "FakeDatabase";
        }

        #endregion

        #region Public Methods

        public void Open()
        {
            _state = ConnectionState.Open;
        }

        public void Close()
        {
            _state = ConnectionState.Closed;
        }

        public IDbCommand CreateCommand()
        {
            return new FakeCommand(_fakeDatabase, this);
        }

        public void ChangeDatabase(string databaseName)
        {
            _database = databaseName;
        }

        public IDbTransaction BeginTransaction()
        {
            _transaction = new FakeTrasaction(this);

            return _transaction;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            _transaction = new FakeTrasaction(this, il);

            return _transaction;
        }

        public void Dispose()
        {
            _isDisposed = true;
            _state = ConnectionState.Closed;

            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }

        #endregion

    }

}
