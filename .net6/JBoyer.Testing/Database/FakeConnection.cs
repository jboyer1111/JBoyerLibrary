using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Object for faking the IDbConnection interface
    /// </summary>
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

        /// <summary>
        /// Returns connection string. Only here to implement IDbConnection interface
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Returns current state of FakeConnection
        /// </summary>
        [ExcludeFromCodeCoverage]
        public ConnectionState State => _state;

        /// <summary>
        /// Returns database name
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string Database => _database;

        /// <summary>
        /// Returns -1. Only here to implement IDbConnection interface.
        /// Unit test code will never time out. To test timeout just fake it using result functions.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public int ConnectionTimeout => -1;

        /// <summary>
        /// Returns whether or not FakeConnection has been disposed.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public bool IsDisposed => _isDisposed;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a class to be used by unit tests to resolve DB calls
        /// </summary>
        /// <param name="fakeDatabase">Object that hold all the data to be returned</param>
        public FakeConnection(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
            _database = "FakeDatabase";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets state to open
        /// </summary>
        public void Open()
        {
            _state = ConnectionState.Open;
        }

        /// <summary>
        /// Sets state to closed
        /// </summary>
        public void Close()
        {
            _state = ConnectionState.Closed;
        }

        /// <summary>
        /// Create FakeCommand object
        /// </summary>
        public IDbCommand CreateCommand()
        {
            return new FakeCommand(_fakeDatabase, this);
        }

        /// <summary>
        /// Changes database string value
        /// </summary>
        public void ChangeDatabase(string databaseName)
        {
            _database = databaseName;
        }

        /// <summary>
        /// Create FakeTransaction object for this FakeConnection
        /// </summary>
        public IDbTransaction BeginTransaction()
        {
            _transaction = new FakeTrasaction(this);

            return _transaction;
        }

        /// <summary>
        /// Create FakeTransaction object for this FakeConnection with a sepified Isoloation level
        /// </summary>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            _transaction = new FakeTrasaction(this, il);

            return _transaction;
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            _isDisposed = true;
            _state = ConnectionState.Closed;
            _transaction?.Dispose();
        }

        #endregion

    }

}
