namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using FluentValidation;
    using Shared.Resources;

    public class UpdateArticleLikesCommandValidator : AbstractValidator<UpdateArticleLikesCommand>
    {
        public UpdateArticleLikesCommandValidator()
        {
            RuleFor(AField => AField.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
            
            RuleFor(AField => AField.AddToLikes)
                .GreaterThan(-1)
                .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                .WithMessage(ValidationCodes.LESS_THAN_ZERO);
        }
    }
}