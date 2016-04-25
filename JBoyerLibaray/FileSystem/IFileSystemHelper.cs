using System;
using System.IO;

namespace JBoyerLibaray.FileSystem
{
    public interface IFileSystemHelper
    {
        FileStream GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess);
        string[] ReadAllLines(string filePath);
        string ReadAllText(string filePath);
        void SaveStreamToFile(string filePath, Stream stream);
        void SaveStreamToFile(string filePath, Stream stream, bool overwrite);
        void WriteAllText(string filePath, string content);
    }
}
