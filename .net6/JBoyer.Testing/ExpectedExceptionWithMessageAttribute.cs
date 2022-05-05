using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    public class ExpectedExceptionWithMessageAttribute : ExpectedExceptionBaseAttribute
    {

        #region Private Variables

        private readonly Type? _exceptionType = null;
        private readonly string? _expectedMessage = null;

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

            Type type = e.GetType();
            if (type != _exceptionType)
            {
                throw new AssertFailedException($"ExpectedExceptionWithMessageAttribute failed. Expected exception type: {_exceptionType.FullName}. Actual exception type: {type.FullName}. Exception message: {e.Message}");
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
