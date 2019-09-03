using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace JBoyerLibaray.UnitTests.Database
{


    public class FakeCommand : IDbCommand
    {

        #region Private Variables

        private IDataParameterCollection _parameters;
        private FakeDatabase _fakeDatabase;

        #endregion

        #region Public Properties

        [ExcludeFromCodeCoverage]
        public string CommandText { get; set; }

        [ExcludeFromCodeCoverage]
        public int CommandTimeout { get; set; }

        [ExcludeFromCodeCoverage]
        public CommandType CommandType { get; set; }

        [ExcludeFromCodeCoverage]
        public IDbConnection Connection { get; set; }

        [ExcludeFromCodeCoverage]
        public IDbTransaction Transaction { get; set; }

        [ExcludeFromCodeCoverage]
        public UpdateRowSource UpdatedRowSource { get; set; }

        [ExcludeFromCodeCoverage]
        public IDataParameterCollection Parameters => _parameters;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the UnitTests.Common.Database.FakeCommand class with the passed data.
        /// </summary>
        /// <param name="fakeDatabase">The data that this command will execute against.</param>
        public FakeCommand(FakeDatabase fakeDatabase) : this (fakeDatabase, null) { }

        /// <summary>
        /// Initializes a new instance of the UnitTests.Common.Database.FakeCommand class with passed data and connection.
        /// </summary>
        /// <param name="fakeDatabase">The data that this command will execute against.</param>
        /// <param name="connection">The connection that this command is connected to.</param>
        public FakeCommand(FakeDatabase fakeDatabase, IDbConnection connection)
        {
            if (fakeDatabase == null)
            {
                throw new ArgumentNullException(nameof(fakeDatabase));
            }
            
            _fakeDatabase = fakeDatabase;

            Connection = connection;
            _parameters = new FakeParameterCollection();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes a Transact-SQL statement against the FakeDatabase. Excutes an event.
        /// </summary>
        /// <returns>Always returns zero.</returns>
        public int ExecuteNonQuery()
        {
            var updateRecordRegex = new Regex(@"^update (\S+) set (\S[\S\s]+) where (\S+) = @id$", RegexOptions.IgnoreCase);
            var deleteRecordRegex = new Regex(@"^delete from (\S+) where (\S+) = @id$", RegexOptions.IgnoreCase);

            if (updateRecordRegex.IsMatch(CommandText))
            {
                var match = updateRecordRegex.Match(CommandText);

                var tableName = match.Groups[1].Value;

                _fakeDatabase.CallUpdateCallback(tableName);
                return 0;
            }
            else if (deleteRecordRegex.IsMatch(CommandText))
            {
                var match = deleteRecordRegex.Match(CommandText);

                var tableName = match.Groups[1].Value;

                _fakeDatabase.CallDeleteCallback(tableName);
                return 0;
            }

            _fakeDatabase.CallNonQuery(CommandText, Parameters);
            return 0;
        }

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
            var getAllReg = new Regex(@"^select [*] from (\S+)$", RegexOptions.IgnoreCase);
            var getSingleRecord = new Regex(@"^select [*] from (\S+) where (\S+) = @id$", RegexOptions.IgnoreCase);
            var insertRecord = new Regex(@"^insert into (\S+) \([^\)]+\) values \([^\)]+\);select scope_identity\(\) (\S+)$", RegexOptions.IgnoreCase);

            if (getAllReg.IsMatch(CommandText))
            {
                // Dapper Get all test
                var match = getAllReg.Match(CommandText);

                var tableName = match.Groups[1].Value;

                return _fakeDatabase.GetTableResults(tableName).ToDataReader();
            }
            else if (getSingleRecord.IsMatch(CommandText))
            {
                // Dapper get by Id test
                var match = getSingleRecord.Match(CommandText);

                var tableName = match.Groups[1].Value;
                var parameterName = match.Groups[2].Value;

                var idParameter = _parameters["id"] as IDbDataParameter;
                var id = idParameter.Value;

                string propertyName = null;
                return _fakeDatabase.GetTableResults(tableName).ToDataReader(o =>
                {
                    if (propertyName == null)
                    {
                        var type = o.GetType();
                        propertyName = type
                            .GetProperties()
                            .Where(p => String.Equals(p.Name, parameterName, StringComparison.CurrentCultureIgnoreCase))
                            .Select(p => p.Name)
                            .FirstOrDefault();
                    }

                    return Object.Equals(o.GetType().GetProperty(propertyName).GetValue(o, null), id);
                });
            }
            else if (insertRecord.IsMatch(CommandText))
            {
                var match = insertRecord.Match(CommandText);

                var tableName = match.Groups[1].Value;

                _fakeDatabase.CallInsertCallback(tableName);
                
                // Dapper insert logic return id of zero
                var a = new []
                {
                    new { SCOPE_IDENTITY = 0 }
                };

                return a.ToDataReader();
            }
            else if (CommandType == CommandType.StoredProcedure)
            {
                return _fakeDatabase.GetStoredProcedureResults(CommandText, Parameters).ToDataReader();
            }

            // Custom Sql logic
            return _fakeDatabase.GetSqlScriptResults(CommandText, Parameters).ToDataReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return ExecuteReader();
        }

        [ExcludeFromCodeCoverage] // Cancel does nothing so no need for code coverage.
        public void Cancel()
        {
            // Do nothing
        }

        public IDbDataParameter CreateParameter()
        {
            return new FakeParameter();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Do nothing
        }

        #endregion

    }

}
