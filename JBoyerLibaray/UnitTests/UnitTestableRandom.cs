using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{
    public class UnitTestableRandom : Random
    {
        #region Pirvate Region

        private int _index = 0;
        private int[] _numbers;

        #endregion

        #region Constructor

        public UnitTestableRandom(params int[] numbers)
        {
            if (numbers == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => numbers);
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
            int result;
            do
            {
                result = Next();
            } while (result >= maxValue);

            return result;
        }

        public override int Next(int minValue, int maxValue)
        {
            int result;
            do
            {
                result = Next(maxValue);
            } while (result < minValue);

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
