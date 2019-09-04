using JBoyerLibaray.Exceptions;
using System;
using System.Linq;

namespace JBoyerLibaray.UnitTests
{
    public class UTRandom : Random
    {
        #region Pirvate Region

        private int _index = 0;
        private int[] _numbers;

        #endregion

        #region Constructor

        public UTRandom(params int[] numbers)
        {
            if (numbers == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => numbers);
            }

            if (numbers.Length < 1)
            {
                throw new ArgumentException("You need to have at least one number.", nameof(numbers));
            }

            _numbers = numbers;
        }

        #endregion

        #region Public Methods

        public override int Next()
        {
            if (_index > _numbers.Length - 1)
            {
                _index = 0;
            }

            return _numbers[_index++];
        }

        public override int Next(int maxValue)
        {
            if (!_numbers.Any(n => n < maxValue))
            {
                throw ExceptionHelper.CreateArgumentInvalidException(() => maxValue, "List of numbers do not have value less than the max value.", maxValue);
            }

            int result;
            do
            {
                result = Next();
            } while (result >= maxValue);

            return result;
        }

        public override int Next(int minValue, int maxValue)
        {
            if (!_numbers.Any(n => n < maxValue && n >= minValue))
            {
                throw new ArgumentInvalidException(null, "List of numbers do not have a valid value with passed parameters.");
            }

            int result;
            do
            {
                result = Next();
            } while (result < minValue || result >= maxValue);

            return result;
        }

        public override void NextBytes(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)Next();
            }
        }

        public override double NextDouble()
        {
            return Next();
        }

        #endregion
    }
}
