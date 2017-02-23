using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests.Database
{
    public class FakeParameterCollection : IDataParameterCollection
    {
        #region Private Variables

        private List<IDataParameter> _parameters;

        #endregion

        #region Public Properties

        public int Count
        {
            get
            {
                return _parameters.Count();
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return null;
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Constructor

        public FakeParameterCollection()
        {
            _parameters = new List<IDataParameter>();
        }

        #endregion

        #region Public Methods

        public bool Contains(string parameterName)
        {
            return _parameters.Select(p => p.ParameterName).Contains(parameterName);
        }

        public int IndexOf(string parameterName)
        {
            return _parameters.Select(p => p.ParameterName).ToList().IndexOf(parameterName);
        }

        public void RemoveAt(string parameterName)
        {
            if (!Contains(parameterName))
            {
                return;
            }

            var parameter = _parameters.Where(p => p.ParameterName == parameterName).FirstOrDefault();
            _parameters.Remove(parameter);
        }

        public object this[string parameterName]
        {
            get
            {
                if (!Contains(parameterName))
                {
                    return null;
                }

                return _parameters.Where(p => p.ParameterName == parameterName).Select(p => p.Value).FirstOrDefault();
            }
            set
            {
                if (!Contains(parameterName))
                {
                    throw ExceptionHelper.CreateArgumentException(() => parameterName, "Parameter not in list");
                }

                var parameter = _parameters.Where(p => p.ParameterName == parameterName).FirstOrDefault();
                parameter.Value = value;
            }
        }

        public void Clear()
        {
            _parameters.Clear();
        }

        public bool Contains(object value)
        {
            return _parameters.Select(p => p.Value).Contains(value);
        }

        public int IndexOf(object value)
        {
            return _parameters.Select(p => p.Value).ToList().IndexOf(value);
        }

        public int Add(object value)
        {
            var parameter = value as IDataParameter;
            if (parameter == null)
            {
                throw ExceptionHelper.CreateArgumentOutOfRangeException(() => value, "value is not a data parameter", value);
            }

            _parameters.Add(parameter);

            return _parameters.IndexOf(parameter);
        }

        public void Insert(int index, object value)
        {
            var parameter = value as IDataParameter;
            if (parameter == null)
            {
                throw ExceptionHelper.CreateArgumentOutOfRangeException(() => value, "value is not a data parameter", value);
            }

            _parameters.Insert(index, parameter);
        }

        public void Remove(object value)
        {
            var parameter = value as IDataParameter;
            if (parameter == null)
            {
                throw ExceptionHelper.CreateArgumentOutOfRangeException(() => value, "value is not a data parameter", value);
            }

            _parameters.Remove(parameter);
        }

        public void RemoveAt(int index)
        {
            _parameters.RemoveAt(index);
        }

        public object this[int index]
        {
            get
            {
                return _parameters[index].Value;
            }
            set
            {
                _parameters[index].Value = value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
