using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JBoyerLibaray.DeckOfCards
{
    public class Card : ICard, IEquatable<Card>
    {
        #region Private Variables

        private string _suit;
        private string _rank;
        private bool _aceIsHigh = false;

        #endregion

        #region Public Properites

        public string Rank
        {
            get
            {
                return _rank;
            }
        }

        public string Suit
        {
            get
            {
                return _suit;
            }
        }

        public int Value
        {
            get
            {
                if (_rank == "Ace")
                {
                    if (_aceIsHigh)
                    {
                        return 14;
                    }
                    return 1;
                }
                else if (_rank == "King")
                {
                    return 13;
                }
                else if (_rank == "Queen")
                {
                    return 12;
                }
                else if (_rank == "Jack")
                {
                    return 11;
                }
                else
                {
                    int result = 0;
                    Int32.TryParse(_rank, out result);

                    // Higher values require face card Ranks
                    if (result > 10)
                    {
                        return 0;
                    }

                    return result;
                }
            }
        }

        #endregion

        #region Constructors

        public Card(string suit, string rank)
        {
            if (String.IsNullOrWhiteSpace(suit))
            {
                throw ExceptionHelper.CreateArgumentException(() => suit, "Cannot be null, empty, or whitespace.");
            }

            if (String.IsNullOrWhiteSpace(rank))
            {
                throw ExceptionHelper.CreateArgumentException(() => rank, "Cannot be null, empty, or whitespace.");
            }

            _suit = suit;
            _rank = rank;
        }

        #endregion

        #region Public Methods

        public void SetAceIsHighSetting(bool value)
        {
            _aceIsHigh = value;
        }

        #endregion

        #region Equals Logic

        public override bool Equals(object obj)
        {
            if (obj is Card)
            {
                return Equals((Card)obj);
            }

            return false;
        }

        public bool Equals(Card other)
        {
            // Null check
            if (other == null)
            {
                return false;
            }

            // Refrence check
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            // Type check. I my cards are only equal if they are same type
            // Sub types are not allowed to be equal to inheritied types
            if (other.GetType() != this.GetType())
            {
                return false;
            }
            
            // Check is suit and value are the same
            return this.Suit == other.Suit && this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            // http://stackoverflow.com/questions/1646807/quick-and-simple-hash-code-combinations

            int hash = 1009;
            int[] vals = new int[] { this.GetType().GetHashCode(), this.Suit.GetHashCode(), this.Value.GetHashCode() };

            foreach (var i in vals)
            {
                hash = (hash * 9176) + i;
            }

            return hash;
        }

        public static bool Equals(Card card1, Card card2)
        {
            return card1 == card2;
        }

        public static bool operator ==(Card card1, Card card2)
        {
            return object.Equals(card1, card2);
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return !object.Equals(card1, card2);
        }

        #endregion

        #region Static

        public static Card Parse(string card)
        {
            if (String.IsNullOrWhiteSpace(card))
            {
                throw ExceptionHelper.CreateArgumentException(() => card, "Cannot be null, empty, or whitespace");
            }

            // Clean up extra whitespace
            card = card.Trim();

            // Require card text to be suit of rank
            string[] cardInfo = Regex.Split(card, " of ");

            // Make sure string was split correctly
            if (cardInfo.Count() != 2)
            {
                throw ExceptionHelper.CreateArgumentException(() => card, String.Format("Unable to parse '{0}' into type of card", card));
            }
            
            return new Card(cardInfo[1].Trim(), cardInfo[0].Trim());
        }

        public static bool TryParse(string cardStr, out Card card)
        {
            try
            {
                card = Card.Parse(cardStr);
            }
            catch
            {
                card = null;
                return false;
            }

            return true;
        }

        #endregion

        public override string ToString()
        {
            return String.Format("{0} of {1}", Rank, Suit);
        }

    }
}
