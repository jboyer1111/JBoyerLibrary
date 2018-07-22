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

        #region Private Variables

        private static FileSystemHelper _fileSystemHelper = new FileSystemHelper();

        private IDirectoryWrapper _directoryWrapper;
        private IFileWrapper _fileWrapper;

        #endregion

        #region Public Properties

        public static FileSystemHelper Default
        {
            get
            {
                return _fileSystemHelper;
            }
        }

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

        #endregion

        #region Constructor

        public FileSystemHelper()
        {
            _directoryWrapper = new DirectoryWrapper();
            _fileWrapper = new FileWrapper();
        }

        #endregion

    }
}
