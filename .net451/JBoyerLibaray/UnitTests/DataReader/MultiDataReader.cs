using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.UnitTests.DataReader
{

    /// <summary>
    /// Combines multiple DataReaders into one. Only the first result of each reader is read.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MultiDataReader : IDataReader
    {

        #region Private Variables

        private int _index = 0;
        private IDataReader[] _readers;

        #endregion

        #region Constructor

        /// <summary>
        /// This object loops through a list of datareaders when NextResult is called
        /// </summary>
        /// <param name="dataReaders"></param>
        public MultiDataReader(IEnumerable<IDataReader> dataReaders)
        {
            if (dataReaders == null)
            {
                throw new ArgumentNullException(nameof(dataReaders));
            }

            if (dataReaders.Count() < 2)
            {
                throw new ArgumentException("Must have at least 2 data readers.", nameof(dataReaders));
            }

            _readers = dataReaders.ToArray();
        }

        #endregion

        #region Interface Methods

        public object this[int i] => _readers[_index][i];

        public object this[string name] => _readers[_index][name];

        public int Depth => _readers[_index].Depth;

        public bool IsClosed => _readers[_index].IsClosed;

        public int RecordsAffected => _readers[_index].RecordsAffected;

        public int FieldCount => _readers[_index].FieldCount;

        public void Close()
        {
            foreach (IDataReader reader in _readers)
            {
                reader?.Close();
            }
        }

        public void Dispose()
        {
            foreach (IDataReader reader in _readers)
            {
                reader?.Dispose();
            }
        }

        public bool GetBoolean(int i) => _readers[_index].GetBoolean(i);

        public byte GetByte(int i) => _readers[_index].GetByte(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => _readers[_index].GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public char GetChar(int i) => _readers[_index].GetChar(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => _readers[_index].GetChars(i, fieldoffset, buffer, bufferoffset, length);

        public IDataReader GetData(int i) => _readers[_index].GetData(i);

        public string GetDataTypeName(int i) => _readers[_index].GetDataTypeName(i);

        public DateTime GetDateTime(int i) => _readers[_index].GetDateTime(i);

        public decimal GetDecimal(int i) => _readers[_index].GetDecimal(i);

        public double GetDouble(int i) => _readers[_index].GetDouble(i);

        public Type GetFieldType(int i) => _readers[_index].GetFieldType(i);

        public float GetFloat(int i) => _readers[_index].GetFloat(i);

        public Guid GetGuid(int i) => _readers[_index].GetGuid(i);

        public short GetInt16(int i) => _readers[_index].GetInt16(i);

        public int GetInt32(int i) => _readers[_index].GetInt32(i);

        public long GetInt64(int i) => _readers[_index].GetInt64(i);

        public string GetName(int i) => _readers[_index].GetName(i);

        public int GetOrdinal(string name) => _readers[_index].GetOrdinal(name);

        public DataTable GetSchemaTable() => _readers[_index].GetSchemaTable();

        public string GetString(int i) => _readers[_index].GetString(i);

        public object GetValue(int i) => _readers[_index].GetValue(i);

        public int GetValues(object[] values) => _readers[_index].GetValues(values);

        public bool IsDBNull(int i) => _readers[_index].IsDBNull(i);

        public bool NextResult()
        {
            _index++;
            return _index < _readers.Length;
        }

        public bool Read() => _readers[_index].Read();

        #endregion

    }

}
