using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    public static class DeckEquator
    {
        private static DifferentOrderEquator _diffOrder = new DifferentOrderEquator();

        public static DifferentOrderEquator DifferentOrder { get { return _diffOrder; } }
    }
}
