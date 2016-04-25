using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Dice
{
    public class Die
    {
        protected int _sides;
        protected Random _rand;
        
        public Die() : this(6) { }

        public Die(int sides)
        {
            _sides = sides;
            _rand = new Random((int)DateTime.Now.Ticks);
        }

        public int Roll()
        {
            return _rand.Next(0, _sides) + 1;
        }
    }
}
