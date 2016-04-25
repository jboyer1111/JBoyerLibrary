using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JBoyerLibaray.Exceptions
{
    public class CouldNotConvertToNumber : Exception, ISerializable
    {
        public CouldNotConvertToNumber() : base()
        {

        }
        public CouldNotConvertToNumber(string message) : base (message)
        {

        }
        public CouldNotConvertToNumber(string message, Exception inner) : base (message, inner)
        {
            
        }

        // This constructor is needed for serialization.
        protected CouldNotConvertToNumber(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}