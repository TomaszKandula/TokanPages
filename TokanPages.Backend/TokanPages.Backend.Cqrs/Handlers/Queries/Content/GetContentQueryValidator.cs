namespace TokanPages.Backend.Cqrs.Handlers.Queries.Content
{
    using FluentValidation;
    using Shared.Resources;

    public class GetContentQueryValidator : AbstractValidator<GetContentQuery>
    {
        public GetContentQueryValidator()
        {
            RuleFor(AField => AField.Type)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(AField => AField.Name)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}