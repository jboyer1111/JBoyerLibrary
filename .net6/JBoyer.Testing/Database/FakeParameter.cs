using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Holds Paramter info
    /// </summary>
    [ExcludeFromCodeCoverage] // Only has constructors and Properties no coverage needed
    public class FakeParameter : IDbDataParameter
    {

        #region Public Properties

        public DbType DbType { get; set; }

        public ParameterDirection Direction { get; set; }

        public string ParameterName { get; set; }

        public byte Precision { get; set; }

        public byte Scale { get; set; }

        public int Size { get; set; }

        public string SourceColumn { get; set; }

        public DataRowVersion SourceVersion { get; set; }

        public object? Value { get; set; }

        public bool IsNullable { get; private set; }

        #endregion

        #region Constructor

        public FakeParameter() : this("", null) { }

        public FakeParameter(string parameterName, DbType dbType) : this(parameterName, dbType, 0) { }

        public FakeParameter(string parameterName, object? value) : this(parameterName, DbType.Object, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, value)
        {
            DbType dbType;
            if (value == null || !Enum.TryParse(value.GetType().Name, out dbType))
            {
                dbType = DbType.String;
            }

            if (dbType == DbType.String && value is string valueStr)
            {
                Size = valueStr.Length;
            }
        }

        public FakeParameter(string parameterName, DbType dbType, int size) : this(parameterName, dbType, size, "") { }

        public FakeParameter(string parameterName, DbType dbType, int size, string sourceColumn) : this(parameterName, dbType, size, ParameterDirection.Input, false, 0, 0, sourceColumn, DataRowVersion.Current, null) { }

        public FakeParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object? value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
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

        #endregion

    }

}
