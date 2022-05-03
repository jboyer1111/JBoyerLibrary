using JBoyerLibaray.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace JBoyerLibaray.Web
{

    public class WebDownloaderFile : IDisposable
    {

        #region Private Variables

        private Stream _file;
        private string _contentType;

        #endregion

        #region Public Properties

        [ExcludeFromCodeCoverage]
        public Stream File => _file;

        [ExcludeFromCodeCoverage]
        public string ContentType => _contentType;

        #endregion

        public WebDownloaderFile(string contentType, Stream stream)
        {
            if (String.IsNullOrWhiteSpace(contentType))
            {
                throw ExceptionHelper.CreateArgumentException(() => contentType, "Cannot be null, empty, or whitespace.");
            }

            if (stream == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => stream);
            }
            
            _contentType = contentType;
            _file = stream;
        }

        public void Dispose()
        {
            _file.Dispose();
        }

    }

}
