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
        #region File Methods

        public void AppendAllText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public IEnumerable<string> GetFilesInDir(string dirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                throw ExceptionHelper.CreateArgumentException(() => dirPath, "Directory does not exist");
            }
            return dirInfo.GetFiles().Select(f => f.FullName).ToList();
        }

        public Stream GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess)
        {
            return new FileStream(filePath, fileMode, fileAccess);
        }

        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
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

        public void WriteAllText(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        #endregion

        #region Directory Methods

        public void CopyDir(string sourcePath, string targetPath)
        {
            CopyDir(sourcePath, targetPath, false);
        }

        public void CopyDir(string sourcePath, string targetPath, bool merge)
        {
            // Validate Arugments
            if (String.IsNullOrWhiteSpace(sourcePath))
            {
                throw ExceptionHelper.CreateArgumentNullException(() => sourcePath);
            }

            if (String.IsNullOrWhiteSpace(targetPath))
            {
                throw ExceptionHelper.CreateArgumentNullException(() => targetPath);
            }

            // Test is source directory exists
            DirectoryInfo sourceDirInfo = new DirectoryInfo(sourcePath);
            if (!sourceDirInfo.Exists)
            {
                throw ExceptionHelper.CreateArgumentException(() => sourcePath, "Source path does not exist");
            }

            // Test if target directory exists if it does throw error unless merging
            DirectoryInfo targetDirInfo = new DirectoryInfo(targetPath);
            if (targetDirInfo.Exists && !merge)
            {
                throw ExceptionHelper.CreateArgumentException(() => targetPath, "Target path already exists.");
            }

            // Create target dir if does not exist
            if (!targetDirInfo.Exists)
            {
                targetDirInfo.Create();
            }

            // Copy all files in this dir
            foreach (var file in sourceDirInfo.GetFiles())
            {
                File.Copy(file.FullName, Path.Combine(targetDirInfo.FullName, file.Name));
            }

            // Copy all subdirectories
            foreach (var dir in sourceDirInfo.GetDirectories())
            {
                CopyDir(dir.FullName, Path.Combine(targetDirInfo.FullName, dir.Name), merge);
            }
        }

        public void DeleteDirectory(string dirPath)
        {
            DeleteDirectory(dirPath, false);
        }

        public void DeleteDirectory(string dirPath, bool recursive)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                throw ExceptionHelper.CreateArgumentException(() => dirPath, "Directory does not exist");
            }

            dirInfo.Delete(recursive);
        }

        public IEnumerable<string> GetDirctoriesInDir(string dirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                throw ExceptionHelper.CreateArgumentException(() => dirPath, "Directory does not exist");
            }
            return dirInfo.GetDirectories().Select(d => d.FullName).ToList();
        }

        #endregion
    }
}
