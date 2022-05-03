using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JBoyerLibaray
{

    public class RespectNumbersFilenameComparer : IComparer<string>
    {

        private static Regex decimalTest = new Regex(@"\d");

        public int Compare(string x, string y)
        {
            // Null Test Logic
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            if (!decimalTest.IsMatch(x) || !decimalTest.IsMatch(y))
            {
                return String.Compare(x, y);
            }

            char[] xChars = x.ToCharArray();
            char[] yChars = y.ToCharArray();

            return internalComapare(xChars, yChars, 0);
        }

        private int internalComapare(char[] x, char[] y, int pos)
        {
            // End of String Tests
            if (x.Length == pos && y.Length == pos)
            {
                return 0;
            }

            if (x.Length == pos)
            {
                return -1;
            }

            if (y.Length == pos)
            {
                return 1;
            }
            
            // Get char at position as String
            string xChar = x[pos].ToString();
            string yChar = y[pos].ToString();

            // Test if they both have a number
            if (decimalTest.IsMatch(xChar) && decimalTest.IsMatch(yChar))
            {
                // Get the entire number form the string at this point
                string xNumStr = getFullNumber(x, pos);
                string yNumStr = getFullNumber(y, pos);

                int xNum = Int32.Parse(xNumStr);
                int yNum = Int32.Parse(yNumStr);

                // Compare numbers and if they are not equal then return the correct value
                if (xNum < yNum)
                {
                    return -1;
                }
                if (xNum > yNum)
                {
                    return 1;
                }

                // If they are equal then Compare the remaining text
                // Normally the lengths will be the same but this is to correctly account for leading zeros
                char[] newX = x.Skip(pos + xNumStr.Length).ToArray();
                char[] newy = y.Skip(pos + yNumStr.Length).ToArray();

                return internalComapare(newX, newy, 0);
            }

            // Compare values of Char. If they are different we already know the order.
            int result = String.Compare(xChar, yChar);
            if (result != 0)
            {
                return result;
            }

            // Test the next position in the string
            return internalComapare(x, y, ++pos);
        }

        private string getFullNumber(char[] chars, int startPos)
        {
            // This calculates the number over multiple characters and returns the full value of the number
            string resultStr = String.Empty;
            for (int i = startPos; i < chars.Length; i++)
            {
                if (!decimalTest.IsMatch(chars[i].ToString()))
                {
                    break;
                }

                resultStr += chars[i];
            }

            return resultStr;
        }

    }

}
