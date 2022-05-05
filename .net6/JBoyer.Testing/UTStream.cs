using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    public interface IUTDisposable : IDisposable
    {

        void UnitTestDispose();

    }

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
                using var streamReader = new StreamReader(this, leaveOpen: true);
                // Save current Position
                var pos = Position;

                // Goto beginning to read it all
                Seek(0, SeekOrigin.Begin);

                // Read the stream
                var result = streamReader.ReadToEnd();

                // Return stream to original Position
                Seek(pos, SeekOrigin.Begin);

                return result;
            }
        }

        #endregion

        #region Public Methods

        public void UnitTestDispose() => base.Dispose(true);

        #endregion

        #region Private Methods

        /// <summary>
        /// For Unit Testing purposes do no dispose this
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) => _disposeCallAttemptCount++;
        
        #endregion        

    }

}
