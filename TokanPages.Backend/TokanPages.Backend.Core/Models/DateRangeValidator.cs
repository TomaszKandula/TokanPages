using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Models
{
    [ExcludeFromCodeCoverage]
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