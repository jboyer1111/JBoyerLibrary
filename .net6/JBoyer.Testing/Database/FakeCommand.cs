using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace JBoyer.Testing.Database
{

    public class FakeCommand : IDbCommand
    {

        #region Private Variables

        private FakeDatabase _fakeDatabase;

        #endregion

        #region Public Properties

        [ExcludeFromCodeCoverage]
        public string CommandText { get; set; } = "";

        [ExcludeFromCodeCoverage]
        public int CommandTimeout { get; set; }

        [ExcludeFromCodeCoverage]
        public CommandType CommandType { get; set; }

        [ExcludeFromCodeCoverage]
        public IDbConnection? Connection { get; set; }

        [ExcludeFromCodeCoverage]
        public IDbTransaction? Transaction { get; set; }

        [ExcludeFromCodeCoverage]
        public UpdateRowSource UpdatedRowSource { get; set; }

        [ExcludeFromCodeCoverage]
        public IDataParameterCollection Parameters { get; } = new FakeParameterCollection();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UnitTests.Common.Database.FakeCommand class with the passed data.
        /// </summary>
        /// <param name="fakeDatabase">The data that this command will execute against.</param>
        public FakeCommand(FakeDatabase? fakeDatabase) : this(fakeDatabase, null) { }

        /// <summary>
        /// Initializes a new instance of the UnitTests.Common.Database.FakeCommand class with passed data and connection.
        /// </summary>
        /// <param name="fakeDatabase">The data that this command will execute against.</param>
        /// <param name="connection">The connection that this command is connected to.</param>
        public FakeCommand(FakeDatabase? fakeDatabase, IDbConnection? connection)
        {
            _fakeDatabase = fakeDatabase ?? throw new ArgumentNullException(nameof(fakeDatabase));
            Connection = connection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes a Transact-SQL statement against the FakeDatabase. Excutes an event.
        /// </summary>
        /// <returns>Returns value based on Dapper sql. Dapper-Update = 1, Dapper-Delete = 2, Anything Else = 0</returns>
        public int ExecuteNonQuery()
        {
            var updateRecordMatch = Regex.Match(CommandText, @"^update (\S+) set (\S[\S\s]+) where (\S+) = @id$", RegexOptions.IgnoreCase);
            if (updateRecordMatch.Success)
            {
                var tableName = updateRecordMatch.Groups[1].Value;

                _fakeDatabase.CallUpdateCallback(tableName);
                return 1;
            }

            var deleteRecordMatch = Regex.Match(CommandText, @"^delete from (\S+) where (\S+) = @id$", RegexOptions.IgnoreCase);
            if (deleteRecordMatch.Success)
            {
                var tableName = deleteRecordMatch.Groups[1].Value;

                _fakeDatabase.CallDeleteCallback(tableName);
                return 2;
            }

            if (CommandType == CommandType.StoredProcedure)
            {
                _fakeDatabase.CallNonQueryStoredProcedure(CommandText, Parameters);
            }
            else
            {
                _fakeDatabase.CallNonQuerySql(CommandText, Parameters);
            }

            return 0;
        }

        [ExcludeFromCodeCoverage] // Just calls DB object methods based on CommandType no need to test here
        public object ExecuteScalar()
        {
            if (CommandType == CommandType.StoredProcedure)
            {
                return _fakeDatabase.GetStoredProcedureScalar(CommandText, Parameters);
            }

            // Custom Sql logic
            return _fakeDatabase.GetSqlScriptScalar(CommandText, Parameters);
        }

        /// <summary>
        /// This method gets the data from the fake database. If attempt to grab info
        /// not setup it will throw the key not found exception
        /// </summary>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">This only occurs if you have yet to setup database sqls or objects</exception>
        public IDataReader ExecuteReader()
        {
            var getAllMatch = Regex.Match(CommandText, @"^select [*] from (\S+)$", RegexOptions.IgnoreCase);
            if (getAllMatch.Success)
            {
                var tableName = getAllMatch.Groups[1].Value;

                return _fakeDatabase.GetTableResults(tableName).ToDataReader();
            }

            var getSingleRecordMatch = Regex.Match(CommandText, @"^select [*] from (\S+) where (\S+) = @id$", RegexOptions.IgnoreCase);
            if (getSingleRecordMatch.Success)
            {
                var tableName = getSingleRecordMatch.Groups[1].Value;
                var parameterName = getSingleRecordMatch.Groups[2].Value;

                IDbDataParameter? idParameter = Parameters["id"] as IDbDataParameter ?? throw new InvalidOperationException("id parameter is not set.");
                object? id = idParameter.Value;

                string? propertyName = null;
                return _fakeDatabase.GetTableResults(tableName).ToDataReader(o =>
                {
                    if (propertyName == null)
                    {
                        var type = o.GetType();
                        propertyName = type
                            .GetProperties()
                            .Where(p => string.Equals(p.Name, parameterName, StringComparison.CurrentCultureIgnoreCase))
                            .Select(p => p.Name)
                            .FirstOrDefault() ?? throw new InvalidOperationException($"{parameterName} does exist on {type.FullName}.");
                    }

                    return Equals(o.GetType().GetProperty(propertyName)?.GetValue(o, null), id);
                });
            }

            var insertRecordMatch = Regex.Match(CommandText, @"^insert into (\S+) \([^\)]+\) values \([^\)]+\);select scope_identity\(\) (\S+)$", RegexOptions.IgnoreCase);
            if (insertRecordMatch.Success)
            {
                var tableName = insertRecordMatch.Groups[1].Value;

                _fakeDatabase.CallInsertCallback(tableName);

                // Dapper insert logic return id of zero
                var a = new[]
                {
                    new { SCOPE_IDENTITY = 0 }
                };

                return a.ToDataReader();
            }

            if (CommandType == CommandType.StoredProcedure)
            {
                return _fakeDatabase.getStoredProcedureResultsReader(CommandText, Parameters);
            }

            // Custom Sql logic
            return _fakeDatabase.getSqlScriptResultsReader(CommandText, Parameters);
        }

        [ExcludeFromCodeCoverage] // Just calls other override don't need to test this
        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return ExecuteReader();
        }

        [ExcludeFromCodeCoverage] // Cancel does nothing so no need for code coverage.
        public void Cancel()
        {
            // Do nothing
        }

        [ExcludeFromCodeCoverage] // CreateParameter only creates FakeParameter, so no need for code coverage.
        public IDbDataParameter CreateParameter()
        {
            return new FakeParameter();
        }

        [ExcludeFromCodeCoverage] // Prepare only creates FakeParameter, so no need for code coverage.
        public void Prepare()
        {
            // Do nothing
            // Usually this is to allow database to optimize for the query before actually doing it
        }

        [ExcludeFromCodeCoverage] // Dispose does nothing so no need for code coverage.
        public void Dispose()
        {
            // Do nothing
        }

        #endregion

    }

}
