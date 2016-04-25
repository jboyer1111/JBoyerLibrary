using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.DeckOfCards
{
    public class DifferentOrderEquator : IEqualityComparer<Deck>
    {

        public bool Equals(Deck x, Deck y)
        {
            var a = Deck.Copy(x);
            var b = Deck.Copy(y);
            a.Sort(CardComparer.SuitThenValue);
            b.Sort(CardComparer.SuitThenValue);

            return a == b;
        }

        public int GetHashCode(Deck obj)
        {
            var a = Deck.Copy(obj);
            a.Sort(CardComparer.SuitThenValue);

            return a.GetHashCode();
        }
    }
}
