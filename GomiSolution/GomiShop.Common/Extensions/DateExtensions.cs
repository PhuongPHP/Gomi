using GomiShop.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GomiShop.Common.Extensions
{
    public static class DateExtensions
    {
        public static long UnixFromDateTime(this DateTime date)
        {
            //var dateTime = new DateTime(user.BirthDay.Year, user.BirthDay.Month, user.BirthDay.Day, user.BirthDay.Hour, user.BirthDay.Minute, user.BirthDay.Second, DateTimeKind.Local);
            var dateTimeOffset = new DateTimeOffset(date, new TimeSpan(0, 0, 0));
            var unixDateTime = dateTimeOffset.ToUnixTimeMilliseconds();
            return unixDateTime;
        }

        public static bool IsDefaultDate(this DateTime date)
        {
            var defaultDate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null).Date;
            if (defaultDate.Year == date.Year)
            {
                return true;
            }
            return false;
        }

        public static DateTime DefaultDate(this DateTime date)
        {
            return new DateTime(1900, 01, 01);
        }

        public static string ToStringWithCompare(this DateTime date, string format)
        {
            var result = DateTime.Compare(date, new DateTime().DefaultDate());
            if (result > 0)
                return date.ToString(format);
            else
                return String.Empty;
        }

        public static long UtcNowTicks
        {
            get
            {
                long lastTimeStamp = DateTime.UtcNow.Ticks;
                long original, newValue;
                do
                {
                    original = lastTimeStamp;
                    long now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange
                             (ref lastTimeStamp, newValue, original) != original);

                return newValue;
            }
        }


        // return how much time passed since date object
        public static string GetTimeSince(this DateTime objDateTime, LanguageType languageType)
        {
            // here we are going to subtract the passed in DateTime from the current time converted to UTC
            //TimeSpan ts = DateTime.Now.ToUniversalTime().Subtract(objDateTime);
            TimeSpan ts = DateTime.Now.Subtract(objDateTime);

            int intDays = ts.Days;
            int intHours = ts.Hours;
            int intMinutes = ts.Minutes;
            int intSeconds = ts.Seconds;

            if (intDays >= 365)
                return string.Format("{0} năm", intDays / 365);

            if (intDays > 0)
                return string.Format("{0} ngày", intDays);

            if (intHours > 0)
                return string.Format("{0} giờ", intHours);

            if (intMinutes > 0)
                return string.Format("{0} phút", intMinutes);

            if (intSeconds > 0)
                return string.Format("{0} giây", intSeconds);

            // let's handle future times..just in case
            if (intDays < 0)
                return string.Format("{0} ngày sau", Math.Abs(intDays));

            if (intHours < 0)
                return string.Format("{0} giờ sau", Math.Abs(intHours));

            if (intMinutes < 0)
                return string.Format("{0} phút sau", Math.Abs(intMinutes));

            if (intSeconds < 0)
                return string.Format("{0} giây sau", Math.Abs(intSeconds));

            return "vừa qua";
        }


        public static string IsTodayOrYesterday(this DateTime date)
        {
            DateTime today = DateTime.Now;
            if (date.Day == today.Day && date.Month == today.Month && date.Year == today.Year)
                return "Hôm nay " + date.ToString("HH:mm");
            else if (date.Day == (today.Day - 1) && date.Month == today.Month && date.Year == today.Year)
                return "Hôm qua " + date.ToString("HH:mm");
            else
                return date.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
