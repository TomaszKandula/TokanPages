namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Mappers;
    using Backend.Shared.Dto.Mailer;
    using Backend.Identity.Attributes;
    using Backend.Identity.Authorization;
    using MediatR;

    [Authorize]
    public class MailerController : ApiBaseController
    {
        public MailerController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> SendMessage([FromBody] SendMessageDto payLoad)
            => await Mediator.Send(MailerMapper.MapToSendMessageCommand(payLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard)]
        public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto payLoad)
            => await Mediator.Send(MailerMapper.MapToSendNewsletterCommand(payLoad));
    }
}