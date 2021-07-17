namespace TokanPages.Backend.Core.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;
    using Models;

    [ExcludeFromCodeCoverage]
    public static class DateValidationExtensions
    {
        public static IRuleBuilderOptions<T, DateTime> IsSameOrLaterThanDate<T>(this IRuleBuilder<T, DateTime> ARuleBuilder, DateTime ABeforeDate)
            => ARuleBuilder.Must(ADate => ADate >= ABeforeDate);

        public static IRuleBuilderOptions<T, DateRangeValidator> IsValidDateRange<T>(this IRuleBuilder<T, DateRangeValidator> ARuleBuilder)
            =>  ARuleBuilder.Must(ADate => ADate.StartDate <= ADate.EndDate);
        
        public static IRuleBuilderOptions<T, DateRangeValidator> AreDatesSame<T>(this IRuleBuilder<T, DateRangeValidator> ARuleBuilder)
            =>  ARuleBuilder.Must(ADate => ADate.StartDate.Date != ADate.EndDate.Date);
    }
}