namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    using FluentValidation;
    using Shared.Resources;

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator() 
        {
            RuleFor(query => query.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}