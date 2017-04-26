﻿using JBoyerLibaray.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBoyerLibaray.DeckOfCards
{
    public class Deck : IDeck, IEquatable<Deck>, IEnumerable<Card>
    {
        #region Private Variables

        protected bool _aceIsHigh = false;
        protected List<Card> _cards;
        protected Random _rand;

        #endregion

        #region Public Variables

        public bool AceIsHigh
        {
            get
            {
                return _aceIsHigh;
            }
            set
            {
                if (_aceIsHigh == value)
                {
                    return;
                }
                foreach (Card card in _cards)
                {
                    card.SetAceIsHighSetting(value);
                }
                _aceIsHigh = value;
            }
        }

        public int CardCount
        {
            get
            {
                return _cards.Count;
            }
        }

        public IEnumerable<Card> Cards
        {
            get
            {
                return _cards.AsEnumerable();
            }
        }

        #endregion

        #region Constructors

        public Deck() : this(DeckOptions.Default) { }

        private Deck(Deck deck)
            : this(DeckOptions.Empty)
        {
            _cards.AddRange(deck.Cards);
        }

        private Deck(params DeckOptions[] options)
        {
            // Setup variagbles
            _cards = new List<Card>(52);
            _rand = new Random();

            if (!options.Contains(DeckOptions.Empty))
            {
                populateDeckLogic();
            }

            if (!options.Contains(DeckOptions.UnShuffled))
            {
                Shuffle();
            }
        }

        #endregion

        #region Private Methods

        protected void populateDeckLogic()
        {
            string[] Suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
            string[] Ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

            foreach (string suit in Suits)
            {
                foreach (string rank in Ranks)
                {
                    Card card = new Card(suit, rank);
                    _cards.Add(card);
                }
            }
        }

        #endregion

        #region Public Methods

        public Card Draw()
        {
            if (CardCount == 0)
            {
                throw new EmptyDeckException("Your deck is empty you cannot draw from an empty deck");
            }

            // Get the card and remove it from list
            Card card = _cards[0];
            _cards.RemoveAt(0);

            return card;
        }

        public Card[] DrawCards(int amount)
        {
            if (amount < 1)
            {
                throw ExceptionHelper.CreateArgumentException(() => amount, "You must draw at least one card");
            }

            if (amount > CardCount)
            {
                throw new NotEnoughCardsException(String.Format("You do not have enough cards in the deck to draw {0} cards", amount));
            }

            Card[] drawnCards = new Card[amount];
            for (int i = 0; i < amount; i++)
            {
                drawnCards[i] = Draw();
            }

            return drawnCards;
        }

        public Card[] DrawUpToCards(int amount)
        {
            if (amount < 1)
            {
                throw ExceptionHelper.CreateArgumentException(() => amount, "You must draw at least one card");
            }

            List<Card> drawnCards = new List<Card>();
            for (int i = 0; i < amount; i++)
            {
                drawnCards.Add(Draw());
                if (CardCount == 0)
                {
                    break;
                }
            }
            return drawnCards.ToArray();
        }

        public void Shuffle()
        {
            int pos = CardCount;
            while (pos > 0)
            {
                int pickPos = _rand.Next(0, pos);
                pos--;

                Card temp = _cards[pos];
                _cards[pos] = _cards[pickPos];
                _cards[pickPos] = temp;
            }
        }

        public void Sort(IComparer<Card> comparer)
        {
            _cards.Sort(comparer);
        }

        #endregion

        #region Equals Logic/Override Equals

        public override bool Equals(object obj)
        {
            if (obj is Deck)
            {
                return Equals((Deck)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            // http://stackoverflow.com/questions/1646807/quick-and-simple-hash-code-combinations

            int hash = 1009;
            foreach (var card in _cards)
            {
                hash = (hash * 9176) + card.GetHashCode();
            }
            
            return hash;
        }

        public bool Equals(Deck deck2)
        {
            if (deck2 == null)
            {
                return false;
            }

            if (CardCount != deck2.CardCount)
            {
                return false;
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                if (!_cards[i].Equals(deck2.Cards.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Equals(Deck deck1, Deck deck2)
        {
            return deck1 == deck2;
        }

        public static bool Equals(Deck deck1, Deck deck2, IEqualityComparer<Deck> comparer)
        {
            return comparer.Equals(deck1, deck2);
        }

        public static bool operator ==(Deck deck1, Deck deck2)
        {
            return object.Equals(deck1, deck2);
        }

        public static bool operator !=(Deck deck1, Deck deck2)
        {
            return !object.Equals(deck1, deck2);
        }

        #endregion

        #region Enumerable Implamentaion

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Static Methods

        public static Deck Copy(Deck deck)
        {
            return new Deck(deck);
        }

        public static Deck GetUnShuffledDeck()
        {
            return new Deck(DeckOptions.UnShuffled);
        }

        public static Deck Parse(string cards)
        {
            if (cards == null)
                throw new ArgumentNullException();
            cards = cards.Trim();
            Deck result = new Deck(DeckOptions.Empty);
            List<string> stringCardList = new List<string>(cards.Split(','));
            foreach (var card in stringCardList)
            {
                result._cards.Add(Card.Parse(card));
            }
            return result;
        }

        public static bool TryParse(string cards, out Deck deck)
        {
            try
            {
                deck = Deck.Parse(cards);
            }
            catch
            {
                deck = null;
                return false;
            }

            return true;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < CardCount; i++)
            {
                sb.AppendFormat("{0}", _cards[i]);
                if (i < CardCount - 1)
                    sb.Append(", ");
            }

            return sb.ToString();
        }

    }
}
