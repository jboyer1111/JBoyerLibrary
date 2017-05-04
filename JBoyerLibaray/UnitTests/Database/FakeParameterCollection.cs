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
    public class FakeParameterCollection : List<IDbDataParameter>, IDataParameterCollection
    {
        public FakeParameterCollection() : base() { }

        public FakeParameterCollection(int capacity) : base(capacity) { }

        public FakeParameterCollection(IEnumerable<IDbDataParameter> collection) : base(collection) { }

        public IDbDataParameter this[string parameterName]
        {
            get
            {
                return this.Where(p => p.ParameterName == parameterName).FirstOrDefault();
            }
            set
            {
                var parameter = this.Where(p => p.ParameterName == parameterName).FirstOrDefault();
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

        public bool Contains(string parameterName)
        {
            return this.Select(p => p.ParameterName).Contains(parameterName);
        }

        public int IndexOf(string parameterName)
        {
            return this.Select(p => p.ParameterName).ToList().IndexOf(parameterName);
        }

        public void RemoveAt(string parameterName)
        {
            this.RemoveAt(IndexOf(parameterName));
        }
    }
}
