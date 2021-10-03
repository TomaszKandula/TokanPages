﻿namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Mappers;
    using Backend.Shared.Dto.Mailer;
    using Backend.Identity.Attributes;
    using Backend.Identity.Authorization;
    using Backend.Cqrs.Handlers.Commands.Mailer;
    using MediatR;

    [Authorize]
    public class MailerController : ApiBaseController
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