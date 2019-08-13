using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    [ExcludeFromCodeCoverage]
    public class SqlInfo<T> : SqlInfo where T : class
    {

        #region Constructor

        public SqlInfo(T result, IEnumerable<string> requiredParameters)
        {
            if (requiredParameters == null)
            {
                throw new ArgumentNullException(nameof(requiredParameters));
            }

            _results = new List<T>() { result };
            _requiredParameters = requiredParameters;
        }

        public SqlInfo(IEnumerable<T> results, IEnumerable<string> requiredParameters)
        {
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            if (requiredParameters == null)
            {
                throw new ArgumentNullException(nameof(requiredParameters));
            }

            _results = results;
            _requiredParameters = requiredParameters;
        }

        public SqlInfo(Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> objectResultResolver, IEnumerable<string> requiredParameters)
        {
            if (objectResultResolver == null)
            {
                throw new ArgumentNullException(nameof(objectResultResolver));
            }

            if (requiredParameters == null)
            {
                throw new ArgumentNullException(nameof(requiredParameters));
            }

            _objectResultResolver = objectResultResolver;
            _requiredParameters = requiredParameters;
        }

        #endregion

    }
}
