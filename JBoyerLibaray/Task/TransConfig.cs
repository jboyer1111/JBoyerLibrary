using JBoyerLibaray.Extensions;
using JBoyerLibaray.FileSystem;
using Microsoft.Build.Framework;
using Microsoft.Web.XmlTransform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace JBoyerLibaray.Task
{
    public class TransConfig : ITask
    {
        #region Private Varables

        private IFileSystemHelper _fileSystemHelper;

        #endregion

        #region Public Properties

        [Required]
        public string ConfigPath { get; set; }

        [Required]
        public string TransFormConfig { get; set; }

        public string OutputPath { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        #endregion

        #region Constructor

        public TransConfig() : this(new FileSystemHelper()) { }

        public TransConfig(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper;
        }

        #endregion

        #region Public Method
        
        public bool Execute()
        {
            try
            {
                using (var config = new MemoryStream())
                using (var transConfig = _fileSystemHelper.File.OpenRead(TransFormConfig))
                using (var xmlDoc = new XmlTransformableDocument())
                {
                    // Open the file to read it then close it.
                    using (var fileStream = _fileSystemHelper.File.OpenRead(ConfigPath))
                    {
                        fileStream.CopyTo(config);
                        // Make sure stream is at begining
                        config.Seek(0);
                    }
                    
                    xmlDoc.PreserveWhitespace = true;
                    xmlDoc.Load(config);

                    using (var xmlTrans = new XmlTransformation(transConfig, null))
                    {
                        if (xmlTrans.Apply(xmlDoc))
                        {
                            var filePath = !String.IsNullOrWhiteSpace(OutputPath) ? OutputPath : ConfigPath;

                            using (var fileStream = _fileSystemHelper.File.Open(filePath, FileMode.Create))
                            {
                                xmlDoc.Save(fileStream);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        #endregion
    }
}
