namespace TokanPages.Backend.Core.Utilities.DateTimeService
{
    using System;

    public interface IDateTimeService
    {
        DateTime Now { get; }

        DateTime TodayStartOfDay { get; }
        
        DateTime TodayEndOfDay { get; }
        
        DateTime GetStartOfDay(DateTime value);
        
        DateTime GetEndOfDay(DateTime value);
        
        DateTime GetFirstDayOfMonth(DateTime value);
    }
}