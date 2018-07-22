using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    public abstract class SqlInfo
    {
        #region Private Variables

        protected Func<FakeDatabase, IDataParameterCollection, IEnumerable<object>> _objectResultResolver;
        protected IEnumerable<object> _results;
        protected IEnumerable<string> _requiredParameters;

        #endregion

        #region Public Mehtods

        public IEnumerable<object> GetResults(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            foreach (var param in _requiredParameters)
            {
                var paramater = passedParams[param];
                if (paramater == null)
                {
                    throw new InvalidOperationException("Sql is missing a required Parameter with the name \"" + param + "\"");
                }
            }
            
            if (_objectResultResolver != null)
            {
                return _objectResultResolver(fakeDatabase, passedParams);
            }

            return _results;
        }

        public object GetScalar(FakeDatabase fakeDatabase, IDataParameterCollection passedParams)
        {
            foreach (var param in _requiredParameters)
            {
                var paramater = passedParams[param];
                if (paramater == null)
                {
                    throw new InvalidOperationException("Sql is missing a required Parameter with the name \"" + param + "\"");
                }
            }

            IDataReader resultReader;
            if (_objectResultResolver != null)
            {
                resultReader = _objectResultResolver(fakeDatabase, passedParams).ToDataReader();
            }
            else
            {
                resultReader = _results.ToDataReader();
            }

            object result;
            if (resultReader.Read())
            {
                result = resultReader.GetValue(0);
            }
            else
            {
                result = -1;
            }

            return result;
        }

        #endregion
    }
}
