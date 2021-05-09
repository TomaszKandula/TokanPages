using System;

namespace TokanPages.Backend.Core.Models
{
    public class DateRangeValidator
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRangeValidator(DateTime AStartDate, DateTime AEndDate)
        {
            StartDate = AStartDate;
            EndDate = AEndDate;
        }
    }
}