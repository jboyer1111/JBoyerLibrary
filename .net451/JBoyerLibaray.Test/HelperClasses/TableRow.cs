using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.HelperClasses
{

    [ExcludeFromCodeCoverage]
    public class TableRow
    {

        public DateTime CreatedOn { get; private set; }

        public int Value { get; private set; }

        public TableRow(int value)
        {
            CreatedOn = DateTime.Now;
            Value = value;
        }

    }

}
