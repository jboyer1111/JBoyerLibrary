using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class UnitTestableRandom : Random
    {
        #region Pirvate Region

        private int _index = 0;
        private int[] _numbers;
        //private int _byteIndex = 0;
        private byte[] _bytes;

        #endregion

        #region Constructor

        public UnitTestableRandom(params int[] numbers) : this(new byte[] { }, numbers)
        {

        }

        public UnitTestableRandom(byte[] bytes) : this(bytes, new int[] { })
        {

        }

        private UnitTestableRandom(byte[] bytes, int[] numbers)
        {
            _bytes = bytes;
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
            } while (result > maxValue);

            return result;
        }

        public override int Next(int minValue, int maxValue)
        {
            int result;
            do
            {
                result = Next();
            } while (result > maxValue || result < minValue);

            return result;
        }

        public override void NextBytes(byte[] buffer)
        {
            
        }

        public override double NextDouble()
        {
            return Next();
        }

        #endregion

        #region

        protected override double Sample()
        {
            return base.Sample();
        }

        #endregion
    }
}
