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

        public MailerController(IMediator AMediator) : base(AMediator)
        {
        }

        [HttpPost]
        public async Task<VerifyEmailAddressCommandResult> VerifyEmailAddress([FromBody] VerifyEmailAddressDto APayLoad) 
        {
            var LCommand = MailerMapper.MapToVerifyEmailAddressCommand(APayLoad);
            return await FMediator.Send(LCommand);        
        }

        [HttpPost]
        public async Task<Unit> SendMessage([FromBody] SendMessageDto APayLoad)
        {
            var LCommand = MailerMapper.MapToSendMessageCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto APayLoad)
        {
            var LCommand = MailerMapper.MapToSendNewsletterCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

    }

}
