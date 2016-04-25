using Microsoft.Build.Framework;
using Microsoft.Web.XmlTransform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JBoyerLibaray.Task
{
    public class TransConfig : ITask
    {
        [Required]
        public string ConfigPath { get; set; }

        [Required]
        public string TransFormConfig { get; set; }

        public string OutputPath { get; set; }

        public bool Execute()
        {
            try
            {
                using (var xmlDoc = new XmlTransformableDocument())
                {
                    xmlDoc.PreserveWhitespace = true;
                    xmlDoc.Load(ConfigPath);

                    using (var xmlTrans = new XmlTransformation(TransFormConfig))
                    {
                        if (xmlTrans.Apply(xmlDoc))
                        {
                            if (!String.IsNullOrWhiteSpace(OutputPath))
                            {
                                xmlDoc.Save(OutputPath);
                            }
                            else
                            {
                                xmlDoc.Save(ConfigPath);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }
    }
}
