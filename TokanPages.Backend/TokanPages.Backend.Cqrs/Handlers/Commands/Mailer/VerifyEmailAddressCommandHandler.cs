namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Database;
    using SmtpClient;
    using Core.Logger;

    public class VerifyEmailAddressCommandHandler : TemplateHandler<VerifyEmailAddressCommand, VerifyEmailAddressCommandResult>
    {
        private readonly ISmtpClientService _smtpClientService;

        public VerifyEmailAddressCommandHandler(DatabaseContext databaseContext, ILogger logger, 
            ISmtpClientService smtpClientService) : base(databaseContext, logger)
        {
            _smtpClientService = smtpClientService;
        }

        public override async Task<VerifyEmailAddressCommandResult> Handle(VerifyEmailAddressCommand request, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Verify given e-mail address format: '{request.Email}'.");
            var isAddressCorrect = _smtpClientService.IsAddressCorrect(new List<string> { request.Email });
            var isDomainCorrect = false;
            
            if (isAddressCorrect[0].IsValid)
            {
                Logger.LogInformation($"Verify given e-mail address domain: '{isAddressCorrect[0].Address}'. Format is valid.");
                isDomainCorrect = await _smtpClientService.IsDomainCorrect(request.Email, cancellationToken);
            }
            
            return new VerifyEmailAddressCommandResult
            {
                IsFormatCorrect = isAddressCorrect[0].IsValid,
                IsDomainCorrect = isDomainCorrect
            };
        }
    }
}