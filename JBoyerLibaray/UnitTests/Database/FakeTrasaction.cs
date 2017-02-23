using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    public class FakeTrasaction : IDbTransaction
    {
        #region Private Variables

        private IDbConnection _connection = null;
        private IsolationLevel _isolationLevel = IsolationLevel.Unspecified;

        #endregion

        #region Public Properties

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                return _isolationLevel;
            }
        }

        #endregion

        #region Constructor

        public FakeTrasaction(IDbConnection connection, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _connection = connection;
            _isolationLevel = isolationLevel;
        }

        #endregion

        #region Public Methods

        public void Commit()
        {
            // Do nothing
        }

        public void Dispose()
        {
            // Do nothing
        }

        public void Rollback()
        {
            // Do nothing
        }

        #endregion
    }
}
