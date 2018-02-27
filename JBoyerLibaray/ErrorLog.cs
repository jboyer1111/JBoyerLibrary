using JBoyerLibaray.Exceptions;
using JBoyerLibaray.FileSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        private ITimeProvider _timeProvider;

        #endregion

        #region Constructor

        public ErrorLog(string filePath) : this(filePath, new FileSystemHelper(), new TimeProvider()) { }

        public ErrorLog(string filePath, IFileSystemHelper fileSystemHelper, ITimeProvider timeProvider)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw ExceptionHelper.CreateArgumentException(() => filePath, "Cannot be null, empty, or whitespace.");
            }

            if (fileSystemHelper == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => fileSystemHelper);
            }

            if (timeProvider == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => timeProvider);
            }

            _errorLogPath = filePath;
            _fileSystemHelper = fileSystemHelper;
            _timeProvider = timeProvider;
        }

        #endregion

        #region Public Methods

        public void Write(IPrincipal user, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var date = _timeProvider.Now;

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
                (user?.Identity?.IsAuthenticated ?? false) ? user.Identity.Name : "Unknown",
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
