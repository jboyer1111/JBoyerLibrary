using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    public class MultiDeck : Deck
    {
        public MultiDeck(int numberOfDecks)
        {
            for (int i = 1; i < numberOfDecks; i++)
            {
                populateDeckLogic();
            }
        }

        public static MultiDeck GetUnShuffledMultiDeck(int numberOfDecks)
        {
            MultiDeck result = (MultiDeck)Deck.GetUnShuffledDeck();
            for (int i = 1; i < numberOfDecks; i++)
            {
                result._cards.AddRange(Deck.GetUnShuffledDeck().Cards);
            }
            return result;
        }
    }
}
