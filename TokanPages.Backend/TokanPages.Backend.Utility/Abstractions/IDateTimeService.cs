namespace TokanPages.Backend.Utility.Abstractions;

public interface IDateTimeService
{
    DateTime Now { get; }

    DateTimeOffset RelativeNow { get; }

    DateTime TodayStartOfDay { get; }
        
    DateTime TodayEndOfDay { get; }
        
    DateTime GetStartOfDay(DateTime value);
        
    DateTime GetEndOfDay(DateTime value);
        
    DateTime GetFirstDayOfMonth(DateTime value);
}