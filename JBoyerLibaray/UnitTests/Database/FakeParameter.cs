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
    public class FakeParameter : IDbDataParameter
    {
        public FakeParameter() : this("", null) { }

        public FakeParameter(string parameterName, DbType dbType) : this(parameterName, dbType, 0) { }

        public FakeParameter(string parameterName, object value)
        {
            int size = 0;
            DbType dbType;
            if (value == null || !DbType.TryParse(value.GetType().Name, out dbType))
            {
                dbType = DbType.String;
            }

            if (dbType == DbType.String && value is String)
            {
                size = (value as String).Length;
            }
            
            construtorLogic(parameterName, dbType, size, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, value);
        }

        public FakeParameter(string parameterName, DbType dbType, int size) : this(parameterName, dbType, size, "") { }

        public FakeParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            construtorLogic(parameterName, dbType, size, ParameterDirection.Input, false, 0, 0, sourceColumn, DataRowVersion.Current, null);
        }

        public FakeParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            construtorLogic(parameterName, dbType, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
        }

        private void construtorLogic(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            ParameterName = parameterName;
            DbType = dbType;
            Size = size;
            Direction = direction;
            IsNullable = isNullable;
            Precision = precision;
            Scale = scale;
            SourceColumn = sourceColumn;
            SourceVersion = sourceVersion;
            Value = value;
        }

        public DbType DbType { get; set; }

        public ParameterDirection Direction { get; set; }

        public string ParameterName { get; set; }

        public byte Precision { get; set; }

        public byte Scale { get; set; }

        public int Size { get; set; }

        public string SourceColumn { get; set; }

        public DataRowVersion SourceVersion { get; set; }

        public object Value { get; set; }

        public bool IsNullable { get; private set; }
    }
}
