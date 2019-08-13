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
    public class FakeParameterCollection : List<IDbDataParameter>, IDataParameterCollection
    {

        #region Constructor

        public FakeParameterCollection() : base() { }

        public FakeParameterCollection(int capacity) : base(capacity) { }

        public FakeParameterCollection(IEnumerable<IDbDataParameter> collection) : base(collection) { }

        #endregion

        #region Public Methods

        public bool Contains(string parameterName)
        {
            return this.Select(p => p.ParameterName).Contains(parameterName, StringComparer.CurrentCultureIgnoreCase);
        }

        public int IndexOf(string parameterName)
        {
            return this.Select(p => p.ParameterName.ToLowerInvariant()).ToList().IndexOf(parameterName.ToLowerInvariant());
        }

        public void RemoveAt(string parameterName)
        {
            this.RemoveAt(IndexOf(parameterName));
        }

        #endregion


        public IDbDataParameter this[string parameterName]
        {
            get
            {
                return this.Where(p => String.Equals(p.ParameterName, parameterName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            }
            set
            {
                var parameter = this.Where(p => String.Equals(p.ParameterName, parameterName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (parameter == null)
                {
                    throw new IndexOutOfRangeException(String.Format(
                        "An IDbDataParameter with ParameterName '{0}' is not contained by this FakeParameterCollection.",
                        parameterName
                    ));
                }

                parameter = value;
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
                throw new Exception();
            }
        }


    }
}
