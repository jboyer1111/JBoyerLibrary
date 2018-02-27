﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    [ExcludeFromCodeCoverage]
    public class FakeCommand : IDbCommand
    {
        #region Private Variables

        private IDataParameterCollection _parameters;
        private FakeDatabase _fakeDatabase;

        #endregion

        #region Public Properties

        public string CommandText { get; set; }

        public int CommandTimeout { get; set; }

        public CommandType CommandType { get; set; }

        public IDbConnection Connection { get; set; }

        public IDbTransaction Transaction { get; set; }

        public UpdateRowSource UpdatedRowSource { get; set; }

        public IDataParameterCollection Parameters
        {
            get { return _parameters; }
        }

        #endregion

        #region Constructor

        public FakeCommand(IDbConnection connection, FakeDatabase fakeDatabase)
        {
            Connection = connection;
            _parameters = new FakeParameterCollection();
            _fakeDatabase = fakeDatabase;
        }

        #endregion

        #region Public Methods

        public int ExecuteNonQuery()
        {
            // Do nothing
            // Integration test these types of queries
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
            var lowerCommandText = CommandText.ToLowerInvariant();
            var getAllReg = new Regex(@"^select [*] from (\S+)$");
            var getSingleRecord = new Regex(@"^select [*] from (\S+) where (\S+) = @id$");
            var insertRecord = new Regex(@"^insert into (\S+) \([^\)]+\) values \([^\)]+\);select scope_identity\(\) (\S+)$");

            if (getAllReg.IsMatch(lowerCommandText))
            {
                // Dapper Get all test
                var match = getAllReg.Match(lowerCommandText);

                var tableName = match.Groups[1].Value;

                return _fakeDatabase.GetTableResults(tableName).ToDataReader();
            }
            else if (getSingleRecord.IsMatch(lowerCommandText))
            {
                // Dapper get by Id test
                var match = getSingleRecord.Match(lowerCommandText);

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
            else if (insertRecord.IsMatch(lowerCommandText))
            {
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
