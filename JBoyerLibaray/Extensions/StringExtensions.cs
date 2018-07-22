using JBoyerLibaray.Exceptions;
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
        public static string AddToEndOfFilename(this string filePath, string endOfFileName)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw ExceptionHelper.CreateArgumentException(() => filePath, "Cannot be null, empty, or whitespace");
            }

            if (String.IsNullOrEmpty(endOfFileName))
            {
                throw ExceptionHelper.CreateArgumentException(() => endOfFileName, "Cannot be null or empty");
            }

            var filename = Path.GetFileName(filePath);
            var filenameWOExt = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);

            return filePath.Replace(filename, filenameWOExt + endOfFileName + ext);
        }

        public static string CapitalizeEveryWord(this string line)
        {
            if (String.IsNullOrWhiteSpace(line))
            {
                return line;
            }

            char[] separateChars = new char[] { ' ', ';', ':', '!', '?', ',', '.', '_', '-', '/', '&', '\'', '(', '"', '\t' };
            char[] resultChars = line.ToLower().ToCharArray();

            for (int i = 0; i < resultChars.Length; i++)
            {
                char lastChar = i > 0 ? resultChars[i - 1] : ' ';

                if (separateChars.Contains(lastChar))
                {
                    resultChars[i] = resultChars[i].ToUpper();
                }
            }

            return String.Join(String.Empty, resultChars);
        }

        public static string CapitalizeFirstChar(this string word)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                return word;
            }

            return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
        }

        public static bool Contains(this string container, string match, StringComparison stringComparison)
        {
            return container.IndexOf(match, 0, stringComparison) != -1;
        }

        public static bool Like(this string toSearch, string toFind)
        {
            return Like(toSearch, toFind, false);
        }

        public static bool Like(this string toSearch, string toFind, bool ingoreCase)
        {
            if (ingoreCase)
            {
                toSearch = toSearch.ToUpper();
                toFind = toFind.ToUpper();
            }

            // Cleans up tofind string to work with regex
            Regex regCleanup = new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\");
            string regCleanedStr = regCleanup.Replace(toFind, ch => @"\" + ch);

            // Replace Sql Wild Cards To Regex wild cards
            string regString = String.Format(@"\A{0}\z", regCleanedStr.Replace('_', '.').Replace("%", ".*"));

            return new Regex(regString, RegexOptions.Singleline).IsMatch(toSearch);
        }

        public static string Repeat(this string str, int times)
        {
            if (times < 2)
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => times, "Cannot be less than 2", times);
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < times; i++)
            {
                sb.Append(str);
            }

            return sb.ToString();
        }

        public static string SplitCamelCase(this string word, char delimitar = ' ')
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                return word;
            }
            
            var reg = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                (?<=[^A-Z])(?=[A-Z]) |
                (?<=[A-Za-z])(?=[^A-Za-z])",
                RegexOptions.IgnorePatternWhitespace
            );

            return reg.Replace(word, delimitar.ToString());
        }

        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(str));

            return stream;
        }

    }
}
