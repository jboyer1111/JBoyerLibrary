using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.PasswordGenerator
{
    [ExcludeFromCodeCoverage]
    public static class CharacterLists
    {
        private static char[] _chars = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
        private static char[] _numChars = "1234567890".ToCharArray();
        private static char[] _nonAlphaNumberic = "!@#$%^&*_+-={}[]\\|;':\"<>,./?".ToCharArray();

        public static char[] Characters
        {
            get
            {
                return _chars;
            }
        }

        public static char[] NumbericCharacters
        {
            get
            {
                return _numChars;
            }
        }

        public static char[] NonAlphaNumericCharacters
        {
            get
            {
                return _nonAlphaNumberic;
            }
        }
    }
}
