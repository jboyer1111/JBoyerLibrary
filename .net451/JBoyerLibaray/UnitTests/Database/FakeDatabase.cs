using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Holds data to be resolved when FakeCommand processes Sql commands.
    /// </summary>
    public class FakeDatabase
    {

        #region Private Variables

        private Dictionary<string, TableInfo> _tables = new Dictionary<string, TableInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, SqlInfo> _sqlScripts = new Dictionary<string, SqlInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, StoredProcedureInfo> _storedProcedures = new Dictionary<string, StoredProcedureInfo>(StringComparer.CurrentCultureIgnoreCase);
        
        private Dictionary<string, NonQueryInfo> _nonQuerySqlScripts = new Dictionary<string, NonQueryInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, NonQueryInfo> _nonQueryStoredProcedures = new Dictionary<string, NonQueryInfo>(StringComparer.CurrentCultureIgnoreCase);

        private Dictionary<string, Action> _insertQueryCallBacks = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, Action> _updateQueryCallBacks = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, Action> _deleteQueryCallBacks = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns an array of the currently setup tables
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] Tables => _tables.Keys.OrderBy(s => s).ToArray();

        /// <summary>
        /// Returns an array of the currently setup sql scripts
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] SqlScripts => _sqlScripts.Keys.ToArray();

        /// <summary>
        /// Returns an array of the currently setup StoredProcedures
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] StoredProcedures => _storedProcedures.Keys.ToArray();

        /// <summary>
        /// Returns an array of the currently setup nonquery sql scripts
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] NonQuerySqlScripts => _nonQuerySqlScripts.Keys.ToArray();

        /// <summary>
        /// Returns an array of the currently setup nonquery stored procedures
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] NonQueryStoredProcedures => _nonQuerySqlScripts.Keys.ToArray();

        /// <summary>
        /// Returns an array of the tables that have insert callbacks setup
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] InsertQueryCallbacks => _insertQueryCallBacks.Keys.ToArray();

        /// <summary>
        /// Returns an array of the tables that have update callbacks setup
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] UpdateQueryCallbacks => _updateQueryCallBacks.Keys.ToArray();

        /// <summary>
        /// Returns an array of the tables that have delete callbacks setup
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string[] DeleteQueryCallbacks => _deleteQueryCallBacks.Keys.ToArray();

        #endregion

        #region Public Methods

        #region Dapper Callback Logic

        /// <summary>
        /// Used by FakeCommand to say that a Dapper.Contrib insert statement was executed
        /// </summary>
        internal void CallInsertCallback(string tableName)
        {
            if (_insertQueryCallBacks.ContainsKey(tableName))
            {
                _insertQueryCallBacks[tableName]();
            }
        }

        /// <summary>
        /// Used by FakeCommand to say that a Dapper.Contrib update statement was executed
        /// </summary>
        internal void CallUpdateCallback(string tableName)
        {
            if (_updateQueryCallBacks.ContainsKey(tableName))
            {
                _updateQueryCallBacks[tableName]();
            }
        }

        /// <summary>
        /// Used by FakeCommand to say that a Dapper.Contrib delete statement was executed
        /// </summary>
        internal void CallDeleteCallback(string tableName)
        {
            if (_deleteQueryCallBacks.ContainsKey(tableName))
            {
                _deleteQueryCallBacks[tableName]();
            }
        }

        /// <summary>
        /// Sets up a callback function to occur for when Dapper.Contrib insert statements are executed.
        /// </summary>
        /// <param name="tableName">The name of the table for the callback function to be applied to</param>
        /// <param name="insertCallback">The function to be executed when the insert statement is executed.</param>
        public void SetupInsertCallback(string tableName, Action insertCallback)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            if (insertCallback == null)
            {
                throw new ArgumentNullException(nameof(insertCallback));
            }

            if (_insertQueryCallBacks.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The insert callback for table \"" + tableName + "\" has already been setup.");
            }

            _insertQueryCallBacks.Add(tableName, insertCallback);
        }

        /// <summary>
        /// Sets up a callback function to occur for when Dapper.Contrib update statements are executed.
        /// </summary>
        /// <param name="tableName">The name of the table for the callback function to be applied to</param>
        /// <param name="updateCallback">The function to be executed when the update statement is executed.</param>
        public void SetupUpdateCallback(string tableName, Action updateCallback)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            if (updateCallback == null)
            {
                throw new ArgumentNullException(nameof(updateCallback));
            }

            if (_updateQueryCallBacks.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The update callback for table \"" + tableName + "\" has already been setup.");
            }

            _updateQueryCallBacks.Add(tableName, updateCallback);
        }

        /// <summary>
        /// Sets up a callback function to occur for when Dapper.Contrib delete statements are executed.
        /// </summary>
        /// <param name="tableName">The name of the table for the callback function to be applied to</param>
        /// <param name="deleteCallback">The function to be executed when the delete statement is executed.</param>
        public void SetupDeleteCallback(string tableName, Action deleteCallback)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            if (deleteCallback == null)
            {
                throw new ArgumentNullException(nameof(deleteCallback));
            }

            if (_deleteQueryCallBacks.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The delete callback for table \"" + tableName + "\" has already been setup.");
            }

            _deleteQueryCallBacks.Add(tableName, deleteCallback);
        }

        #endregion

        #region NonQuery Logic

        /// <summary>
        /// Used by FakeCommand to say a non query sql statement was executed.
        /// </summary>
        internal void CallNonQuerySql(string sql, IDataParameterCollection parameters)
        {
            if (!_nonQuerySqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The sql \"" + sql + "\" was not setup for non query in the database.");
            }

            _nonQuerySqlScripts[sql].DoCallback(this, parameters);
        }

        /// <summary>
        /// Sets up sql for when nonquery sql statements
        /// </summary>
        /// <param name="sql">Sql to setup.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupNonQuerySql(string sql, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_nonQuerySqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _nonQuerySqlScripts.Add(sql, new NonQueryInfo(requiredParameters));
        }

        /// <summary>
        /// Sets up sql for when nonquery sql statements
        /// </summary>
        /// <param name="sql">Sql to setup.</param>
        /// <param name="nonQueryCallback">Callback to execute when the FakeCommand executes sql statement.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupNonQuerySql(string sql, Action<FakeDatabase, IDataParameterCollection> nonQueryCallback, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (nonQueryCallback == null)
            {
                throw new ArgumentNullException(nameof(nonQueryCallback));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_nonQuerySqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _nonQuerySqlScripts.Add(sql, new NonQueryInfo(nonQueryCallback, requiredParameters));
        }

        /// <summary>
        /// Used by FakeCommand to say a non query stored procedure was executed.
        /// </summary>
        internal void CallNonQueryStoredProcedure(string storedProcedure, IDataParameterCollection parameters)
        {
            if (!_nonQueryStoredProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("The stored procedure \"" + storedProcedure + "\" was not setup for non query in the database.");
            }

            _nonQueryStoredProcedures[storedProcedure].DoCallback(this, parameters);
        }

        /// <summary>
        /// Sets up sql for when nonquery sql statements
        /// </summary>
        /// <param name="storedProcedure">Stored procedure to setup.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupNonQueryStoredProcedure(string storedProcedure, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(storedProcedure))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedure));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_nonQueryStoredProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("The Stored Procedure \"" + storedProcedure + "\" has already been setup.");
            }

            _nonQueryStoredProcedures.Add(storedProcedure, new NonQueryInfo(requiredParameters));
        }

        /// <summary>
        /// Sets up sql for when nonquery sql statements
        /// </summary>
        /// <param name="storedProcedure">Stored procedure to setup.</param>
        /// <param name="nonQueryCallback">Callback to execute when the FakeCommand executes stored procedure statement.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupNonQueryStoredProcedure(string storedProcedure, Action<FakeDatabase, IDataParameterCollection> nonQueryCallback, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(storedProcedure))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedure));
            }

            if (nonQueryCallback == null)
            {
                throw new ArgumentNullException(nameof(nonQueryCallback));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_nonQueryStoredProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("The Stored Procedure \"" + storedProcedure + "\" has already been setup.");
            }

            _nonQueryStoredProcedures.Add(storedProcedure, new NonQueryInfo(nonQueryCallback, requiredParameters));
        }

        #endregion

        #region Dapper Table Logic

        /// <summary>
        /// Used by FakeCommand to get list of objects for the table sql. 
        /// Tables are dependant on the Dapper Contrib logic or sql that matches Dapper Contrib sql.
        /// </summary>
        /// <param name="tableName">Table to get data for</param>
        /// <returns>Rows setup for the passed table</returns>
        public IEnumerable<object> GetTableResults(string tableName)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            tableName = tableName.Trim();

            if (!_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("Table \"" + tableName + "\" is not setup in the database.");
            }

            return _tables[tableName].GetResults();
        }

        /// <summary>
        /// Sets up results for a table. These tables are dependant on the Dapper Contrib logic or sql that matches Dapper Contrib sql.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the table.</typeparam>
        /// <param name="tableName">The name of the table to assign the results to.</param>
        /// <param name="readerResults">The list of results to be assign to the table.</param>
        public void SetupTable<T>(string tableName, IEnumerable<T> readerResults) where T : class
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            tableName = tableName.Trim();

            if (_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The table \"" + tableName + "\" has already been setup.");
            }

            if (readerResults == null)
            {
                readerResults = Enumerable.Empty<T>();
            }

            _tables.Add(tableName, new TableInfo<T>(readerResults));
        }

        /// <summary>
        /// Sets up results for a table. These tables are dependant on the Dapper Contrib logic or sql that matches Dapper Contrib sql.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the table.</typeparam>
        /// <param name="tableName">The name of the table to assign the resolver to.</param>
        /// <param name="tableResultResolver">Called when retrieving the results for the table.</param>
        public void SetupTable<T>(string tableName, Func<IEnumerable<T>> tableResultResolver) where T : class
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            tableName = tableName.Trim();

            if (_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The table \"" + tableName + "\" has already been setup.");
            }

            if (tableResultResolver == null)
            {
                tableResultResolver = () => Enumerable.Empty<T>();
            }

            _tables.Add(tableName, new TableInfo<T>(tableResultResolver));
        }

        #endregion

        #region Sql Statement Logic

        /// <summary>
        /// Method of getting the scallar result for a sql statement.
        /// </summary>
        public object GetSqlScriptScalar(string sql, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in the database.");
            }

            return _sqlScripts[sql].GetScalar(this, parameters);
        }

        /// <summary>
        /// Method for getting list of objects for a sql statement. 
        /// </summary>
        public IEnumerable<object> GetSqlScriptResults(string sql, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in the database.");
            }

            return _sqlScripts[sql].GetResults(this, parameters);
        }

        /// <summary>
        /// Method for getting the list of result sets for a multi result set sql statment.
        /// </summary>
        public IEnumerable<IEnumerable<object>> GetSqlScriptMultiResults(string sql, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in the database.");
            }

            return _sqlScripts[sql].GetMultiResulsts(this, parameters);
        }

        /// <summary>
        /// Sets up a result object for a sql statement that returns a scalar result.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the sql statement.</typeparam>
        /// <param name="sql">Sql statement to setup.</param>
        /// <param name="scalarResult">The result to be assigned to the sql statement.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupSql<T>(string sql, T scalarResult, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _sqlScripts.Add(sql, new SqlInfo<T>(scalarResult, requiredParameters));
        }

        /// <summary>
        /// Sets up results for a sql statement that returns a single result set.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the sql statement.</typeparam>
        /// <param name="sql">Sql statement to setup.</param>
        /// <param name="results">The list of results to be assigned to the sql statement.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupSql<T>(string sql, IEnumerable<T> results, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (results == null)
            {
                results = Enumerable.Empty<T>();
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _sqlScripts.Add(sql, new SqlInfo<T>(results, requiredParameters));
        }

        /// <summary>
        /// Sets up results for a sql statement that returns a single result set.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the sql statement.</typeparam>
        /// <param name="sql">Sql statement to setup.</param>
        /// <param name="objectResultResolver">Called when retrieving the results for the sql statement.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupSql<T>(string sql, Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> objectResultResolver, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (objectResultResolver == null)
            {
                throw new ArgumentNullException(nameof(objectResultResolver));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _sqlScripts.Add(sql, new SqlInfo<T>(objectResultResolver, requiredParameters));
        }

        /// <summary>
        /// Sets up results for a sql statement that returns multiple result sets.
        /// </summary>
        /// <param name="sql">Sql statement to setup.</param>
        /// <param name="multiResultSet">Called when retrieving the results for the sql statement.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupSql(string sql, Func<FakeDatabase, IDataParameterCollection, MultiResultSet> multiResultSet, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (multiResultSet == null)
            {
                throw new ArgumentNullException(nameof(multiResultSet));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _sqlScripts.Add(sql, new SqlInfoMulti(multiResultSet, requiredParameters));
        }

        #endregion

        #region Stored Procedures

        /// <summary>
        /// Method of getting the scallar result for a stored procedure.
        /// </summary>
        public object GetStoredProcedureScalar(string storedProcedure, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(storedProcedure))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedure));
            }

            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in the database.");
            }

            return _storedProcedures[storedProcedure].GetScalar(this, parameters);
        }

        /// <summary>
        /// Method for getting list of objects for a stored procedure.
        /// </summary>
        public IEnumerable<object> GetStoredProcedureResults(string storedProcedure, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(storedProcedure))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedure));
            }

            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in the database.");
            }

            return _storedProcedures[storedProcedure].GetResults(this, parameters);
        }

        /// <summary>
        /// Method for getting the list of result sets for a multi result set stored procedure.
        /// </summary>
        public IEnumerable<IEnumerable<object>> GetStoredProcedureMultiResults(string storedProcedure, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(storedProcedure))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedure));
            }

            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in the database.");
            }

            return _storedProcedures[storedProcedure].GetMultiResulsts(this, parameters);
        }

        /// <summary>
        /// Sets up a result object for a stored procedure that returns a scalar result.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the stored procedure.</typeparam>
        /// <param name="storedProcedureName">Stored procedure to setup.</param>
        /// <param name="result">The result to be assigned to the stored procedure.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupStoredProcedure<T>(string storedProcedureName, T result, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedureName));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_storedProcedures.ContainsKey(storedProcedureName))
            {
                throw new InvalidOperationException("The Stored Procedure \"" + storedProcedureName + "\" has already been setup.");
            }

            _storedProcedures.Add(storedProcedureName, new StoredProcedureInfo<T>(result, requiredParameters));
        }

        /// <summary>
        /// Sets up results for a stored procedure that returns a single result set.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the stored procedure.</typeparam>
        /// <param name="storedProcedureName">Stored procedure to setup.</param>
        /// <param name="results">The list of results to be assigned to the stored procedure.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupStoredProcedure<T>(string storedProcedureName, IEnumerable<T> results, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedureName));
            }

            if (results == null)
            {
                results = Enumerable.Empty<T>();
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_storedProcedures.ContainsKey(storedProcedureName))
            {
                throw new InvalidOperationException("The Stored Procedure \"" + storedProcedureName + "\" has already been setup.");
            }

            _storedProcedures.Add(storedProcedureName, new StoredProcedureInfo<T>(results, requiredParameters));
        }

        /// <summary>
        /// Sets up results for a stored procedure that returns a single result set.
        /// </summary>
        /// <typeparam name="T">The type of object to return for the stored procedure.</typeparam>
        /// <param name="storedProcedureName">Stored procedure to setup.</param>
        /// <param name="objectResultResolver">Called when retrieving the results for the stored procedure.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupStoredProcedure<T>(string storedProcedureName, Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> objectResultResolver, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedureName));
            }

            if (objectResultResolver == null)
            {
                throw new ArgumentNullException(nameof(objectResultResolver));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_storedProcedures.ContainsKey(storedProcedureName))
            {
                throw new InvalidOperationException("The Stored Procedure \"" + storedProcedureName + "\" has already been setup.");
            }

            _storedProcedures.Add(storedProcedureName, new StoredProcedureInfo<T>(objectResultResolver, requiredParameters));
        }

        /// <summary>
        /// Sets up results for a stored procedure that returns multiple result sets.
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure to setup.</param>
        /// <param name="multiResultSet">Called when retrieving the results for the stored procedure.</param>
        /// <param name="requiredParameters">List of required paramters. If passed parameters do not match an exception will be thrown.</param>
        public void SetupStoredProcedure(string storedProcedureName, Func<FakeDatabase, IDataParameterCollection, MultiResultSet> multiResultSet, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedureName));
            }

            if (multiResultSet == null)
            {
                throw new ArgumentNullException(nameof(multiResultSet));
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_storedProcedures.ContainsKey(storedProcedureName))
            {
                throw new InvalidOperationException("The Stored Procedure \"" + storedProcedureName + "\" has already been setup.");
            }

            _storedProcedures.Add(storedProcedureName, new StoredProcedureInfoMulti(multiResultSet, requiredParameters));
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Used by FakeCommand to get data reader for an sql statement.
        /// </summary>
        internal IDataReader getSqlScriptResultsReader(string sql, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(sql));
            }

            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in the database.");
            }

            return _sqlScripts[sql].GetDataReader(this, parameters);
        }

        /// <summary>
        /// Used by FakeCommand to get data reader for a stored procedure.
        /// </summary>
        internal IDataReader getStoredProcedureResultsReader(string storedProcedure, IDataParameterCollection parameters)
        {
            if (String.IsNullOrWhiteSpace(storedProcedure))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(storedProcedure));
            }

            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in the database.");
            }

            return _storedProcedures[storedProcedure].GetDataReader(this, parameters);
        }

        #endregion

    }

}
