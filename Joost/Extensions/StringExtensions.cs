using System;

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

      if (input.Value.DayOfYear <= Today) { Year++; }

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

    #region DisplayDateDiff
    public static string DateDiff(this DateTime? DateMeasure)
    {
      return DateDiff((DateTime)DateMeasure, DateTime.Now);
    }

    public static string DateDiff(this DateTime DateMeasure, DateTime DateReference)
    {
      string result = string.Empty;

      DateReference = RoundTimeMinute(DateReference);
      DateMeasure = RoundTimeMinute(DateMeasure);

      TimeSpan timeDiff = DateReference.Subtract(DateMeasure);

      if (timeDiff.Days < 1)
      {
        return $"{timeDiff.Hours} u {timeDiff.Minutes} m";
      }

      if (timeDiff.Days < 7)
      {
        DateTime dateDiff = new DateTime(1, 1, 1);
        dateDiff = dateDiff.Add(timeDiff);
        dateDiff = RoundTimeHour(dateDiff);
        return $"{timeDiff.Days} d {dateDiff.Hour} h";
      }

      (int Week, int Day) weeks = CalcWeeks(timeDiff);

      if (weeks.Week < 14)
      {
        return $"{weeks.Week} w {weeks.Day} d";
      }

      int days = Math.Abs(DateReference.Day - DateMeasure.Day);
      int months = (DateReference.Month - DateMeasure.Month);
      if (DateReference.Day < DateMeasure.Day)
      {
        months--;

        //DateTime dateReference = new DateTime(DateReference.Year, DateReference.Month, DateMeasure.Day);
        DateTime dateReference = new DateTime(DateReference.Year, DateReference.Month, DateReference.Day);
        dateReference.AddMonths(-1);
        days = (DateReference - dateReference).Days;
      }

      int years = DateReference.Year - DateMeasure.Year;
      if (DateReference.DayOfYear < DateMeasure.DayOfYear)
      {
        years--;
        months += 12;
      }

      int count = 0;
      if (years > 0)
      {
        result = $"{years} y";
        count++;
      }

      if (months > 0)
      {
        if (count == 1 && days > 15)
        {
          months++;
        }
        result = $"{result} {months} m".Trim();
        count++;
      }

      if (count < 2 && days > 0)
      {
        result = $"{result} {days} d".Trim();
      }

      return result;
    }

    private static DateTime RoundTimeMinute(DateTime dateTime)
    {

      //Round the DataReference to minute
      if (dateTime.Second < 30)
      {
        dateTime = dateTime.AddSeconds(-dateTime.Second);
      }
      else
      {
        dateTime = dateTime.AddSeconds(60 - dateTime.Second);
      }

      return dateTime;

    }

    private static DateTime RoundTimeHour(DateTime dateTime)
    {

      //Round the DataReference to minute
      if (dateTime.Minute < 30)
      {
        dateTime = dateTime.AddMinutes(-dateTime.Minute);
      }
      else
      {
        dateTime = dateTime.AddMinutes(60 - dateTime.Minute);
      }

      return dateTime;

    }

    private static (int Week, int Day) CalcWeeks(TimeSpan ts)
    {
      int Weeks = ts.Days / 7;
      int Days = ts.Days - (Weeks * 7);

      return (Weeks, Days);
    }

    #endregion
  }
}
