using System;

namespace JBoyerLibaray.DeckOfCards
{
    interface ICard
    {
        string Rank { get; }
        void SetAceIsHighSetting(bool value);
        string Suit { get; }
        int Value { get; }
    }
}
