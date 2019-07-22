using System;

namespace Commons.Extenssions
{
    public static class DateTimeHelper
    {
        public static string ToNormal(this DateTime date)
        {

            return $"{date.Year:0000}{date.Month:00}{date.Day:00}{date.Hour:00}{date.Minute:00}{date.Second:00}";

        }

        public static Int64 ToTimeStamp(this DateTime date)
        {
            var seconds = date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return Convert.ToInt64(seconds);
        }
    }
}
