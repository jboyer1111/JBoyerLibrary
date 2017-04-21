using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JBoyerLibaray.DeckOfCards
{
    [Serializable]
    public class EmptyHandException : Exception, ISerializable
    {
        public EmptyHandException() { }

        public EmptyHandException(string message)
            : base(message)
        {
        }

        public EmptyHandException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // This constructor is needed for serialization.
        protected EmptyHandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
