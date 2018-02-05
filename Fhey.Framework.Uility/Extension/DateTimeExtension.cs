using System;

namespace Fhey.Framework.Uility.FileOperation
{
    public static class DateTimeExtensions
    {
        public static long TimeStamp(this DateTime dateTime)
        {
            return (long)dateTime.Subtract(TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalMilliseconds;
        }
    }
}
