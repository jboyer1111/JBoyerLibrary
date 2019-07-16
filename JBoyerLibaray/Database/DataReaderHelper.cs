using System.Collections.Generic;
using System.Data;

namespace JBoyerLibaray.Database
{

    public static class DataReaderHelper
    {

        public static List<DataResult> GetData(this IDataReader reader)
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
