using System;
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
        private Dictionary<string, IEnumerable<object>> _testData;

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

        public FakeCommand(IDbConnection connection, Dictionary<string, IEnumerable<object>> testData)
        {
            Connection = connection;
            _parameters = new FakeParameterCollection();
            _testData = testData;
        }

        #endregion

        #region Public Methods

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            var lowerCommandText = CommandText.ToLowerInvariant();
            Regex getAllReg = new Regex(@"^select [*] from (\S+)$");
            Regex getSingleRecord = new Regex(@"^select [*] from (\S+) where id = @id$");

            if (getAllReg.IsMatch(lowerCommandText))
            {
                var match = getAllReg.Match(lowerCommandText);

                var tableName = match.Groups[1].Value;

                return _testData[tableName].ToDataReader();
            }
            else if (getSingleRecord.IsMatch(lowerCommandText))
            {
                var match = getSingleRecord.Match(lowerCommandText);

                var tableName = match.Groups[1].Value;
                var idParameter = _parameters["id"] as IDbDataParameter;
                int id = (int)idParameter.Value;

                var test = _testData[tableName];

                return _testData[tableName].ToDataReader(o => (int)o.GetType().GetProperty("Id").GetValue(o, null) == id);
            }

            return null;
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
