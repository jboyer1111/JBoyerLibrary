using JBoyerLibaray.UnitTests.DataReader;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Class for setting up sql statements or stored procudures that return multiple result sets.
    /// </summary>
    public class MultiResultSet
    {

        #region Private Variables

        private List<IEnumerable<object>> _resultSets = new List<IEnumerable<object>>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the current result info
        /// </summary>
        public IEnumerable<IEnumerable<object>> ResultSets => _resultSets;

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds list of objects to be the next result set in object
        /// </summary>
        public void Add<T>(IEnumerable<T> results) where T : class
        {
            _resultSets.Add(results);
        }

        /// <summary>
        /// Creates the DataReader for Dapper code to process against
        /// </summary>
        public IDataReader ToDataReader()
        {
            return new MultiDataReader(_resultSets.Select(r => r.ToDataReader()).ToArray());
        }

        #endregion

    }

}
