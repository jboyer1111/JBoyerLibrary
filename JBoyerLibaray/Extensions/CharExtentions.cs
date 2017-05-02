using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
