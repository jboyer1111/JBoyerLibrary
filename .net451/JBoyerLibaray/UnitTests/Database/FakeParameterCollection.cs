using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    /// <summary>
    /// Collection for holding CommandParamters
    /// </summary>
    public class FakeParameterCollection : List<IDbDataParameter>, IDataParameterCollection
    {

        #region Constructor

        [ExcludeFromCodeCoverage]
        public FakeParameterCollection() : base() { }

        [ExcludeFromCodeCoverage]
        public FakeParameterCollection(int capacity) : base(capacity) { }

        [ExcludeFromCodeCoverage]
        public FakeParameterCollection(IEnumerable<IDbDataParameter> collection) : base(collection) { }

        #endregion

        #region Public Methods

        public bool Contains(string parameterName)
        {
            return this
                .Select(p => p.ParameterName)
                .Contains(parameterName, StringComparer.CurrentCultureIgnoreCase);
        }

        public int IndexOf(string parameterName)
        {
            return this
                .Select(p => p.ParameterName.ToLowerInvariant())
                .ToList()
                .IndexOf(parameterName.ToLowerInvariant());
        }

        public void RemoveAt(string parameterName)
        {
            RemoveAt(IndexOf(parameterName));
        }

        #endregion

        #region Indexer

        public IDbDataParameter this[string parameterName]
        {
            get
            {
                var parameter = this
                    .Where(p => String.Equals(p.ParameterName, parameterName, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

                if (parameter == null)
                {
                    throw new IndexOutOfRangeException($"An IDataParameter with ParameterName '{parameterName}' is not contained by this FakeParameterCollection.");
                }

                return parameter;
            }
            set
            {
                if (IndexOf(parameterName) < 0)
                {
                    throw new IndexOutOfRangeException($"An IDataParameter with ParameterName '{parameterName}' is not contained by this FakeParameterCollection.");
                }

                RemoveAt(parameterName);
                Add(value);
            }
        }

        object IDataParameterCollection.this[string parameterName]
        {
            get
            {
                return this[parameterName];
            }
            set
            {
                this[parameterName] = (IDbDataParameter)value;
            }
        }

        #endregion

    }

}
