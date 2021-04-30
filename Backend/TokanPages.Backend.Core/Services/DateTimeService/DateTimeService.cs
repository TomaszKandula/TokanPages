using System;

namespace TokanPages.Backend.Core.Services.DateTimeService
{
    public class DateTimeService : DateTimeObject, IDateTimeService
    {
        public override DateTime Now => DateTime.UtcNow;

        public override DateTime TodayStartOfDay => DateTime.Today;
        
        public override DateTime TodayEndOfDay => DateTime.Today.AddDays(1).AddTicks(-1);
        
        public override DateTime GetStartOfDay(DateTime AValue) => AValue.Date;
        
        public override DateTime GetEndOfDay(DateTime AValue) => AValue.Date.AddDays(1).AddTicks(-1);
        
        public override DateTime GetFirstDayOfMonth(DateTime AValue) => new DateTime(AValue.Year, AValue.Month, 1);
    }
}