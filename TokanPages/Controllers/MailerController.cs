using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using MediatR;

namespace TokanPages.Controllers
{
    public class MailerController : __BaseController
    {
        public MailerController(IMediator AMediator) : base(AMediator) { }

        [HttpPost]
        public async Task<VerifyEmailAddressCommandResult> VerifyEmailAddress([FromBody] VerifyEmailAddressDto APayLoad) 
            => await FMediator.Send(MailerMapper.MapToVerifyEmailAddressCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> SendMessage([FromBody] SendMessageDto APayLoad)
            => await FMediator.Send(MailerMapper.MapToSendMessageCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto APayLoad)
            => await FMediator.Send(MailerMapper.MapToSendNewsletterCommand(APayLoad));
    }
}
