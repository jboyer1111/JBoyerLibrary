using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public class RespectNumbersFilenameComparer : IComparer<string>
    {
        private static Regex decimalTest = new Regex(@"\d");

        public int Compare(string x, string y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            char[] xChars = x.ToCharArray();
            char[] yChars = y.ToCharArray();

            return InternalComapare(xChars, yChars, 0);
        }

        private int InternalComapare(char[] x, char[] y, int pos)
        {
            if (x.Length == pos && y.Length == pos)
                return 0;
            if (x.Length == pos)
                return -1;
            if (y.Length == pos)
                return 1;
            
            string xChar = x[pos].ToString();
            string yChar = y[pos].ToString();

            if (decimalTest.IsMatch(xChar) && decimalTest.IsMatch(yChar))
            {
                int xNum = GetFullNumber(x, pos);
                int yNum = GetFullNumber(y, pos);
                if (xNum < yNum)
                    return -1;
                if (yNum < xNum)
                    return 1;
                // If equal then keep reading the rest of the string using the new positions for comparing
                pos = pos + xNum.ToString().Length - 1;
            }
            else
            {
                int result = String.Compare(xChar, yChar);
                if (result != 0)
                    return result;
            }
            
            return InternalComapare(x, y, ++pos);
        }

        private int GetFullNumber(char[] chars, int startPos)
        {
            string resultStr = String.Empty;
            for (int i = startPos; i < chars.Length; i++)
            {
                if (!decimalTest.IsMatch(chars[i].ToString()))
                    break;

                resultStr += chars[i];
            }
            return Int16.Parse(resultStr);
        }
    }
}
