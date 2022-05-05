using System;
using System.Runtime.Serialization;

namespace JBoyerLibaray.DeckOfCards
{

    [Serializable]
    public class EmptyDeckException : Exception, ISerializable
    {

        public EmptyDeckException() { }

        public EmptyDeckException(string message) : base(message) { }

        public EmptyDeckException(string message, Exception inner) : base(message, inner) { }

        // This constructor is needed for serialization.
        protected EmptyDeckException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
