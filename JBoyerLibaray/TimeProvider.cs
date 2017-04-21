﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray
{
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
            return TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZoneInfo);
        }

        public DateTime GetTimezoneTime(TimeZone timeZone)
        {
            return timeZone.ToLocalTime(UtcNow);
        }

        #endregion
    }
}