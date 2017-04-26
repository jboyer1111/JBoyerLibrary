using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Dice
{
    public class Die
    {
        #region Privte Variabls

        protected int _sides;
        protected Random _rand;

        #endregion

        #region Constructor

        public Die() : this(6) { }

        public Die(int sides)
        {
            _sides = sides;
            _rand = new Random((int)DateTime.Now.Ticks);
        }

        #endregion

        #region Public Methods

        public int Roll()
        {
            return _rand.Next(0, _sides) + 1;
        }

        #endregion
    }
}
