using JBoyer.Testing.DataReader;
using System.Data;

namespace JBoyer.Testing.Database
{

    public static class Extentions
    {

        public static EnumerableDataReader ToDataReader<T>(this IEnumerable<T> collection)
        {
            return new EnumerableDataReader(collection);
        }

        public static EnumerableDataReader ToDataReader<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            var type = EnumerableDataReader.CalculateType(collection);

            return new EnumerableDataReader(collection.Where(predicate), type);
        }

        public static T GetParamValue<T>(this IDataParameterCollection parameters, string? name)
        {
            IDataParameter parameter = parameters
                .Cast<IDataParameter>()
                .Where(p => string.Equals(p.ParameterName, name, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault() ?? throw new InvalidOperationException($"Unable to find parameter: {name}");

            if (parameter.Value == null)
            {
                throw new InvalidOperationException("Parameter value is null");
            }

            return (T)parameter.Value;
        }

    }

}
