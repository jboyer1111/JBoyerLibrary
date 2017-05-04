using JBoyerLibaray.Exceptions;
using JBoyerLibaray.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JBoyerLibaray.PasswordGenerator
{
    public class PasswordGenerator
    {
        #region Private Variables

        private const int LENGTH = 8;
        private const int NONALPHANUMERICCHARS = 1;
        private const int UPPERCASE = 1;
        private const int NUMBERCHARS = 1;

        private List<CharType> _randSetup;
        private Random _rand;

        #endregion

        #region Constructor

        public PasswordGenerator() : this(LENGTH, NONALPHANUMERICCHARS, UPPERCASE, NUMBERCHARS) { }

        public PasswordGenerator(int length, int nonAlphaNumericChar, int uppercase, int numberChars)
        {
            _rand = new Random();
            _randSetup = new List<CharType>(length);

            if (length < nonAlphaNumericChar + uppercase + numberChars)
            {
                string message = String.Format(
                    "Length of password is not long enough to support your other arguments.{0}" +
                    "Non-AlphaNumeric: {1}{0}" +
                    "Uppercase: {2}{0}" +
                    "Number: {3}",
                    Environment.NewLine,
                    nonAlphaNumericChar,
                    uppercase,
                    numberChars                    
                );

                throw ExceptionHelper.CreateArgumentInvalidException(() => length, message, length);
            }

            while (_randSetup.Count(c => c == CharType.NonAlphaNumericCharacters) < nonAlphaNumericChar)
            {
                _randSetup.Add(CharType.NonAlphaNumericCharacters);
            }

            while (_randSetup.Count(c => c == CharType.UppercaseCharacters) < uppercase)
            {
                _randSetup.Add(CharType.UppercaseCharacters);
            }

            while (_randSetup.Count(c => c == CharType.NumbericCharacters) < numberChars)
            {
                _randSetup.Add(CharType.NumbericCharacters);
            }

            while (_randSetup.Count < length)
            {
                _randSetup.Add(CharType.Characters);
            }
        }

        #endregion

        #region Public Methods

        public string Generate()
        {
            _randSetup.Shuffle();

            var chars = _randSetup.Select(ct =>
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
                        currChar = CharacterLists.Characters[pos].ToUpper();
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

        #endregion
    }
}
