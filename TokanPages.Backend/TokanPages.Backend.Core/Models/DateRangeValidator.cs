namespace TokanPages.Backend.Core.Models;

using System;
using System.Diagnostics.CodeAnalysis;

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