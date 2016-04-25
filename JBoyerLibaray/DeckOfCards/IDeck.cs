using System;
using System.Collections.Generic;

namespace JBoyerLibaray.DeckOfCards
{
    interface IDeck
    {
        bool AceIsHigh { get; set; }
        int CardCount { get; }
        IEnumerable<Card> Cards { get; }
        Card Draw();
        Card[] DrawCards(int amount);
        Card[] DrawUpToCards(int amount);
        void Shuffle();
        void Sort(IComparer<Card> comparer);
    }
}
