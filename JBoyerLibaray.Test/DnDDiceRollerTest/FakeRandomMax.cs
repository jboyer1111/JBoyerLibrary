using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Test.DnDDiceRollerTest
{
    class FakeRandomMax : Random
    {
        public override int Next()
        {
            return int.MaxValue;
        }

        public override int Next(int maxValue)
        {
            return maxValue - 1;
        }

        public override int Next(int minValue, int maxValue)
        {
            return maxValue - 1;
        }

        public override double NextDouble()
        {
            return double.MaxValue;
        }

        protected override double Sample()
        {
            return 1.0;
        }

    }
}
