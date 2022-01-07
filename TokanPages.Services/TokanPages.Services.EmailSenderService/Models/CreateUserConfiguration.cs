namespace TokanPages.Services.EmailSenderService.Models;

using System;
using Interfaces;

public class CreateUserConfiguration : IConfiguration
{
    public string EmailAddress { get; set; } = "";
    
    public Guid ActivationId { get; set; }    
    
    public DateTime ActivationIdEnds { get; set; }
}