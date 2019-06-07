using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joost.Data
{
	/// <summary>
	/// Extensions for string classes
	/// </summary>
	public static class StringExtensions
	{
		#region MaxLength
		/// <summary>
		/// Maximum length of input, the string will be extended with ...
		/// </summary>
		/// <param name="input"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string MaxLength(this string input, int maxLength)
		{
			if (string.IsNullOrEmpty(input) || maxLength == 0)
			{
				return string.Empty;
			}

			if (input.Length <= maxLength || input.Length <= 4)
			{
				return input;
			}

			return string.Format("{0} ...", input.Substring(0, maxLength - 4));
		}
		#endregion

		#region OrderByMonthDay
		/// <summary>
		/// Order the dates based on Month and Day.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string OrderByMonthDay(this DateTime? input)
		{
			return input == null ? "ZZZ" : string.Format("{0:MM-dd}", input);
		}
		#endregion

		#region OrderByMonthDayRotering
		/// <summary>
		/// Order the dates based on Month and Day.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string OrderByMonthDayRotering(this DateTime? input)
		{
			if (input == null) { return "ZZZ"; }

			int Today = DateTime.Now.DayOfYear;
			int Year = DateTime.Now.Year;

			if (input.Value.DayOfYear < Today) { Year++; }

			return string.Format("{0}{1:000}", Year, input.Value.DayOfYear);
		}
		#endregion

		#region DisplayDayMonthName
		/// <summary>
		/// Display the date as day without leading 0 and month as abbrividation.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string DisplayDayMonthName(this DateTime? input)
		{
			return input == null ? "<unknown>" : string.Format("{0:d MMM}{1}", 
				input, 
				(input.Value.DayOfYear == DateTime.Now.DayOfYear ? " 🎂" : string.Empty));
		}
		#endregion

		#region DisplayAge
		public static string DisplayAge(this DateTime? input, bool IsBirthDate, DateTime? DeathDate)
		{
			if (input == null || !IsBirthDate) { return string.Empty; }

			int Today = DateTime.Now.DayOfYear;
			int Year = DateTime.Now.Year;

			if (DeathDate != null)
			{
				Today = DeathDate.Value.DayOfYear;
				Year = DeathDate.Value.Year;
			}

			if (input.Value.DayOfYear >= Today) { Year--; }

			return string.Format("{0}{1}",
				(Year - input.Value.Year).ToString(),
				DeathDate != null ? " (†)" : string.Empty);
		}
		#endregion

		#region DisplaySimpleDate
		public static string DisplaySimpleDate(this DateTime? date)
		{
			string result = ((DateTime)date).ToString("yyyy-MM-dd HH:mm").Substring(0, 16);
			return result;
		}
		#endregion

		#region DisplayEventAge
		public static string DisplayEventAge(this DateTime? date)
		{
			string result = string.Empty;
			TimeSpan eventAge = DateTime.Now - (DateTime)date;
			
			
			if (eventAge.Days > 0)
			{
				result += ((int)eventAge.TotalDays).ToString() + " d ";
			}
			else
			{
				result += ((int)eventAge.Hours).ToString() + " h ";
			}

			return result.Trim();
		}
		#endregion
	}
}
