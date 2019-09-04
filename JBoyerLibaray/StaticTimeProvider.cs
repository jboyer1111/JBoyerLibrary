using JBoyerLibaray.Exceptions;
using System;
using System.Threading;

namespace JBoyerLibaray
{

    public static class StaticTimeProvider
    {

        #region Private Variables

        private static ITimeProvider _timeProvider = null;
        private static ThreadLocal<Context<ITimeProvider>> _timeContext = new ThreadLocal<Context<ITimeProvider>>();

        #endregion

        #region Public Properties

        public static DateTime Now
        {
            get
            {
                if (_timeContext.Value != null && !_timeContext.Value.IsDisposed)
                {
                    return _timeContext.Value.Value.Now;
                }

                if (_timeProvider != null)
                {
                    return _timeProvider.Now;
                }

                return DateTime.Now;
            }
        }

        public static DateTime Today
        {
            get
            {
                if (_timeContext.Value != null && !_timeContext.Value.IsDisposed)
                {
                    return _timeContext.Value.Value.Today;
                }

                if (_timeProvider != null)
                {
                    return _timeProvider.Today;
                }

                return DateTime.Today;
            }
        }

        public static DateTime UtcNow
        {
            get
            {
                if (_timeContext.Value != null && !_timeContext.Value.IsDisposed)
                {
                    return _timeContext.Value.Value.UtcNow;
                }

                if (_timeProvider != null)
                {
                    return _timeProvider.UtcNow;
                }

                return DateTime.UtcNow;
            }
        }

        #endregion

        #region Public Methods

        public static DateTime GetTimezoneTime(TimeZoneInfo timeZoneInfo)
        {
            if (timeZoneInfo == null)
            {
                throw ExceptionHelper.CreateArgumentNullException(() => timeZoneInfo);
            }

            return TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZoneInfo);
        }

        public static DateTime GetTimezoneTime(TimeZone timeZone)
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
