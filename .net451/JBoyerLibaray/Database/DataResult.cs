using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace JBoyerLibaray.Database
{

    public class DataResult : IEnumerable
    {

        #region Private Variables

        private string[] _columnNames = null;
        private List<dynamic> _rows = new List<dynamic>();

        #endregion

        #region Public Properteis

        [ExcludeFromCodeCoverage]
        public IEnumerable<dynamic> Rows => _rows;

        [ExcludeFromCodeCoverage]
        public IEnumerable<string> ColumnNames => _columnNames;

        #endregion

        #region Constructor

        public DataResult(IDataReader reader)
        {
            // Loop Through rows to build dynamic objects
            while (reader.Read())
            {
                dynamic row = new ExpandoObject();

                // Set column Names
                if (_columnNames == null)
                {
                    var unnamedColumns = 0;
                    _columnNames = new string[reader.FieldCount];
                    for (int i = 0; i < _columnNames.Length; i++)
                    {
                        var name = reader.GetName(i);
                        if (String.IsNullOrWhiteSpace(name))
                        {
                            name = String.Format("(No column name{0})", unnamedColumns > 0 ? " " + unnamedColumns : "");
                            unnamedColumns++;
                        }

                        _columnNames[i] = name;
                    }
                }

                // Build up Properties of Dynamic object
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    ((IDictionary<string, object>)row).Add(_columnNames[i], reader.GetValue(i));
                }

                _rows.Add(row);
            }
        }

        #endregion

        #region Public Methods

        public IEnumerator GetEnumerator() => _rows.GetEnumerator();

        #endregion

    }

}
