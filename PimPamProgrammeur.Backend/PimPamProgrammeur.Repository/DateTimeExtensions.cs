using System;
namespace PimPamProgrammeur.Repository
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUtcToGmt(this DateTime dateTime)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

            var utcOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
            var timeOffset = utcOffset.ToOffset(timeZone.GetUtcOffset(utcOffset));

            return timeOffset.DateTime;
        }
    }
}
