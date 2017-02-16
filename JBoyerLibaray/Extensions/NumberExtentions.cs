using JBoyerLibaray.Exceptions;
using System;
using System.Text;

namespace JBoyerLibaray.Extensions
{
    public static class NumberExtentions
    {

        /// <summary>
        ///    http://www.blackwasp.co.uk/Code.aspx?file=NumberToRoman
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static string ToRomanNumeral(this int number)
        {
            //
            // Validate.
            //
            if (number < 0 || number > 3999)
            {
                throw ExceptionHelper.CreateArgumentOutOfRangeException(() => number, "Value must be in the range 0 - 3,999", number);
            }

            if (number == 0) { return "N"; }

            //
            // Set up key numerals and numeral pairs.
            //
            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] numerals = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            StringBuilder result = new StringBuilder();


            //
            // Loop through each of the values to diminish the number.
            //
            for (int i = 0; i < 13; i++)
            {
                //
                // If the number being converted is less than the test value, append
                // the corresponding numeral or numeral pair to the resultant string.
                //
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }
            }

            return result.ToString();
        }

        public static string TimeInMillisecondsToHumanReadableString(this int milliseconds)
        {
            TimeSpan totalTime = TimeSpan.FromMilliseconds(System.Convert.ToDouble(milliseconds));
            StringBuilder totalTimeToWait = new StringBuilder();

            if (totalTime.Days > 0)
            {
                totalTimeToWait.AppendFormat("{0} day", totalTime.Days);

                if (totalTime.Days > 1)
                {
                    totalTimeToWait.Append("s");
                }
            }

            if (totalTime.Hours > 0)
            {
                if (totalTimeToWait.Length > 0)
                {
                    totalTimeToWait.Append(", ");
                }

                totalTimeToWait.AppendFormat("{0} hour", totalTime.Hours);

                if (totalTime.Hours > 1)
                {
                    totalTimeToWait.Append("s");
                }
            }

            if (totalTime.Minutes > 0)
            {
                if (totalTimeToWait.Length > 0)
                {
                    totalTimeToWait.Append(", ");
                }

                totalTimeToWait.AppendFormat("{0} minute", totalTime.Minutes);

                if (totalTime.Minutes > 1)
                {
                    totalTimeToWait.Append("s");
                }
            }

            if (totalTime.Seconds > 0)
            {
                if (totalTimeToWait.Length > 0)
                {
                    totalTimeToWait.Append(", ");
                }

                totalTimeToWait.AppendFormat("{0} second", totalTime.Seconds);

                if (totalTime.Seconds > 1)
                {
                    totalTimeToWait.Append("s");
                }
            }
            else
            {
                if (totalTimeToWait.Length == 0)
                {
                    totalTimeToWait.Append("0 seconds");
                }
            }

            return totalTimeToWait.ToString();
        }


        /// <summary>
        /// Converts   1 -> A
        ///            2 -> B
        ///           27 -> AA
        ///          702 -> ZZ
        ///          703 -> AAA
        /// </summary>
        /// <exception cref="System.ArgumentException"></exception>
        public static string ToExcelColumnName(this int column)
        {
            if (column <= 0)
            {
                throw ExceptionHelper.CreateArgumentException(() => column, "Excel file naming scheme only works on postive numbers");
            }
            
            string columnString = "";
            decimal columnNumber = column;

            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);

                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }

            return columnString;
        }
    }
}
