using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            TimeSpan totalTime = TimeSpan.FromMilliseconds(Convert.ToDouble(milliseconds));
            List<string> timeParts = new List<string>();

            // Get pieces
            if (totalTime.Days > 0)
            {
                timeParts.Add(pluralFormat(totalTime.Days, "day"));
            }

            if (totalTime.Hours > 0)
            {
                timeParts.Add(pluralFormat(totalTime.Hours, "hour"));
            }

            if (totalTime.Minutes > 0)
            {
                timeParts.Add(pluralFormat(totalTime.Minutes, "minute"));
            }

            if (totalTime.Seconds > 0)
            {
                timeParts.Add(pluralFormat(totalTime.Seconds, "second"));
            }
            else if (timeParts.Count == 0)
            {
                if (totalTime.Milliseconds > 0)
                {
                    timeParts.Add(String.Format("{0} seconds", totalTime.TotalSeconds));
                }
                else
                {
                    timeParts.Add("0 seconds");
                }
            }

            if (timeParts.Count > 2)
            {
                return String.Join(", ", timeParts.Take(timeParts.Count - 1)) + ", and " + timeParts.Last();
            }
            else if (timeParts.Count == 2)
            {
                return String.Join(" and ", timeParts);
            }
            else
            {
                return timeParts.First();
            }
        }

        private static string pluralFormat(int number, string unit)
        {
            return String.Format(
                "{0} {1}{2}",
                number,
                unit,
                number != 1 ? "s" : ""
            );
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
