﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.FileSystemHelper
{
    public class FileSystemHelper
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
    }
}
