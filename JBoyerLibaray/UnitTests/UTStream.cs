using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace JBoyerLibaray.UnitTests
{

    [ExcludeFromCodeCoverage]
    public class UTStream : MemoryStream, IUTDisposable
    {

        #region Private Variables

        private int _disposeCallAttemptCount = 0;

        #endregion

        #region Public Properties

        public bool AttemptedToDispose => _disposeCallAttemptCount > 0;

        public int AttemptedToDisposeCount => _disposeCallAttemptCount;

        public string ReadStreamAsText
        {
            get
            {
                using (var streamReader = new StreamReader(this))
                {
                    // Save current Position
                    var pos = Position;

                    // Goto beginning to read it all
                    this.Seek(0, SeekOrigin.Begin);
                    
                    // Read the stream
                    var result = streamReader.ReadToEnd();

                    // Return stream to original Position
                    this.Seek(pos, SeekOrigin.Begin);

                    return result;
                }
            }
        }

        #endregion

        #region Public Methods

        public void UnitTestDispose()
        {
            base.Dispose(true);
        }

        #endregion

        #region Private Methods

        protected override void Dispose(bool disposing)
        {
            // For Unit Testing purposes do no dispose this
            _disposeCallAttemptCount++;
        }

        #endregion        

    }

}
