using System;

namespace GameUtils
{
    public class TimeHelper
    {
        public const int WeekOfDay = 7;
        public static int GetDateTimeToWeek(DateTime dateTime)
        {
            var totalDays = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - dateTime.Ticks).TotalDays;
            return GetDayToWeek(totalDays);
        }
        public static int GetDayToWeek(double totalDays)
        {
            return (int)(totalDays / WeekOfDay);
        }

        public static DateTime CalculateStartOfWeek(DateTime currentDateTime)
        {
            var dateTime = currentDateTime.Date;
            int daysToWeekStart;
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                daysToWeekStart = (int)DayOfWeek.Saturday;
            }
            else
            {
                daysToWeekStart = dateTime.DayOfWeek - DayOfWeek.Monday;
            }
            var weekStartDate = dateTime.AddDays(-daysToWeekStart);
            return weekStartDate;
        }

    }
}
