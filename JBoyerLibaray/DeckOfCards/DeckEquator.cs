using System.Collections.Generic;

namespace JBoyerLibaray.DeckOfCards
{

    public static class DeckEquator
    {

        private static DifferentOrderEquator _diffOrder = new DifferentOrderEquator();

        public static DifferentOrderEquator DifferentOrder => _diffOrder;

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

}
