using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Exceptions
{
    [Serializable]
    public class ArgumentInvalidException : ArgumentException, ISerializable
    {
        #region Private Variables

        private bool _passedMessage = false;

        #endregion

        #region Public Properties

        public virtual object ActualValue
        {
            get;
        }

        public override string Message
        {
            get
            {
                string result = "";

                if (!_passedMessage)
                {
                    if (ActualValue == null)
                    {
                        result += "Specified argument was out of the range of valid values.";

                        if (ParamName != null)
                        {
                            result += Environment.NewLine + "Parameter name: " + ParamName;
                        }
                    }
                }
                else
                {
                    // Add passed message
                    result += base.Message;

                    if (ActualValue != null)
                    {
                        result += Environment.NewLine + "Actual value was " + ActualValue + ".";
                    }
                }

                return result;
            }
        }

        #endregion

        #region Constructor

        public ArgumentInvalidException() : base() { }

        public ArgumentInvalidException(string paramName) : base(null, paramName) { }

        public ArgumentInvalidException(string paramName, string message) : base(message, paramName)
        {
            _passedMessage = true;
        }

        public ArgumentInvalidException(string message, Exception innerException) : base(message, innerException)
        {
            _passedMessage = true;
        }

        public ArgumentInvalidException(string paramName, object actualValue, string message) : base(message, paramName)
        {
            ActualValue = actualValue;
            _passedMessage = true;
        }

        protected ArgumentInvalidException(SerializationInfo info, StreamingContext context) : base (info, context)
        {
            ActualValue = info.GetValue("ActualValue", typeof(Object));
            _passedMessage = info.GetBoolean("_passedMessage");
        }

        #endregion

        #region Public Methods

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ActualValue", ActualValue);
            info.AddValue("_passedMessage", _passedMessage);

            base.GetObjectData(info, context);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => info);
            }

            GetObjectData(info, context);
        }

        #endregion
    }
}       
        
        
        

        
        

        