using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{

    [ExcludeFromCodeCoverage]
    public class ExpectedExceptionWithMessageAttribute : ExpectedExceptionBaseAttribute
    {

        #region Private Variables

        private Type _exceptionType = null;
        private string _expectedMessage = null;

        #endregion

        #region Constructor

        /// <summary>
        /// This attribute tests the type and the message of the thrown error
        /// </summary>
        /// <param name="exceptionType">An expected type of exception to be thrown by a method.</param>
        /// <param name="expectedMessage">The expected message of the thrown error.</param>
        public ExpectedExceptionWithMessageAttribute(Type exceptionType, string expectedMessage)
        {
            _exceptionType = exceptionType;
            _expectedMessage = expectedMessage;
        }

        #endregion

        #region Public Methods

        public void UTVerify(Exception e)
        {
            verifyLogic(e);
        }

        #endregion

        #region Private Methods

        [ExcludeFromCodeCoverage]
        protected override void Verify(Exception e)
        {
            verifyLogic(e);
        }
        
        private void verifyLogic(Exception e)
        {
            if (_exceptionType == null)
            {
                throw new AssertInconclusiveException("Exception Type was not set.");
            }
            
            if (e.GetType() != _exceptionType)
            {
                string message = String.Format(
                    "ExpectedExceptionWithMessageAttribute failed. Expected exception type: {0}. Actual exception type: {1}. Exception message: {2}",
                    _exceptionType.FullName,
                    e.GetType().FullName,
                    e.Message
                );

                throw new AssertFailedException(message);
            }

            var actualMessage = e.Message.Trim();
            if (_expectedMessage != null)
            {
                Assert.AreEqual(_expectedMessage, actualMessage);
            }
        }

        #endregion

    }

}
