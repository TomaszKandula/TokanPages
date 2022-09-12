using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Core.Models;

[ExcludeFromCodeCoverage]
public class DateRangeValidator
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public DateRangeValidator(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}