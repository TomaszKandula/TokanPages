using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Requests;
using TokanPages.Backend.Shared.Dto.Responses;
using MediatR;

namespace TokanPages.Controllers
{

    public class MailerController : __BaseController
    {

        public MailerController(IMediator AMediator) : base(AMediator)
        {
        }

        [HttpPost]
        public async Task<VerifyEmailAddressResponse> VerifyEmailAddress(VerifyEmailAddressRequest APayLoad) 
        {
            var LCommand = MailerMapper.MapToVerifyEmailAddressCommand(APayLoad);
            return await FMediator.Send(LCommand);        
        }

    }

}
