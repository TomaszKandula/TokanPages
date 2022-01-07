namespace TokanPages.Services.EmailSenderService.Models;

using System;
using Interfaces;

public class ResetPasswordConfiguration : IConfiguration
{
    public string EmailAddress { get; set; } = "";
    
    public Guid ResetId { get; set; }    
    
    public DateTime ExpirationDate { get; set; }
}