namespace TokanPages.Backend.Core.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Models;

[ExcludeFromCodeCoverage]
public static class DateValidationExtensions
{
    public static IRuleBuilderOptions<T, DateTime> IsSameOrLaterThanDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder, DateTime beforeDate)
        => ruleBuilder.Must(date => date >= beforeDate);

    public static IRuleBuilderOptions<T, DateRangeValidator> IsValidDateRange<T>(this IRuleBuilder<T, DateRangeValidator> ruleBuilder)
        =>  ruleBuilder.Must(date => date.StartDate <= date.EndDate);
        
    public static IRuleBuilderOptions<T, DateRangeValidator> AreDatesSame<T>(this IRuleBuilder<T, DateRangeValidator> ruleBuilder)
        =>  ruleBuilder.Must(date => date.StartDate.Date != date.EndDate.Date);
}