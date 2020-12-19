﻿using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class VerifyEmailAddressCommandHandler : IRequestHandler<VerifyEmailAddressCommand, VerifyEmailAddressCommandResult>
    {

        private readonly ISmtpClientService FSmtpClientService;

        public VerifyEmailAddressCommandHandler(ISmtpClientService ASmtpClientService) 
        {
            FSmtpClientService = ASmtpClientService;
        }

        public async Task<VerifyEmailAddressCommandResult> Handle(VerifyEmailAddressCommand ARequest, CancellationToken ACancellationToken)
        {

            var LIsAddressCorrect = FSmtpClientService.IsAddressCorrect(new List<string> { ARequest.Email });
            var LIsDomainCorrect = await FSmtpClientService.IsDomainCorrect(ARequest.Email);

            return new VerifyEmailAddressCommandResult
            {
                IsFormatCorrect = LIsAddressCorrect[0].IsValid,
                IsDomainCorrect = LIsDomainCorrect
            };
        
        }

    }

}
