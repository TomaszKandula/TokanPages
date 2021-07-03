using System;

namespace TokanPages.Backend.Shared.Services.DateTimeService
{
    public class DateTimeService : DateTimeObject, IDateTimeService
    {
        /// <summary>
        /// Returns current DateTime in UTC.
        /// </summary>
        public override DateTime Now => DateTime.UtcNow;

        /// <summary>
        /// Returns today's date and set time at midnight (00:00:00).
        /// </summary>
        public override DateTime TodayStartOfDay => DateTime.Today;
        
        /// <summary>
        /// Returns today's date and set time at second before midnight (23:59:59). 
        /// </summary>
        public override DateTime TodayEndOfDay => DateTime.Today.AddDays(1).AddTicks(-1);
        
        /// <summary>
        /// Returns date component (time set to 00:00:00) from given DateTime. 
        /// </summary>
        /// <param name="AValue">Date and Time.</param>
        /// <returns>Extracted date component from supplied value.</returns>
        public override DateTime GetStartOfDay(DateTime AValue) => AValue.Date;
        
        /// <summary>
        /// Returns date component (time set to 23:59:59) from given DateTime. 
        /// </summary>
        /// <param name="AValue">Date and Time.</param>
        /// <returns>Extracted date component from supplied value.</returns>
        public override DateTime GetEndOfDay(DateTime AValue) => AValue.Date.AddDays(1).AddTicks(-1);
        
        /// <summary>
        /// Returns first day of the month from given Date and Time.
        /// </summary>
        /// <param name="AValue">Date and Time.</param>
        /// <returns>Date and time being first day of the month.</returns>
        public override DateTime GetFirstDayOfMonth(DateTime AValue) => new (AValue.Year, AValue.Month, 1);
    }
}