using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.Dice
{

    public class Die
    {

        #region Private Variables

        protected int _sides;
        protected Random _rand = new Random((int)DateTime.Now.Ticks);

        #endregion

        #region Public Properties

        [ExcludeFromCodeCoverage]
        public int Sides => _sides;

        #endregion

        #region Constructor

        public Die() : this(6) { }

        public Die(int sides)
        {
            _sides = sides;
        }
        
        #endregion

        #region Public Methods

        public int Roll() => _rand.Next(0, _sides) + 1;

        #endregion

    }

}
