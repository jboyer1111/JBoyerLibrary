using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public static class PascalTriangle
    {
        public static int[] GetRow(int row)
        {
            if (row > 34)
            {
                throw new OverflowException("Passed int will cause overflow.");
            }
            if (row == 1)
            {
                return GetRow(row, null);
            }
            return GetRow(row, GetRow(row - 1));
        }

        private static int[] GetRow(int row, int[] prevRow)
        {
            // Recursive Exit
            if (row == 1)
            {
                return new int[] { 1 };
            }

            // Create array for math
            int[] prevRowWithZeros = new int[prevRow.Length + 2];
            prevRow.CopyTo(prevRowWithZeros, 1);

            // Calcuate the row
            int[] rtnVal = new int[prevRow.Length + 1];
            for (int i = 0; i < prevRowWithZeros.Length - 1; i++)
            {
                rtnVal[i] = prevRowWithZeros[i] + prevRowWithZeros[i + 1];
            }

            return rtnVal;
        }
    }
}
