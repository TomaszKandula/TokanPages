using System.Collections.Generic;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendMessageCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string EmailFrom { get; set; }
        public List<string> EmailTos { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
