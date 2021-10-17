namespace TokanPages.Backend.Core.Utilities.DateTimeService
{
    using System;

    public abstract class DateTimeObject
    {
        public abstract DateTime Now { get; }
        
        public abstract DateTime TodayStartOfDay { get; }

        public abstract DateTime TodayEndOfDay { get; }
        
        public abstract DateTime GetStartOfDay(DateTime value);
        
        public abstract DateTime GetEndOfDay(DateTime value);
        
        public abstract DateTime GetFirstDayOfMonth(DateTime value);
    }
}