using JBoyerLibaray.Exceptions;
using JBoyerLibaray.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.FileSystem
{

    #region Base Case

    public interface IFileSystemHelper
    {
        IDirectoryWrapper Directory { get; }

        IFileWrapper File { get; }
    }
    
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

    #endregion

    #region Directory Prop

    public interface IDirectoryWrapper
    {
        void Copy(string sourcePath, string targetPath);
        void Copy(string sourcePath, string targetPath, bool merge);
        DirectoryInfo CreateDirectory(string path);
        DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity);
        void Delete(string path);
        void Delete(string path, bool recursive);
        IEnumerable<string> EnumerateDirectories(string path);
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern);
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption);
        IEnumerable<string> EnumerateFiles(string path);
        IEnumerable<string> EnumerateFiles(string path, string searchPattern);
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
        IEnumerable<string> EnumerateFileSystemEntries(string path);
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern);
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption);
        bool Exists(string path);
        DirectorySecurity GetAccessControl(string path);
        DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections);
        DateTime GetCreationTime(string path);
        DateTime GetCreationTimeUtc(string path);
        string GetCurrentDirectory();
        string[] GetDirectories(string path);
        string[] GetDirectories(string path, string searchPattern);
        string[] GetDirectories(string path, string searchPattern, SearchOption searchOption);
        string GetDirectoryRoot(string path);
        string[] GetFiles(string path);
        string[] GetFiles(string path, string searchPattern);
        string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
        string[] GetFileSystemEntries(string path);
        string[] GetFileSystemEntries(string path, string searchPattern);
        string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption);
        DateTime GetLastAccessTime(string path);
        DateTime GetLastAccessTimeUtc(string path);
        DateTime GetLastWriteTime(string path);
        DateTime GetLastWriteTimeUtc(string path);
        string[] GetLogicalDrives();
        DirectoryInfo GetParent(string path);
        string GetParentPath(string path);
        bool IsReadable(string path);
        bool IsWriteable(string path);
        void Move(string sourceDirName, string destDirName);
        void SetAccessControl(string path, DirectorySecurity directorySecurity);
        void SetCreationTime(string path, DateTime creationTime);
        void SetCreationTimeUtc(string path, DateTime creationTimeUtc);
        void SetCurrentDirectory(string path);
        void SetLastAccessTime(string path, DateTime lastAccessTime);
        void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);
        void SetLastWriteTime(string path, DateTime lastWriteTime);
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
    }

    [ExcludeFromCodeCoverage]
    public class DirectoryWrapper : IDirectoryWrapper
    {
        internal DirectoryWrapper()
        {

        }

        public void Copy(string sourcePath, string targetPath)
        {
            Copy(sourcePath, targetPath, false);
        }

        public void Copy(string sourcePath, string targetPath, bool merge)
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
                Copy(dir.FullName, Path.Combine(targetDirInfo.FullName, dir.Name), merge);
            }
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            return Directory.CreateDirectory(path, directorySecurity);
        }

        public void Delete(string path)
        {
            Directory.Delete(path);
        }

        public void Delete(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            return Directory.EnumerateDirectories(path, searchPattern);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }

        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return Directory.EnumerateFiles(path, searchPattern);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            return Directory.EnumerateFileSystemEntries(path);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(path, searchPattern);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public DirectorySecurity GetAccessControl(string path)
        {
            return Directory.GetAccessControl(path);
        }

        public DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            return Directory.GetAccessControl(path, includeSections);
        }

        public DateTime GetCreationTime(string path)
        {
            return Directory.GetCreationTime(path);
        }

        public DateTime GetCreationTimeUtc(string path)
        {
            return Directory.GetCreationTimeUtc(path);
        }

        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public string[] GetDirectories(string path, string searchPattern)
        {
            return Directory.GetDirectories(path, searchPattern);
        }

        public string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        public string GetDirectoryRoot(string path)
        {
            return Directory.GetDirectoryRoot(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        public string[] GetFileSystemEntries(string path)
        {
            return Directory.GetFileSystemEntries(path);
        }

        public string[] GetFileSystemEntries(string path, string searchPattern)
        {
            return Directory.GetFileSystemEntries(path, searchPattern);
        }

        public string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFileSystemEntries(path, searchPattern, searchOption);
        }

        public DateTime GetLastAccessTime(string path)
        {
            return Directory.GetLastAccessTime(path);
        }

        public DateTime GetLastAccessTimeUtc(string path)
        {
            return Directory.GetLastAccessTimeUtc(path);
        }

        public DateTime GetLastWriteTime(string path)
        {
            return Directory.GetLastWriteTime(path);
        }

        public DateTime GetLastWriteTimeUtc(string path)
        {
            return Directory.GetLastWriteTimeUtc(path);
        }

        public string[] GetLogicalDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public DirectoryInfo GetParent(string path)
        {
            return Directory.GetParent(path);
        }

        public string GetParentPath(string path)
        {
            return Directory.GetParent(path).FullName;
        }

        public bool IsReadable(string path)
        {
            AuthorizationRuleCollection rules;
            WindowsIdentity identity;

            try
            {
                rules = new DirectoryInfo(path).GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));
                identity = WindowsIdentity.GetCurrent();
            }
            catch (UnauthorizedAccessException uae)
            {
                Trace.WriteLine(uae.ToString());
                return false;
            }

            bool isAllow = false;
            string userSID = identity.User.Value;

            foreach (FileSystemAccessRule rule in rules)
            {
                if (rule.IdentityReference.ToString() == userSID || identity.Groups.Contains(rule.IdentityReference))
                {
                    if ((rule.FileSystemRights.HasFlag(FileSystemRights.Read) ||
                        rule.FileSystemRights.HasFlag(FileSystemRights.ReadAttributes) ||
                        rule.FileSystemRights.HasFlag(FileSystemRights.ReadData)) && rule.AccessControlType == AccessControlType.Deny)
                        return false;
                    else if ((rule.FileSystemRights.HasFlag(FileSystemRights.Read) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.ReadAttributes) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.ReadData)) && rule.AccessControlType == AccessControlType.Allow)
                        isAllow = true;

                }
            }
            return isAllow;
        }

        public bool IsWriteable(string path)
        {
            AuthorizationRuleCollection rules;
            WindowsIdentity identity;
            try
            {
                rules = new DirectoryInfo(path).GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));
                identity = WindowsIdentity.GetCurrent();
            }
            catch (UnauthorizedAccessException uae)
            {
                Debug.WriteLine(uae.ToString());
                return false;
            }

            bool isAllow = false;
            string userSID = identity.User.Value;

            foreach (FileSystemAccessRule rule in rules)
            {
                if (rule.IdentityReference.ToString() == userSID || identity.Groups.Contains(rule.IdentityReference))
                {
                    if ((rule.FileSystemRights.HasFlag(FileSystemRights.Write) ||
                        rule.FileSystemRights.HasFlag(FileSystemRights.WriteAttributes) ||
                        rule.FileSystemRights.HasFlag(FileSystemRights.WriteData) ||
                        rule.FileSystemRights.HasFlag(FileSystemRights.CreateDirectories) ||
                        rule.FileSystemRights.HasFlag(FileSystemRights.CreateFiles)) && rule.AccessControlType == AccessControlType.Deny)
                        return false;
                    else if ((rule.FileSystemRights.HasFlag(FileSystemRights.Write) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.WriteAttributes) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.WriteData) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.CreateDirectories) &&
                        rule.FileSystemRights.HasFlag(FileSystemRights.CreateFiles)) && rule.AccessControlType == AccessControlType.Allow)
                        isAllow = true;

                }
            }

            return isAllow;
        }

        public void Move(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

        public void SetAccessControl(string path, DirectorySecurity directorySecurity)
        {
            Directory.SetAccessControl(path, directorySecurity);
        }

        public void SetCreationTime(string path, DateTime creationTime)
        {
            Directory.SetCreationTime(path, creationTime);
        }

        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            Directory.SetCreationTimeUtc(path, creationTimeUtc);
        }

        public void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            Directory.SetLastAccessTime(path, lastAccessTime);
        }

        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            Directory.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            Directory.SetLastWriteTime(path, lastWriteTime);
        }

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            Directory.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }
    }

    #endregion

    #region File Prop

    public interface IFileWrapper
    {
        void AppendAllLines(string path, IEnumerable<string> contents);
        void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);
        void AppendAllText(string path, string contents);
        void AppendAllText(string path, string contents, Encoding encoding);
        StreamWriter AppendText(string path);
        void Copy(string sourceFileName, string destFileName);
        void Copy(string sourceFileName, string destFileName, bool overwrite);
        Stream Create(string path);
        Stream Create(string path, int bufferSize);
        Stream Create(string path, int bufferSize, FileOptions options);
        Stream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity);
        StreamWriter CreateText(string path);
        void Decrypt(string path);
        void Delete(string path);
        void Encrypt(string path);
        bool Exists(string path);
        FileSecurity GetAccessControl(string path);
        FileSecurity GetAccessControl(string path, AccessControlSections includeSections);
        FileAttributes GetAttributes(string path);
        DateTime GetCreationTime(string path);
        DateTime GetCreationTimeUtc(string path);
        DateTime GetLastAccessTime(string path);
        DateTime GetLastAccessTimeUtc(string path);
        DateTime GetLastWriteTime(string path);
        DateTime GetLastWriteTimeUtc(string path);
        void Move(string sourceFileName, string destFileName);
        Stream Open(string path, FileMode mode);
        Stream Open(string path, FileMode mode, FileAccess access);
        Stream Open(string path, FileMode mode, FileAccess access, FileShare share);
        Stream OpenRead(string path);
        StreamReader OpenText(string path);
        Stream OpenWrite(string path);
        byte[] ReadAllBytes(string path);
        string[] ReadAllLines(string path);
        string[] ReadAllLines(string path, Encoding encoding);
        string ReadAllText(string path);
        string ReadAllText(string path, Encoding encoding);
        IEnumerable<string> ReadLines(string path);
        IEnumerable<string> ReadLines(string path, Encoding encoding);
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
        void SaveStream(string path, Stream stream);
        void SetAccessControl(string path, FileSecurity fileSecurity);
        void SetAttributes(string path, FileAttributes fileAttributes);
        void SetCreationTime(string path, DateTime creationTime);
        void SetCreationTimeUtc(string path, DateTime creationTimeUtc);
        void SetLastAccessTime(string path, DateTime lastAccessTime);
        void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);
        void SetLastWriteTime(string path, DateTime lastWriteTime);
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
        void WriteAllBytes(string path, byte[] bytes);
        void WriteAllLines(string path, string[] contents);
        void WriteAllLines(string path, IEnumerable<string> contents);
        void WriteAllLines(string path, string[] contents, Encoding encoding);
        void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);
        void WriteAllText(string path, string contents);
        void WriteAllText(string path, string contents, Encoding encoding);
        string GetNewFileNamePath(string directoryPath);
    }

    [ExcludeFromCodeCoverage]
    public class FileWrapper : IFileWrapper
    {
        internal FileWrapper()
        {

        }

        public void AppendAllLines(string path, IEnumerable<string> contents)
        {
            File.AppendAllLines(path, contents);
        }

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            File.AppendAllLines(path, contents, encoding);
        }

        public void AppendAllText(string path, string contents)
        {
            File.AppendAllText(path, contents);
        }

        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            File.AppendAllText(path, contents, encoding);
        }

        public StreamWriter AppendText(string path)
        {
            return File.AppendText(path);
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        public Stream Create(string path)
        {
            return File.Create(path);
        }

        public Stream Create(string path, int bufferSize)
        {
            return File.Create(path, bufferSize);
        }

        public Stream Create(string path, int bufferSize, FileOptions options)
        {
            return File.Create(path, bufferSize, options);
        }

        public Stream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
        {
            return File.Create(path, bufferSize, options, fileSecurity);
        }

        public StreamWriter CreateText(string path)
        {
            return File.CreateText(path);
        }

        public void Decrypt(string path)
        {
            File.Decrypt(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public void Encrypt(string path)
        {
            File.Encrypt(path);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public FileSecurity GetAccessControl(string path)
        {
            return File.GetAccessControl(path);
        }

        public FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            return File.GetAccessControl(path, includeSections);
        }

        public FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        public DateTime GetCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        public DateTime GetCreationTimeUtc(string path)
        {
            return File.GetCreationTimeUtc(path);
        }

        public DateTime GetLastAccessTime(string path)
        {
            return File.GetLastAccessTime(path);
        }

        public DateTime GetLastAccessTimeUtc(string path)
        {
            return File.GetLastAccessTimeUtc(path);
        }

        public DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        public DateTime GetLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(path);
        }

        public void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        public Stream Open(string path, FileMode mode)
        {
            return File.Open(path, mode);
        }

        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access);
        }

        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return File.Open(path, mode, access, share);
        }

        public Stream OpenRead(string path)
        {
            return File.OpenRead(path);
        }

        public StreamReader OpenText(string path)
        {
            return File.OpenText(path);
        }

        public Stream OpenWrite(string path)
        {
            return File.OpenWrite(path);
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public string[] ReadAllLines(string path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }

        public IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            return File.ReadLines(path, encoding);
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
        }

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        }

        public void SaveStream(string path, Stream stream)
        {
            using (var fileStream = Create(path))
            {
                try
                {
                    stream.Seek(0);
                }
                catch
                {
                    // Do nothing stream is unseekable
                }

                stream.CopyTo(fileStream);
            }
        }

        public void SetAccessControl(string path, FileSecurity fileSecurity)
        {
            File.SetAccessControl(path, fileSecurity);
        }

        public void SetAttributes(string path, FileAttributes fileAttributes)
        {
            File.SetAttributes(path, fileAttributes);
        }

        public void SetCreationTime(string path, DateTime creationTime)
        {
            File.SetCreationTime(path, creationTime);
        }

        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            File.SetCreationTimeUtc(path, creationTimeUtc);
        }

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            File.SetLastAccessTime(path, lastAccessTime);
        }

        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            File.SetLastWriteTime(path, lastWriteTime);
        }

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public void WriteAllLines(string path, string[] contents)
        {
            File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            File.WriteAllLines(path, contents, encoding);
        }

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            File.WriteAllLines(path, contents, encoding);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }

        public string GetNewFileNamePath(string directoryPath)
        {
            int i = 0;
            FileInfo fileInfo;
            do
            {
                string filename = String.Format("{0}.txt", i++);
                fileInfo = new FileInfo(Path.Combine(directoryPath, filename));
            }
            while (fileInfo.Exists);

            return fileInfo.FullName;
        }
    }

    #endregion

}
