using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    public class CardValueThenSuitComparer : Comparer<Card>
    {
        internal CardValueThenSuitComparer() { }

        public override int Compare(Card x, Card y)
        {
            if (x == null & y == null)
            {
                return 0;
            }
                
            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }
                
            int valueResult = x.Value.CompareTo(y.Value);
            if (valueResult != 0)
            {
                return valueResult;
            }
                
            return String.Compare(x.Suit, y.Suit, StringComparison.CurrentCulture);
        }
    }
}
