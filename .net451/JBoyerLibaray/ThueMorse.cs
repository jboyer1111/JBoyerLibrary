using System.Text.RegularExpressions;

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

            result = getSquenceLengthOf(result, length);

            return result;
        }

        private static string getSquenceLengthOf(string sequence, int length)
        {
            if (sequence.Length >= length)
            {
                return sequence.Substring(0, length);
            }

            string newSequence = Regex.Replace(sequence, "A|B", m =>
            {
                if (m.Value == "A")
                {
                    return "AB";
                }
                else
                {
                    return "BA";
                }
            });

            return getSquenceLengthOf(newSequence, length);
        }

    }

}
