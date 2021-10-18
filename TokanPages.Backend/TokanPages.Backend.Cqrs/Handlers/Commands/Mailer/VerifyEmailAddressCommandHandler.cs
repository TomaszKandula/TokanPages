namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SmtpClient;

    public class VerifyEmailAddressCommandHandler : TemplateHandler<VerifyEmailAddressCommand, VerifyEmailAddressCommandResult>
    {
        private readonly ISmtpClientService _smtpClientService;

        public VerifyEmailAddressCommandHandler(ISmtpClientService smtpClientService) 
            => _smtpClientService = smtpClientService;

        public override async Task<VerifyEmailAddressCommandResult> Handle(VerifyEmailAddressCommand request, CancellationToken cancellationToken)
        {
            var isAddressCorrect = _smtpClientService.IsAddressCorrect(new List<string> { request.Email });
            var isDomainCorrect = false;
            
            if (isAddressCorrect[0].IsValid)
                isDomainCorrect = await _smtpClientService.IsDomainCorrect(request.Email, cancellationToken);

            return new VerifyEmailAddressCommandResult
            {
                IsFormatCorrect = isAddressCorrect[0].IsValid,
                IsDomainCorrect = isDomainCorrect
            };
        }
    }
}