using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace CoreLayer.Models
{
	public static class Utilities
	{
		public static string GenerateCode(int digitsCount)
		{
			Random random = new Random();
			return random.Next((int)Math.Pow(10, digitsCount - 1), ((int)Math.Pow(10, digitsCount)) - 1).ToString();
		}
        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        public static string ToPersianDate(this DateTime date)
        {
            PersianCalendar pc = new();

            return $"{pc.GetHour(date):00}:{pc.GetMinute(date):00}   {pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";        
        }
    }
}
