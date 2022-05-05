using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Class for holding row info for scalar result sets. 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ScalarValue<T>
    {

        public T Value { get; private set; }

        public ScalarValue(T value)
        {
            Value = value;
        }

    }

}
