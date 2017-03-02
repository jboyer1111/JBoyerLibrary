using System;
using System.Collections.Generic;
using System.Data;
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
        private Dictionary<string, IEnumerable<object>> _testData;

        #endregion

        #region Public Properties

        public string ConnectionString { get; set; }

        public ConnectionState State
        {
            get
            {
                return _state;
            }
        }

        public string Database
        {
            get
            {
                return _database;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return -1;
            }
        }

        #endregion

        #region Constructor

        public FakeConnection(Dictionary<string, IEnumerable<object>> databaseData)
        {
            _testData = databaseData;
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
            return new FakeCommand(this, _testData);
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
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }

        #endregion
    }
}
