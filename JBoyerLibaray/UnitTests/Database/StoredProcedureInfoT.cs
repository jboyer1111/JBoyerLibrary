using JBoyerLibaray.Exceptions;
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
    public class StoredProcedureInfo<T> : StoredProcedureInfo where T : class
    {
        #region Constructor

        public StoredProcedureInfo(T result, IEnumerable<string> requiredParameters)
        {
            if (requiredParameters == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => requiredParameters);
            }

            _results = new List<T>() { result };
            _requiredParameters = requiredParameters.OrderBy(s => s).ToArray();
        }

        public StoredProcedureInfo(IEnumerable<T> results, IEnumerable<string> requiredParameters)
        {
            if (results == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => results);
            }

            if (requiredParameters == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => requiredParameters);
            }

            _results = results;
            _requiredParameters = requiredParameters.OrderBy(s => s).ToArray();
        }

        public StoredProcedureInfo(Func<FakeDatabase, IDataParameterCollection, IEnumerable<T>> objectResultResolver, IEnumerable<string> requiredParameters)
        {
            if (objectResultResolver == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => objectResultResolver);
            }

            if (requiredParameters == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => requiredParameters);
            }

            _objectResultResolver = objectResultResolver;
            _requiredParameters = requiredParameters;
        }

        #endregion
    }
}
