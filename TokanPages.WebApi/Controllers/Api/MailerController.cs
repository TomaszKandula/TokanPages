using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Identity.Attributes;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;
using MediatR;

namespace TokanPages.WebApi.Controllers.Api
{
    [Authorize]
    public class MailerController : BaseController
    {
        public MailerController(IMediator AMediator) : base(AMediator) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<VerifyEmailAddressCommandResult> VerifyEmailAddress([FromBody] VerifyEmailAddressDto APayLoad) 
            => await FMediator.Send(MailerMapper.MapToVerifyEmailAddressCommand(APayLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> SendMessage([FromBody] SendMessageDto APayLoad)
            => await FMediator.Send(MailerMapper.MapToSendMessageCommand(APayLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto APayLoad)
            => await FMediator.Send(MailerMapper.MapToSendNewsletterCommand(APayLoad));
    }
}
