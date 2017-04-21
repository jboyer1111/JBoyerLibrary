using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public static class ThueMorse
    {
        public static string GetSquenceLengthOf(int length)
        {
            // If zero or negative number return null
            if (length <= 0)
            {
                return null;
            }

            if (length == 1)
            {
                return "A";
            }

            string result = "AB";
            if (length == 2)
            {
                return result;
            }

            result = GetSquenceLengthOf(result, length);

            return result;
        }

        private static string GetSquenceLengthOf(string sequence, int length)
        {
            if (sequence.Length < length)
            {
                MatchEvaluator matchEvaluator = match =>
                {
                    if (match.ToString() == "A")
                    {
                        return "AB";
                    }
                    else
                    {
                        return "BA";
                    }
                };

                string newSequence = Regex.Replace(sequence, "A|B", matchEvaluator);

                return GetSquenceLengthOf(newSequence, length);
            }

            return sequence.Substring(0, length);
        }










    }
}
