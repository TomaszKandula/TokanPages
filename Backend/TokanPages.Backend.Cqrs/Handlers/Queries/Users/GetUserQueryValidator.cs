﻿using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {

        public GetUserQueryValidator() 
        {

            RuleFor(Field => Field.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

        }

    }

}
