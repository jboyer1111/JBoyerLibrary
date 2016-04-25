using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JBoyerLibaray.PasswordGenerator
{
    public class PasswordGenerator
    {
        private static int _length = 8;
        private static int _nonAlphaNumbericChars = 1;
        private static int _uppercase = 1;
        private static int _numberChars = 1;

        private List<CharType> randSetup;
        private Random _rand;

        public PasswordGenerator() : this(_length, _nonAlphaNumbericChars, _uppercase, _numberChars) { }

        public PasswordGenerator(int length, int nonAlphaNumbericChar, int uppercase, int numberChars)
        {
            _rand = new Random();
            randSetup = new List<CharType>(length);

            if (length < nonAlphaNumbericChar + uppercase + numberChars)
            {
                throw new Exception("Length of password is not big enough");
            }

            while (randSetup.Count(c => c == CharType.NonAlphaNumericCharacters) < nonAlphaNumbericChar)
            {
                randSetup.Add(CharType.NonAlphaNumericCharacters);
            }

            while (randSetup.Count(c => c == CharType.UppercaseCharacters) < uppercase)
            {
                randSetup.Add(CharType.UppercaseCharacters);
            }

            while (randSetup.Count(c => c == CharType.NumbericCharacters) < numberChars)
            {
                randSetup.Add(CharType.NumbericCharacters);
            }

            while (randSetup.Count < length)
            {
                randSetup.Add(CharType.Characters);
            }
        }

        public string Generate()
        {
            shuffleChar();

            var chars = randSetup.Select(ct =>
            {
                int pos;
                char currChar;
                switch (ct)
                {
                    case CharType.NumbericCharacters:
                        pos = _rand.Next(0, CharacterLists.NumbericCharacters.Count());
                        currChar = CharacterLists.NumbericCharacters[pos];
                        break;
                    case CharType.UppercaseCharacters:
                        pos = _rand.Next(0, CharacterLists.Characters.Count());
                        currChar = CharacterLists.Characters[pos].ToString().ToUpper().ToCharArray().First();
                        break;
                    case CharType.NonAlphaNumericCharacters:
                        pos = _rand.Next(0, CharacterLists.NonAlphaNumericCharacters.Count());
                        currChar = CharacterLists.NonAlphaNumericCharacters[pos];
                        break;
                    default:
                        pos = _rand.Next(0, CharacterLists.Characters.Count());
                        currChar = CharacterLists.Characters[pos];
                        break;
                }
                return currChar;
            }).ToList();

            return String.Join(String.Empty, chars);
        }


        private void shuffleChar()
        {
            int pos = randSetup.Count;
            while (pos > 0)
            {
                int pickPos = _rand.Next(0, pos);
                pos--;

                var temp = randSetup[pos];
                randSetup[pos] = randSetup[pickPos];
                randSetup[pickPos] = temp;
            }
        }
    }
}
