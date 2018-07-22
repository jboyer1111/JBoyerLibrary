using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Database
{
    public class DataResult : IEnumerable
    {
        private List<dynamic> _rows = new List<dynamic>();

        public IEnumerable<dynamic> Rows
        {
            get
            {
                return _rows;
            }
        }

        public DataResult(IDataReader reader)
        {
            string[] columnNames = new string[0];

            while (reader.Read())
            {
                dynamic row = new ExpandoObject();

                if (columnNames.Length < 1)
                {
                    var unnamedColumns = 0;
                    columnNames = new string[reader.FieldCount];
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        var name = reader.GetName(i);
                        if (String.IsNullOrWhiteSpace(name))
                        {
                            name = String.Format("(No column name{0})", unnamedColumns > 0 ? " " + unnamedColumns : "");
                            unnamedColumns++;
                        }

                        columnNames[i] = name;
                    }
                }

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    ((IDictionary<string, object>)row).Add(columnNames[i], reader.GetValue(i));
                }

                _rows.Add(row);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _rows.GetEnumerator();
        }
    }

}
