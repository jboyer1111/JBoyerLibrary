using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public static class PluralizeString
    {
        private static PluralizerPart[] replaceables = new PluralizerPart[] {
            new PluralizerPart("{A}", "a ", ""),
            new PluralizerPart("{A-#}", "a", "{#}"),
            new PluralizerPart("{AN}", "an ", ""),
            new PluralizerPart("{AN-#}", "an", "{#}"),
            new PluralizerPart("{IS-ARE}", "is", "are"),
            new PluralizerPart("{S}", "", "s"),
            new PluralizerPart("{ES}", "", "es"),
            new PluralizerPart("{Y-IES}", "y", "ies"),
            new PluralizerPart("{F-VES}", "f", "ves")
        };

        public static string Pluralize(string sentenace, int count)
        {
            var currReplaceables = new List<PluralizerPart>(replaceables);
            var additionalParts = Regex.Matches(sentenace, @"{(\S+)-(\S+)}")
                            .Cast<Match>()
                            .Select(m => new PluralizerPart(m.ToString(), m.Groups[1].ToString(), m.Groups[2].ToString()))
                            .Where(m => !replaceables.Any(r => String.Equals(r.Replacer, m.Replacer, StringComparison.CurrentCultureIgnoreCase)))
                            .ToArray();
            
            currReplaceables.AddRange(additionalParts);

            string regexText = String.Join("|", currReplaceables.Select(r => r.Replacer));
            
            sentenace = Regex.Replace(sentenace, regexText, m => matchEval(currReplaceables, m.ToString()));

            return String.Format(sentenace, currReplaceables.Select(r => r.GetText(count)).ToArray());
        }
        
        private static string matchEval(List<PluralizerPart> currReplaceables, string matchText)
        {
            return String.Format("{{{0}}}", currReplaceables.Select(r => r.Replacer).ToList().IndexOf(matchText));
        }

        private class PluralizerPart
        {
            private string _single;
            private string _multi;

            public string Replacer { get; private set; }

            public PluralizerPart(string replacer, string single, string multi)
            {
                Replacer = replacer;
                _single = single;
                _multi = multi;
            }

            public string GetText(int count)
            {
                if (count != 1)
                {
                    return _multi.Replace("{#}", count.ToString());
                }

                return _single;
            }
        }
    }
}
