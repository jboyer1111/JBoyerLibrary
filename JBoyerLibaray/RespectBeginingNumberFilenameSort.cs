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

            Match xMatch = _reg.Match(x);
            Match yMatch = _reg.Match(y);
            string xStr = null;
            string yStr = null;
            if (xMatch.Success)
            {
                xStr = xMatch.ToString();
            }
            if (yMatch.Success)
            {
                yStr = yMatch.ToString();
            }

            // If both have numbers at the begining then use that to determine what comes first.
            if (xStr != null && yStr != null)
            {
                int xNum = Int32.Parse(xStr);
                int yNum = Int32.Parse(yStr);

                if (xNum < yNum)
                {
                    return -1;
                }
                if (yNum < xNum)
                {
                    return 1;
                }
                // If the numbers are equal then just use string compare on the file name.
            }

            return String.Compare(x, y);
        }
    }
}
