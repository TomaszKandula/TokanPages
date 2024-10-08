﻿using MediatR;

namespace TokanPages.Backend.Application.Sender.Mailer.Commands;

public class SendMessageCommand : IRequest<Unit>
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string UserEmail { get; set; } = "";

    public string EmailFrom { get; set; } = "";

    public List<string> EmailTos { get; set; } = new();

    public string Subject { get; set; } = "";

    public string Message { get; set; } = "";

    public string BusinessData { get; set; } = "";
}