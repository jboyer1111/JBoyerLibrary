using System;
using System.Collections.Generic;

namespace JBoyerLibaray.DeckOfCards
{

    public static class CardComparer
    {

        #region Private Variables

        private static CardSuitThenValueComparer _suitThenValue = new CardSuitThenValueComparer();
        private static CardValueThenSuitComparer _valueThenSuit = new CardValueThenSuitComparer();

        #endregion

        #region Public Properties

        public static CardSuitThenValueComparer SuitThenValue => _suitThenValue;

        public static CardValueThenSuitComparer ValueThenSuit => _valueThenSuit;

        #endregion

        public class CardSuitThenValueComparer : IComparer<Card>
        {
            internal CardSuitThenValueComparer() { }

            public int Compare(Card x, Card y)
            {
                if (x == null && y == null)
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

                int suitResult = String.Compare(x.Suit, y.Suit, StringComparison.CurrentCulture);
                if (suitResult != 0)
                {
                    return suitResult;
                }

                return x.Value.CompareTo(y.Value);
            }
        }

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

}
