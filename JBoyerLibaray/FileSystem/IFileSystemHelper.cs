using System;
using System.Collections.Generic;
using System.IO;

namespace JBoyerLibaray.FileSystem
{
    public interface IFileSystemHelper
    {
        #region File Methods

        void AppendAllText(string filePath, string content);
        bool FileExists(string filePath);
        IEnumerable<string> GetFilesInDir(string dirPath);
        Stream GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess);
        string[] ReadAllLines(string filePath);
        string ReadAllText(string filePath);
        void SaveStreamToFile(string filePath, Stream stream);
        void SaveStreamToFile(string filePath, Stream stream, bool overwrite);
        void WriteAllText(string filePath, string content);

        #endregion

        #region Directory Methods

        void CopyDir(string sourcePath, string targetPath);
        void CopyDir(string sourcePath, string targetPath, bool merge);
        void DeleteDirectory(string dirPath);
        void DeleteDirectory(string dirPath, bool recursive);
        IEnumerable<string> GetDirctoriesInDir(string dirPath);

        #endregion

    }
}
