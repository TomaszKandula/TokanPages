using System;

namespace TokanPages.Backend.Shared.Services.DateTimeService
{
    public abstract class DateTimeObject
    {
        public abstract DateTime Now { get; }
        
        public abstract DateTime TodayStartOfDay { get; }

        public abstract DateTime TodayEndOfDay { get; }
        
        public abstract DateTime GetStartOfDay(DateTime AValue);
        
        public abstract DateTime GetEndOfDay(DateTime AValue);
        
        public abstract DateTime GetFirstDayOfMonth(DateTime AValue);
    }
}