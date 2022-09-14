using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using TokanPages.Backend.Core.Extensions.Models;

namespace TokanPages.Backend.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class DateValidationExtensions
{
    public static IRuleBuilderOptions<T, DateTime> IsSameOrLaterThanDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder, DateTime beforeDate)
        => ruleBuilder.Must(date => date >= beforeDate);

    public static IRuleBuilderOptions<T, DateRangeModel> IsValidDateRange<T>(this IRuleBuilder<T, DateRangeModel> ruleBuilder)
        =>  ruleBuilder.Must(date => date.StartDate <= date.EndDate);
        
    public static IRuleBuilderOptions<T, DateRangeModel> AreDatesSame<T>(this IRuleBuilder<T, DateRangeModel> ruleBuilder)
        =>  ruleBuilder.Must(date => date.StartDate.Date != date.EndDate.Date);
}