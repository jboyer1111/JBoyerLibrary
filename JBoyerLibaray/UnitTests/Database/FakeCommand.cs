using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    public class FakeCommand : IDbCommand
    {
        #region Private Variables

        private IDataParameterCollection _parameters;

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

        public FakeCommand(IDbConnection connection)
        {
            Connection = connection;
            _parameters = new FakeParameterCollection();
        }

        #endregion

        #region Public Methods

        

        

        #endregion

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
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Do nothing
        }
    }
}
