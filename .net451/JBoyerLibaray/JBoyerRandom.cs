﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    /// <summary>
    /// This class is a wrapper for the System.Random Class where it can
    /// change between using System.Random or System.Security.Cryptography.RandomNumberGenerator class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class JBoyerRandom : Random, IDisposable
    {

        #region Private Variables

        private JBoyerRandomMode _randMode;
        private RandomNumberGenerator _rand = RandomNumberGenerator.Create();

        #endregion

        #region Constructor

        public JBoyerRandom() : this(JBoyerRandomMode.CryptographyRandomNumberGenerator) { }

        public JBoyerRandom(int seed) : this(JBoyerRandomMode.CryptographyRandomNumberGenerator, seed) { }

        public JBoyerRandom(JBoyerRandomMode mode) : base() => clampMode(mode);

        public JBoyerRandom(JBoyerRandomMode mode, int seed) : base(seed) => clampMode(mode);

        private void clampMode(JBoyerRandomMode mode)
        {
            if (!Enum.IsDefined(typeof(JBoyerRandomMode), mode))
            {
                _randMode = JBoyerRandomMode.CryptographyRandomNumberGenerator;
            }

            _randMode = mode;
        }

        #endregion

        #region Public Methods

        public override int Next()
        {
            byte[] bytes = new byte[4];

            // Use NextBytes to generate the Next Random Number
            NextBytes(bytes);

            return Math.Abs((int)BitConverter.ToUInt32(bytes, 0));
        }

        public override int Next(int maxValue)
        {
            // Use Next get get random number and then do remainder logic to apply the max value.
            // Now you have a random number between 0 and maxvalue
            return Next() % maxValue;
        }

        public override int Next(int minValue, int maxValue)
        {
            // Find the range between min and max and get a random number for that range.
            // Then add that to min value. Now you have a random number between the two values
            return Next(maxValue - minValue) + minValue;
        }

        public override void NextBytes(byte[] buffer)
        {
            // If the static randmode is Cryptography, then use the Cryptography instance otherwise just use the base class code
            if (_randMode == JBoyerRandomMode.CryptographyRandomNumberGenerator)
            {
                _rand.GetBytes(buffer);
            }
            else
            {
                base.NextBytes(buffer);
            }
        }

        public override double NextDouble()
        {
            return Convert.ToDouble(Next());
        }

        public void Dispose()
        {
            _rand.Dispose();
        }

        #endregion

    }

}
