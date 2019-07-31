using System;
using System.Linq;

namespace JBoyerLibaray.Extensions
{
    public static class CharExtentions
    {
        public static char ToUpper(this char character)
        {
            return character.ToString().ToUpper().First();
        }

        public static char ToUpperInvariant(this char character)
        {
            return character.ToString().ToUpperInvariant().First();
        }

        public static char ToLower(this char character)
        {
            return character.ToString().ToLower().First();
        }

        public static char ToLowerInvariant(this char character)
        {
            return character.ToString().ToLowerInvariant().First();
        }
    }
}
