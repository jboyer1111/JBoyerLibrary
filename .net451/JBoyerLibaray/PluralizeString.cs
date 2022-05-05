using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JBoyerLibaray
{

    public static class PluralizeString
    {

        private static PluralizerPart[] replaceables = new PluralizerPart[] {
            new PluralizerPart("{A}", "a ", ""),
            new PluralizerPart("{A-#}", "a", "#"),
            new PluralizerPart("{AN}", "an ", ""),
            new PluralizerPart("{AN-#}", "an", "#"),
            new PluralizerPart("{IS-ARE}", "is", "are"),
            new PluralizerPart("{S}", "", "s"),
            new PluralizerPart("{ES}", "", "es"),
            new PluralizerPart("{Y-IES}", "y", "ies"),
            new PluralizerPart("{F-VES}", "f", "ves")
        };

        public static string Pluralize(string sentenace, int count)
        {
            var currReplaceables = new List<PluralizerPart>(replaceables);
            // Add Additional Parts
            var additionalParts = Regex.Matches(sentenace, @"{([^-{}]+)-([^-{}]+)}")
                            .Cast<Match>()
                            .Select(m => new PluralizerPart(m.ToString(), m.Groups[1].ToString(), m.Groups[2].ToString()))
                            .Where(m => !replaceables.Any(r => String.Equals(r.Replacer, m.Replacer, StringComparison.CurrentCultureIgnoreCase)))
                            .ToArray();

            currReplaceables.AddRange(additionalParts);
            // Look for Zero parts
            var zeroParts = Regex.Matches(sentenace, @"{([^-{}]+)-([^-{}]+)-([^-{}]+)}")
                            .Cast<Match>()
                            .Select(m => new PluralizerPart(m.ToString(), m.Groups[2].ToString(), m.Groups[3].ToString(), m.Groups[1].ToString()))
                            .Where(m => !replaceables.Any(r => String.Equals(r.Replacer, m.Replacer, StringComparison.CurrentCultureIgnoreCase)))
                            .ToArray();
            currReplaceables.AddRange(zeroParts);

            string regexText = String.Join("|", currReplaceables.Select(r => r.Replacer));

            sentenace = Regex.Replace(sentenace, regexText, m => matchEval(currReplaceables, m.ToString()));

            return String.Format(sentenace, currReplaceables.Select(r => r.GetText(count)).ToArray());
        }

        private static string matchEval(List<PluralizerPart> currReplaceables, string matchText)
        {
            var matchReplacerText = PluralizerPart.CleanUpReplacerText(matchText);
            var replacerList = currReplaceables.Select(r => r.Replacer).ToList();

            return String.Format("{{{0}}}", replacerList.IndexOf(matchReplacerText));
        }

        private class PluralizerPart
        {
            private string _single;
            private string _multi;
            private string _zero;

            public string Replacer { get; private set; }

            public PluralizerPart(string replacer, string single, string multi)
            {
                Replacer = PluralizerPart.CleanUpReplacerText(replacer);

                _single = single;
                _multi = multi;
            }

            public PluralizerPart(string replacer, string single, string multi, string zero) : this(replacer, single, multi)
            {
                _zero = zero;
            }

            public string GetText(int count)
            {
                if (!String.IsNullOrWhiteSpace(_zero) && count == 0)
                {
                    return enterCountValue(_zero, count);
                }

                if (count != 1)
                {
                    return enterCountValue(_multi, count);
                }

                return enterCountValue(_single, count);
            }

            private string enterCountValue(string formula, int count)
            {
                return Regex.Replace(formula, "#{1,2}", m => m.ToString() == "#" ? count.ToString() : "#");
            }

            public static string CleanUpReplacerText(string replacer)
            {
                // Cleans up replacer string to work with regex
                Regex regCleanup = new Regex(@"\.|\$|\^|\[|\(|\||\)|\*|\+|\?|\\");
                return regCleanup.Replace(replacer, ch => @"\" + ch.Value);
            }
        }

    }

}
