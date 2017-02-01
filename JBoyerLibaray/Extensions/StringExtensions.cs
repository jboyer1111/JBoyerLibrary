using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JBoyerLibaray.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string str, int times)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < times; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        public static bool Like(this string toSearch, string toFind)
        {
            // Cleans up tofind string to work with regex
            Regex regCleanup = new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\");
            string regCleanedStr = regCleanup.Replace(toFind, ch => @"\" + ch);

            // Replace Sql Wild Cards To Regex wild cards
            string regString = String.Format(@"\A{0}\z", regCleanedStr.Replace('_', '.').Replace("%", ".*"));

            return new Regex(regString, RegexOptions.Singleline).IsMatch(toSearch);
        }

        public static bool Like(this string toSearch, string toFind, bool ingoreCase)
        {
            bool result;
            if (ingoreCase)
            {
                result = Like(toSearch.ToUpperInvariant(), toFind.ToUpperInvariant());
            }
            else
            {
                result = Like(toSearch, toFind);
            }
            return result;
        }

        public static string CapitalizeEveryWord(this string line)
        {
            char[] separateChars = new char[] { ' ', ';', ':', '!', '?', ',', '.', '_', '-', '/', '&', '\'', '(', '"', '\t' };
            char[] resultChars = line.ToLower().ToCharArray();

            for (int i = 0; i < resultChars.Length; i++)
            {
                char lastChar = i > 0 ? resultChars[i-1] : ' ';

                if (separateChars.Contains(lastChar))
                {
                    resultChars[i] = resultChars[i].ToString().ToUpper().ToCharArray().First();
                }
            }

            return String.Join(String.Empty, resultChars);
        }

        public static string CapitalizeFirstChar(this string word)
        {
            var chars = word.ToLower().ToCharArray();
            chars[0] = chars[0].ToString().ToUpper().ToCharArray().First();
            string result = String.Join(String.Empty, chars);

            string test = word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();


            return result;
        }

        public static string AddToEndOfFilename(this string filePath, string endOfFileName)
        {
            var filename = Path.GetFileName(filePath);
            var filenameWOExt = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);

            return filePath.Replace(filename, filenameWOExt + endOfFileName + ext);
        }

        public static Stream ToStream(this string str)
        {
            Stream stream = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode, str.Length, true) { AutoFlush = true })
            {
                streamWriter.Write(str);
            }
            return stream;
        }
    }
}
