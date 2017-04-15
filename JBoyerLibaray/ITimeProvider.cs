using System;

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
}