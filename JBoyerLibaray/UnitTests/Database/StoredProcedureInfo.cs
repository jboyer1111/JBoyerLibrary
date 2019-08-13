﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.UnitTests.Database
{

    [ExcludeFromCodeCoverage]
    public abstract class StoredProcedureInfo
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
                    throw new InvalidOperationException("Stored Procedure is missing a required Parameter with the name \"" + param + "\"");
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
                    throw new InvalidOperationException("Stored Procedure is missing a required Parameter with the name \"" + param + "\"");
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
