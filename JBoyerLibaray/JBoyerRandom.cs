using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public class JBoyerRandom : Random
    {
        #region Private Variables

        private static JBoyerRandomMode _randMode;
        private RandomNumberGenerator _rand = RandomNumberGenerator.Create();

        #endregion

        #region Constructor

        public JBoyerRandom()
        {
        }

        public JBoyerRandom(int Seed) : base(Seed)
        {
        }

        #endregion

        #region Public Methods

        public static void SetRandomMode(JBoyerRandomMode randMode)
        {
            if (!Enum.IsDefined(typeof(JBoyerRandomMode), randMode))
            {
                _randMode = JBoyerRandomMode.SystemRandom;
                return;
            }
            
            _randMode = randMode;
        }
        
        public override int Next()
        {
            if (_randMode == JBoyerRandomMode.CryptographyRandomNumberGenerator)
            {
                byte[] bytes = new byte[4];
                _rand.GetBytes(bytes);

                return (int)BitConverter.ToUInt32(bytes, 0);
            }
            
            return base.Next();
        }

        public override int Next(int maxValue)
        {
            if (_randMode == JBoyerRandomMode.CryptographyRandomNumberGenerator)
            {
                return Next() % maxValue;
            }
            
            return base.Next(maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            if (_randMode == JBoyerRandomMode.CryptographyRandomNumberGenerator)
            {
                return Next(maxValue - minValue) + minValue;
            }

            return base.Next(minValue, maxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            base.NextBytes(buffer);
        }

        public override double NextDouble()
        {
            if (_randMode == JBoyerRandomMode.CryptographyRandomNumberGenerator)
            {
                return Convert.ToDouble(Next());
            }

            return base.NextDouble();
        }

        #endregion

    }
}
