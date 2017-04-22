using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JBoyerLibaray.DeckOfCards
{

    public class NotEnoughCardsException : Exception, ISerializable
    {
        public NotEnoughCardsException() { }

        public NotEnoughCardsException(string message)
            : base(message)
        {
        }

        public NotEnoughCardsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // This constructor is needed for serialization.
        protected NotEnoughCardsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
