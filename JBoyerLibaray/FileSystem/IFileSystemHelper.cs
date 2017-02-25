using System;
using System.Collections.Generic;
using System.IO;

namespace JBoyerLibaray.FileSystem
{
    public interface IFileSystemHelper
    {
        IDirectoryWrapper Directory { get; }
        
        IFileWrapper File { get; }
    }
}
