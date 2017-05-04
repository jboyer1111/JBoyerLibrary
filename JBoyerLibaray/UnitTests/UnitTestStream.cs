using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class UnitTestStream : MemoryStream, IUnitTestDisposable
    {
        #region Private Varables

        private int _disposeCallAttemptCount = 0;

        #endregion

        #region Public Properties

        public bool AttemptedToDispose
        {
            get
            {
                return _disposeCallAttemptCount > 0;
            }
        }

        public int AttemptedToDisposeCount
        {
            get
            {
                return _disposeCallAttemptCount;
            }
        }

        public string ReadStreamAsText
        {
            get
            {
                using (var streamReader = new StreamReader(this))
                {
                    // Save current Position
                    var pos = Position;

                    // Goto begining to read it all
                    this.Seek(0, SeekOrigin.Begin);
                    
                    // Read the stream
                    var result = streamReader.ReadToEnd();

                    // Return stream to orignal pos
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
            // For Unit Tesing purposes do no dispose this
            _disposeCallAttemptCount++;
        }

        #endregion        
    }
}
