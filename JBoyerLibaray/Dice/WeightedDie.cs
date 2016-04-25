using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Dice
{
    public class WeightedDie : Die
    {
        private List<int> _numbers;
        
        public WeightedDie() : this(6) { }

        public WeightedDie(int sides) : this(sides, i => 1) { }

        public WeightedDie(int sides, Func<int, int> weightFunc)
            : base(sides)
        {
            int total = 0;
            for (var i = 1; i <= sides; i++)
            {
                total += weightFunc(i);
            }

            _numbers = new List<int>(total);

            for (var i = 1; i <= sides; i++)
            {
                int currentWeight = weightFunc(i);
                for (var j = 0; j < currentWeight; j++)
                {
                    _numbers.Add(i);
                }
            }
        }

        public new int Roll()
        {
            return _numbers[_rand.Next(0, _numbers.Count)];
        }
    }
}
