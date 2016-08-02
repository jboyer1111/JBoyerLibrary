using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    public static class CardComparer
    {
        private static CardSuitThenValueComparer _suitThenValue = new CardSuitThenValueComparer();

        public static CardSuitThenValueComparer SuitThenValue { get { return _suitThenValue; } }

        private static CardValueThenSuitComparer _valueThenSuit = new CardValueThenSuitComparer();

        public static CardValueThenSuitComparer ValueThenSuit { get { return _valueThenSuit; } }

    }
}
