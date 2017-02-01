using JBoyerLibaray.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Write(string message)
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
                "{0}\r\n{1}\r\n\r\n",
                String.Format(
                    "{0:MM-dd-yyyy hh:mm:ss} {1} {2}:",
                    date,
                    date.Hour > 12 ? "PM" : "AM",
                    abbrv
                ),
                message
            );

            _fileSystemHelper.AppendAllText(_errorLogPath, formattedMessage);
        }

        public void Write(Exception exception)
        {
            if (exception == null)
            {
                return;
            }
            
            Write(exception.ToString());
        }

        #endregion

    }
}
