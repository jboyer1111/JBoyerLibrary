using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public class RespectBeginingNumberFilenameSort : IComparer<string>
    {
        private Regex _reg = new Regex(@"^\d+");

        public int Compare(string x, string y)
        {
            // Check if values are both null
            if (x == null && y == null)
            {
                return 0;
            }

            // Check if x is null
            if (x == null)
            {
                return -1;
            }

            // Check if y is null
            if (y == null)
            {
                return 1;
            }

            // If both have numbers at the begining then use that to determine what comes first.
            Match xMatch = _reg.Match(x);
            Match yMatch = _reg.Match(y);
            if (xMatch.Success && yMatch.Success)
            {
                int xNum = Int32.Parse(xMatch.Value);
                int yNum = Int32.Parse(yMatch.Value);

                // Compare the two numbers and return value if they are not equal
                int compare = Comparer<int>.Default.Compare(xNum, yNum);
                if (compare != 0)
                {
                    return compare;
                }

                // If the numbers are equal then just use string compare on the file name.
            }

            return String.Compare(x, y);
        }
    }
}
