using JBoyerLibaray.Exceptions;
using JBoyerLibaray.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.FileSystem
{
    public class FileHelper
    {
        private IFileSystemHelper _fileSystemHelper;

        public FileHelper() : this(new FileSystemHelper()) { }

        public FileHelper(IFileSystemHelper fileSystemHelper)
        {
            if (fileSystemHelper == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => fileSystemHelper);
            }

            _fileSystemHelper = fileSystemHelper;
        }

        public bool IsTiff(string filePath)
        {
            using (var fileStream = _fileSystemHelper.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return IsTiff(fileStream);
            }
        }

        public bool IsTiff(Stream stream)
        {
            const byte kIntelMark = 0x49;
            const byte kMotorolaMark = 0x4d;
            const ushort kTiffMagicNumber = 42;
            byte[] marks = new byte[] { kIntelMark, kMotorolaMark };

            stream.Seek(0);
            if (stream.Length < 8)
            {
                return false;
            }

            byte[] header = new byte[2];
            stream.Read(header, 0, header.Length);

            if (header[0] != header[1] || !marks.Contains(header[0]))
            {
                return false;
            }

            bool isIntel = header[0] == kIntelMark;
            ushort magicNumber = ReadShort(stream, isIntel);

            if (magicNumber != kTiffMagicNumber)
            {
                return false;
            }
                
            return true;
        }

        private ushort ReadShort(Stream stream, bool isIntel)
        {
            byte[] b = new byte[2];
            stream.Read(b, 0, b.Length);
            return ToShort(isIntel, b[0], b[1]);
        }

        private static ushort ToShort(bool isIntel, byte b0, byte b1)
        {
            if (isIntel)
            {
                return (ushort)(((int)b1 << 8) | (int)b0);
            }
            else
            {
                return (ushort)(((int)b0 << 8) | (int)b1);
            }
        }

        public bool IsPDF(string filePath)
        {
            using (var fileStream = _fileSystemHelper.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return IsPDF(fileStream);
            }
        }

        public bool IsPDF(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            char[] buf = new char[5];
            sr.Read(buf, 0, 4);
            sr.Close();

            var responseCheckString = String.Join("", buf);

            return responseCheckString.StartsWith("%PDF");
        }
    }
}
