using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Database
{
    public static class DataReaderHelper
    {
        public static List<DataResult> GetData(IDataReader reader)
        {
            var resultSets = new List<DataResult>();

            do
            {
                resultSets.Add(new DataResult(reader));
            } while (reader.NextResult());

            return resultSets;
        }


        public static List<DataResult> ExecuteReaderToData(this IDbCommand command)
        {
            return GetData(command.ExecuteReader());
        }
    }
}
