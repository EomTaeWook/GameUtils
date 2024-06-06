using System;

namespace Utils
{
    public class TimeHelper
    {
        public const int WeekOfDay = 7;
        public static int GetDateTimeToWeek(DateTime dateTime)
        {
            var days = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - dateTime.Ticks).TotalDays;
            return GetDayToWeek(days);
        }
        public static int GetDayToWeek(double days)
        {
            return (int)(days / WeekOfDay);
        }
    }
}
