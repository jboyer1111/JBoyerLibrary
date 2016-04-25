using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Task
{
    public class Add2Numbers : ITask
    {
        [Required]
        public double Number1 { get; set; }

        [Required]
        public double Number2 { get; set; }

        [Output]
        public double Result { get; set; }

        public bool Execute()
        {
            Result = Number1 + Number2;

            BuildEngine.LogMessageEvent(new BuildMessageEventArgs("Added 2 numbers", "Add", "Add2Numbers", MessageImportance.High));

            return true;
        }

        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }
    }
}
