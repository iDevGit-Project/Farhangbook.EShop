using System;
using System.Globalization;

namespace MarketPlaceEshop.Common.BaseClasses
{
    public class DateAndTimeShamsi
    {
        public static string DateShamsi()
        {
            var currentDate = DateTime.Now;
            PersianCalendar pcCalender = new PersianCalendar();
            int year = pcCalender.GetYear(currentDate);
            int month = pcCalender.GetMonth(currentDate);
            int day = pcCalender.GetDayOfMonth(currentDate);


            string shamsiDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));
            return shamsiDate;
        }

        public static string MyTime()
        {
            var currentDate = DateTime.Now;
            PersianCalendar pcCalender = new PersianCalendar();
            int year = pcCalender.GetYear(currentDate);
            int month = pcCalender.GetMonth(currentDate);
            int day = pcCalender.GetDayOfMonth(currentDate);

            string NowTime = string.Format("{0:hh:mm:ss tt}", Convert.ToDateTime(pcCalender.GetHour(currentDate) + ":" + pcCalender.GetMinute(currentDate)));
            return NowTime;
        }
    }
}
