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
            var xSorted = Deck.Copy(x);
            var ySorted = Deck.Copy(y);
            xSorted.Sort(CardComparer.SuitThenValue);
            ySorted.Sort(CardComparer.SuitThenValue);

            return xSorted == ySorted;
        }

        public int GetHashCode(Deck obj)
        {
            var objSorted = Deck.Copy(obj);

            objSorted.Sort(CardComparer.SuitThenValue);

            return objSorted.GetHashCode();
        }
    }
}
