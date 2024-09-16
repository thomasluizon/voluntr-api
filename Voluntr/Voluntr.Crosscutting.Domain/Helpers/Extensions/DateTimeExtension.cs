using System.Globalization;

namespace Voluntr.Crosscutting.Domain.Helpers.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToFriendlyDateTimeString(this DateTime date)
        {
            var isToday = Math.Round(DateTime.Now.Subtract(date).TotalDays) == 0;
            var isWeek = Math.Round(DateTime.Now.Subtract(date).TotalDays) <= 7;
            var isYesterday = Math.Round(DateTime.Now.Subtract(date).TotalDays) >= 1 && Math.Round(DateTime.Now.Subtract(date).TotalDays) < 2;

            if (isToday)
                return "hoje";

            if (isYesterday)
                return "ontem";

            if (isWeek)
                return date.ToString("dddd", new CultureInfo("pt-BR")).ToLower();

            return date.ToString(date.Year == DateTime.Now.Year
                ? "d 'de' MMMM"
                : "d 'de' MMM 'de' yyyy", new CultureInfo("pt-BR")).ToLower();
        }

        public static DateTime DateNextMonth(this DateTime date)
        {
            DateTime dateNextMonth = date.AddMonths(1);

            if (dateNextMonth.Day != date.Day)
                dateNextMonth = new DateTime(dateNextMonth.Year, dateNextMonth.Month, DateTime.DaysInMonth(dateNextMonth.Year, dateNextMonth.Month));

            return dateNextMonth;
        }
    }
}
