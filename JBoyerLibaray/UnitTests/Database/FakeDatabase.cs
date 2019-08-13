using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{

    [ExcludeFromCodeCoverage]
    public class FakeDatabase
    {

        #region Private Variables

        private Dictionary<string, TableInfo> _tables = new Dictionary<string, TableInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, SqlInfo> _sqlScripts = new Dictionary<string, SqlInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, StoredProcedureInfo> _storedProcedures = new Dictionary<string, StoredProcedureInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, NonQueryInfo> _nonQuerySqlScripts = new Dictionary<string, NonQueryInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, Action> _insertQueryCallBacks = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, Action> _updateQueryCallBacks = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, Action> _deleteQueryCallBacks = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);

        #endregion

        #region Public Varibles

        public string[] Tables
        {
            get
            {
                return _tables.Keys.OrderBy(s => s).ToArray();
            }
        }

        public string[] SqlScripts
        {
            get
            {
                return _sqlScripts.Keys.OrderBy(s => s).ToArray();
            }
        }

        public string[] StoredProcedures
        {
            get
            {
                return _storedProcedures.Keys.OrderBy(s => s).ToArray();
            }
        }

        public string[] NonQuerySqlScripts
        {
            get
            {
                return _nonQuerySqlScripts.Keys.OrderBy(s => s).ToArray();
            }
        }

        public string[] InsertQueryCallbacks
        {
            get
            {
                return _insertQueryCallBacks.Keys.OrderBy(s => s).ToArray();
            }
        }

        public string[] UpdateQueryCallbacks
        {
            get
            {
                return _updateQueryCallBacks.Keys.OrderBy(s => s).ToArray();
            }
        }

        public string[] DeleteQueryCallbacks
        {
            get
            {
                return _deleteQueryCallBacks.Keys.OrderBy(s => s).ToArray();
            }
        }

        #endregion

        #region Public Methods

        public IEnumerable<object> GetTableResults(string tableName)
        {
            if (!_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("Table \"" + tableName + "\" is not setup in the database.");
            }

            return _tables[tableName].GetResults();
        }

        public IEnumerable<object> GetSqlScriptResults(string sql, IDataParameterCollection parameters)
        {
            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in the database.");
            }

            return _sqlScripts[sql].GetResults(this, parameters);
        }

        public IEnumerable<object> GetStoredProcedureResults(string storedProcedure, IDataParameterCollection parameters)
        {
            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in the database.");
            }
            
            return _storedProcedures[storedProcedure].GetResults(this, parameters);
        }

        public object GetSqlScriptScalar(string sql, IDataParameterCollection parameters)
        {
            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in the database.");
            }

            return _sqlScripts[sql].GetScalar(this, parameters);
        }

        public object GetStoredProcedureScalar(string storedProcedure, IDataParameterCollection parameters)
        {
            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in the database.");
            }

            return _storedProcedures[storedProcedure].GetScalar(this, parameters);
        }

        public void CallNonQuery(string sql, IDataParameterCollection parameters)
        {
            if (!_nonQuerySqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The sql \"" + sql + "\" is was not setup for non query in the database.");
            }

            _nonQuerySqlScripts[sql].DoCallback(this, parameters);
        }

        public void CallInsertCallback(string tableName)
        {
            if (_insertQueryCallBacks.ContainsKey(tableName))
            {
                _insertQueryCallBacks[tableName]();
            }
        }

        public void CallUpdateCallback(string tableName)
        {
            if (_updateQueryCallBacks.ContainsKey(tableName))
            {
                _updateQueryCallBacks[tableName]();
            }
        }

        public void CallDeleteCallback(string tableName)
        {
            if (_deleteQueryCallBacks.ContainsKey(tableName))
            {
                _deleteQueryCallBacks[tableName]();
            }
        }

        public void SetupTable<T>(string tableName, IEnumerable<T> readerResults) where T : class
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            if (readerResults == null)
            {
                readerResults = Enumerable.Empty<T>();
            }

            if (_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The table \"" + tableName + "\" has already been setup.");
            }

            _tables.Add(tableName, new TableInfo<T>(readerResults));
        }

        public void SetupTable<T>(string tableName, Func<IEnumerable<T>> tableResultResolver) where T : class
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace.", nameof(tableName));
            }

            if (tableResultResolver == null)
            {
                tableResultResolver = () => Enumerable.Empty<T>();
            }

            if (_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The table \"" + tableName + "\" has already been setup.");
            }

            _tables.Add(tableName, new TableInfo<T>(tableResultResolver));
        }

        public void SetupSql<T>(string sql, T scalarResult, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(sql));
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

        public void SetupSql<T>(string sql, IEnumerable<T> readerResults, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(sql));
            }

            if (readerResults == null)
            {
                readerResults = Enumerable.Empty<T>();
            }

            if (requiredParameters == null)
            {
                requiredParameters = Enumerable.Empty<string>();
            }

            if (_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("The Sql \"" + sql + "\" has already been setup.");
            }

            _sqlScripts.Add(sql, new SqlInfo<T>(readerResults, requiredParameters));
        }

        public void SetupSql<T>(string sql, Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> objectResultResolver, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(sql));
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

        public void SetupStoredProcedure<T>(string storedProcedureName, IEnumerable<T> results, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(storedProcedureName));
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

        public void SetupStoredProcedure<T>(string storedProcedureName, T result, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(storedProcedureName));
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

        public void SetupStoredProcedure<T>(string storedProcedureName, Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> objectResultResolver, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(storedProcedureName));
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

        public void SetupNonQuery(string sql, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(sql));
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

        public void SetupNonQuery(string sql, Action<FakeDatabase, IDataParameterCollection> nonQueryCallback, IEnumerable<string> requiredParameters = null)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(sql));
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

        public void SetupInsertCallback(string tableName, Action insertCallback)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(tableName));
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
        
        public void SetupUpdateCallback(string tableName, Action insertCallback)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(tableName));
            }

            if (insertCallback == null)
            {
                throw new ArgumentNullException(nameof(insertCallback));
            }

            if (_updateQueryCallBacks.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The update callback for table \"" + tableName + "\" has already been setup.");
            }

            _updateQueryCallBacks.Add(tableName, insertCallback);
        }

        public void SetupDeleteCallback(string tableName, Action insertCallback)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Cannot be null, empty, or whitespace", nameof(tableName));
            }

            if (insertCallback == null)
            {
                throw new ArgumentNullException(nameof(insertCallback));
            }

            if (_deleteQueryCallBacks.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The delete callback for table \"" + tableName + "\" has already been setup.");
            }

            _deleteQueryCallBacks.Add(tableName, insertCallback);
        }

        #endregion

    }

}
