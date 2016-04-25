using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    public class CardSuitThenValueComparer : Comparer<Card>
    {
        internal CardSuitThenValueComparer() { }

        public override int Compare(Card x, Card y)
        {
            if (x == null & y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;
            int suitResult = String.Compare(x.Suit, y.Suit, StringComparison.CurrentCulture);
            if (suitResult != 0)
                return suitResult;
            return x.Value.CompareTo(y.Value);
        }
    }
}
