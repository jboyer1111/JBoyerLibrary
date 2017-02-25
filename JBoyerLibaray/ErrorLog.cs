using JBoyerLibaray.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
    public class ErrorLog
    {
        #region Private Varables

        private string _errorLogPath;
        private IFileSystemHelper _fileSystemHelper;

        #endregion

        #region Constructor

        public ErrorLog(string filePath) : this(filePath, new FileSystemHelper()) { }

        public ErrorLog(string filePath, IFileSystemHelper fileSystemHelper)
        {
            _errorLogPath = filePath;
            _fileSystemHelper = fileSystemHelper;
        }

        #endregion

        #region Public Methods

        public void Write(IPrincipal user, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var date = DateTime.Now;

            string name;
            if (date.IsDaylightSavingTime())
            {
                name = TimeZone.CurrentTimeZone.DaylightName;
            }
            else
            {
                name = TimeZone.CurrentTimeZone.StandardName;
            }

            var abbrv = name.Split(' ').Aggregate("", (s, i) => s += i.First().ToString().ToUpper());

            var formattedMessage = String.Format(
                "{0} {1}:\r\n{2}\r\n\r\n",
                String.Format(
                    "{0:MM-dd-yyyy hh:mm:ss} {1} {2}:",
                    date,
                    date.Hour > 12 ? "PM" : "AM",
                    abbrv
                ),
                user.Identity.IsAuthenticated ? user.Identity.Name : "Unknown",
                message
            );

            _fileSystemHelper.File.AppendAllText(_errorLogPath, formattedMessage);
        }

        public void Write(IPrincipal user, Exception exception)
        {
            if (exception == null)
            {
                return;
            }
            
            Write(user, exception.ToString());
        }

        #endregion

    }
}
