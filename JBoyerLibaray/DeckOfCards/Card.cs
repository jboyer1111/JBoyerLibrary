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

        #region Public Variables

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
                switch (_rank)
                {
                    case "Ace":
                        if (_aceIsHigh)
                        {
                            return 14;
                        }
                        return 1;
                    case "2":
                        return 2;
                    case "3":
                        return 3;
                    case "4":
                        return 4;
                    case "5":
                        return 5;
                    case "6":
                        return 6;
                    case "7":
                        return 7;
                    case "8":
                        return 8;
                    case "9":
                        return 9;
                    case "10":
                        return 10;
                    case "Jack":
                        return 11;
                    case "Queen":
                        return 12;
                    case "King":
                        return 13;
                    default:
                        return 0;
                }
            }
        }

        #endregion

        #region Constructors

        public Card(string suit, string rank)
        {
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
                return Equals((Card)obj);
            else
                return false;
        }

        public bool Equals(Card other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (other.GetType() != this.GetType())
                return false;
            Card rhs = other as Card;
            return this.Suit == rhs.Suit && this.Value == rhs.Value;
        }

        public override int GetHashCode()
        {
            return this.Suit.GetHashCode() ^ this.Value.GetHashCode();
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
            if (card == null)
                throw new ArgumentNullException();
            card = card.Trim();
            string[] cardInfo = Regex.Split(card, "of");
            if (cardInfo.Count() != 2)
                throw new Exception(string.Format("Unable to parse '{0}' into type of card", card));
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
