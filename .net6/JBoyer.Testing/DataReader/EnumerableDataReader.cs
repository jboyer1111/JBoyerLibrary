﻿using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing.DataReader
{

    [ExcludeFromCodeCoverage]
    public class EnumerableDataReader : ObjectDataReader
    {

        private readonly IEnumerator _enumerator;
        private readonly Type _type;
        private object _current;

        /// <summary>
        /// Create an IDataReader over an instance of IEnumerable&lt;>.
        /// 
        /// Note: anonymous type arguments are acceptable.
        /// 
        /// Use other constructor for IEnumerable.
        /// </summary>
        /// <param name="collection">IEnumerable&lt;>. For IEnumerable use other constructor and specify type.</param>
        public EnumerableDataReader(IEnumerable collection)
        {
            _type = CalculateType(collection);

            SetFields(_type);

            _enumerator = collection.GetEnumerator();

        }

        /// <summary>
        /// Create an IDataReader over an instance of IEnumerable.
        /// Use other constructor for IEnumerable&lt;>
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="elementType"></param>
        public EnumerableDataReader(IEnumerable collection, Type elementType)
            : base(elementType)
        {
            _type = elementType;
            _enumerator = collection.GetEnumerator();
        }

        /// <summary>
        /// Helper method to create generic lists from anonymous type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList ToGenericList(Type type)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new[] { type }));
        }

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> which will contain the field value upon return.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public override object GetValue(int i)
        {
            if (i < 0 || i >= Fields.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return Fields[i].Getter(_current);
        }

        /// <summary>
        /// Advances the <see cref="T:System.Data.IDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// true if there are more rows; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override bool Read()
        {
            bool returnValue = _enumerator.MoveNext();
            _current = returnValue ? _enumerator.Current : _type.IsValueType ? Activator.CreateInstance(_type) : null;
            return returnValue;
        }

        public override bool NextResult()
        {
            return false;
        }

        public static Type CalculateType(IEnumerable collection)
        {
            Type type = null;

            // THANKS DANIEL!
            foreach (Type intface in collection.GetType().GetInterfaces())
            {
                if (intface.IsGenericType && intface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    type = intface.GetGenericArguments()[0];
                }
            }

            if (type == null && collection.GetType().IsGenericType)
            {
                type = collection.GetType().GetGenericArguments()[0];
            }

            if (type == null)
            {
                throw new ArgumentException("Collection must be IEnumerable<>. Use other constructor for IEnumerable and specify type");
            }

            return type;
        }

    }

}
