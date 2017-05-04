using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.FileSystem
{
    [ExcludeFromCodeCoverage]
    public class FileSystemHelper : IFileSystemHelper
    {
        private IDirectoryWrapper _directoryWrapper;
        private IFileWrapper _fileWrapper;

        public IDirectoryWrapper Directory
        {
            get
            {
                return _directoryWrapper;
            }
        }

        public IFileWrapper File
        {
            get
            {
                return _fileWrapper;
            }
        }


        public FileSystemHelper()
        {
            _directoryWrapper = new DirectoryWrapper();
            _fileWrapper = new FileWrapper();
        }

    }
}
