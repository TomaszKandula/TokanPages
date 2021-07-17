namespace TokanPages.Backend.Shared.Services.DateTimeService
{
    using System;

    public interface IDateTimeService
    {
        DateTime Now { get; }

        DateTime TodayStartOfDay { get; }
        
        DateTime TodayEndOfDay { get; }
        
        DateTime GetStartOfDay(DateTime AValue);
        
        DateTime GetEndOfDay(DateTime AValue);
        
        DateTime GetFirstDayOfMonth(DateTime AValue);
    }
}