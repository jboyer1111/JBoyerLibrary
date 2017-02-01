using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.FileSystem
{
    public class FileSystemHelper : IFileSystemHelper
    {
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void WriteAllText(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        public FileStream GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess)
        {
            return new FileStream(filePath, fileMode, fileAccess);
        }

        public void SaveStreamToFile(string filePath, Stream stream)
        {
            SaveStreamToFile(filePath, stream, false);
        }

        public void SaveStreamToFile(string filePath, Stream stream, bool overwrite)
        {
            FileMode mode = overwrite ? FileMode.Create : FileMode.CreateNew;
            // Make sure stream is at pos 0
            stream.Position = 0;
            using (FileStream fileStream = new FileStream(filePath, mode, FileAccess.ReadWrite))
            {
                stream.CopyTo(fileStream);
            }
        }

        public IEnumerable<FileInfo> GetFilesInDir(string dirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                throw ExceptionHelper.CreateArgumentException(() => dirPath, "Directory does not exist");
            }
            return dirInfo.GetFiles();
        }

        public IEnumerable<DirectoryInfo> GetDirctoriesInDir(string dirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                throw ExceptionHelper.CreateArgumentException(() => dirPath, "Directory does not exist");
            }
            return dirInfo.GetDirectories();
        }
    }
}
