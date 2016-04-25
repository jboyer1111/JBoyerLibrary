using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.DeckOfCards
{
    public class CustomExpections : Exception
    {
        public CustomExpections() { }

        public CustomExpections(string message)
            : base(message)
        {
        }

        public CustomExpections(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class EmptyDeckException : Exception
    {
        public EmptyDeckException() { }

        public EmptyDeckException(string message)
            : base(message)
        {
        }

        public EmptyDeckException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class EmptyHandException : Exception
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
    }

    public class NotEnoughCardsException : Exception
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
    }
}
