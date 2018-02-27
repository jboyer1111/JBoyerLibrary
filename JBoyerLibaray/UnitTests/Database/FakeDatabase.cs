using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    public class FakeDatabase
    {
        #region Private Variables

        private Dictionary<string, IEnumerable<object>> _tables = new Dictionary<string, IEnumerable<object>>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, SqlInfo> _sqlScripts = new Dictionary<string, SqlInfo>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, StoredProcedureInfo> _storedProcedures = new Dictionary<string, StoredProcedureInfo>(StringComparer.CurrentCultureIgnoreCase);

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

        #endregion

        #region Public Methods

        public IEnumerable<object> GetTableResults(string tableName)
        {
            if (!_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("Table \"" + tableName + "\" is not setup in database.");
            }

            return _tables[tableName];
        }

        public IEnumerable<object> GetSqlScriptResults(string sql, IDataParameterCollection parameters)
        {
            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in database.");
            }

            return _sqlScripts[sql].GetResults(this, parameters);
        }

        public IEnumerable<object> GetStoredProcedureResults(string storedProcedure, IDataParameterCollection parameters)
        {
            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in database.");
            }
            
            return _storedProcedures[storedProcedure].GetResults(this, parameters);
        }

        public object GetSqlScriptScalar(string sql, IDataParameterCollection parameters)
        {
            if (!_sqlScripts.ContainsKey(sql))
            {
                throw new InvalidOperationException("Sql \"" + sql + "\" is not setup in database.");
            }

            return _sqlScripts[sql].GetScalar(this, parameters);
        }

        public object GetStoredProcedureScalar(string storedProcedure, IDataParameterCollection parameters)
        {
            if (!_storedProcedures.ContainsKey(storedProcedure))
            {
                throw new InvalidOperationException("Stored Procedure \"" + storedProcedure + "\" is not setup in database.");
            }

            return _storedProcedures[storedProcedure].GetScalar(this, parameters);
        }

        public void SetupTable<T>(string tableName, IEnumerable<T> readerResults) where T : class
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => tableName, "Cannot be null, empty, or whitespace.", tableName);
            }

            if (readerResults == null)
            {
                readerResults = Enumerable.Empty<T>();
            }

            if (_tables.ContainsKey(tableName))
            {
                throw new InvalidOperationException("The table \"" + tableName + "\" has already been setup.");
            }

            _tables.Add(tableName, readerResults);
        }

        public void SetupSql<T>(string sql, T scalarResult, IEnumerable<string> requiredParameters = null) where T : class
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => sql, "Cannot be null, empty, or whitespace", sql);
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
                throw ExceptionHelper.CreateArgumentInvalidException(() => sql, "Cannot be null, empty, or whitespace", sql);
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
                throw ExceptionHelper.CreateArgumentInvalidException(() => sql, "Cannot be null, empty, or whitespace", sql);
            }

            if (objectResultResolver == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => objectResultResolver);
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
                throw ExceptionHelper.CreateArgumentInvalidException(() => storedProcedureName, "Cannot be null, empty, or whitespace", storedProcedureName);
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
                throw ExceptionHelper.CreateArgumentInvalidException(() => storedProcedureName, "Cannot be null, empty, or whitespace", storedProcedureName);
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
                throw ExceptionHelper.CreateArgumentInvalidException(() => storedProcedureName, "Cannot be null, empty, or whitespace", storedProcedureName);
            }

            if (objectResultResolver == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => objectResultResolver);
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

        #endregion

    }
}
