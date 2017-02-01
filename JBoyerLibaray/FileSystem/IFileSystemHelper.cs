using System;
using System.IO;

namespace JBoyerLibaray.FileSystem
{
    public interface IFileSystemHelper
    {
        string[] ReadAllLines(string filePath);
        string ReadAllText(string filePath);
        void WriteAllText(string filePath, string content);
        void AppendAllText(string filePath, string content);
        Stream GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess);
        void SaveStreamToFile(string filePath, Stream stream);
        void SaveStreamToFile(string filePath, Stream stream, bool overwrite);
        IEnumerable<FileInfo> GetFilesInDir(string dirPath);
        IEnumerable<DirectoryInfo> GetDirctoriesInDir(string dirPath);
    }
}
