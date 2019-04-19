using JBoyerLibaray.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray
{

    public interface ITimeProvider
    {

        DateTime Now { get; }

        DateTime Today { get; }

        DateTime UtcNow { get; }

        DateTime GetTimezoneTime(TimeZoneInfo timeZoneInfo);

        DateTime GetTimezoneTime(TimeZone timeZone);

    }

    public class TimeProvider : ITimeProvider
    {
        #region Public Properties

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
        }

        public DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        #endregion

        #region Public Mehtods

        public DateTime GetTimezoneTime(TimeZoneInfo timeZoneInfo)
        {
            if (timeZoneInfo == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => timeZoneInfo);
            }

            return TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZoneInfo);
        }

        public DateTime GetTimezoneTime(TimeZone timeZone)
        {
            if (timeZone == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => timeZone);
            }

            return timeZone.ToLocalTime(UtcNow);
        }

        #endregion
    }

}
