using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Mailer.Model;

namespace TokanPages.BackEnd.Mailer
{

    public class Mailer : IMailer
    {

        private readonly SendGridKeys FSendGridKeys;

        public Mailer(SendGridKeys ASendGridKeys) 
        {
            FSendGridKeys = ASendGridKeys;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        private string Body { get; set; }

        public async Task<Result> Send() 
        {

            try 
            {

                if (!FieldsCheck()) 
                {
                    return new Result
                    {
                        ErrorMessage = "No field can be empty.",
                        ErrorCode    = 1001
                    };
                }

                var CheckEmails = CheckEmailAddresses(new List<string> { From, To });
                var TestResults = CheckEmails.Select(EmailAddresses => EmailAddresses.IsValid).Contains(false);
                if (TestResults) 
                {
                    return new Result
                    {
                        ErrorMessage = "Invalid or malformed.",
                        ErrorCode    = 1002
                    };
                }

                await Execute(From, To, Subject, Body);
                return new Result
                {
                    IsSucceeded = true
                };

            } 
            catch (Exception Exception) 
            {
                return new Result
                {
                    ErrorCode    = Exception.HResult,
                    ErrorMessage = Exception.Message
                };                
            }
        
        }

        public bool FieldsCheck()
        {

            if (string.IsNullOrEmpty(From)
                || string.IsNullOrEmpty(To)
                || string.IsNullOrEmpty(Subject)
                || string.IsNullOrEmpty(Body)
                || string.IsNullOrWhiteSpace(From)
                || string.IsNullOrWhiteSpace(To)
                || string.IsNullOrWhiteSpace(Subject)
                || string.IsNullOrWhiteSpace(Body))
            {
                return false;
            }

            return true;

        }

        public List<Emails> CheckEmailAddresses(List<string> AEmailAddress)
        {

            var Results = new List<Emails>();

            foreach (var Item in AEmailAddress) 
            {

                try
                {
                    var LEmailAddress = new MailAddress(Item);
                    Results.Add(new Emails {EmailAddress = Item, IsValid = true });                                   
                }
                catch (FormatException)
                {
                    Results.Add(new Emails { EmailAddress = Item, IsValid = false });
                }

            }

            return Results;

        }

        private async Task Execute(string AFrom, string ATo, string ASubject, string ABody)
        {
            var Client    = new SendGridClient(FSendGridKeys.ApiKey1);
            var EmailFrom = new EmailAddress(AFrom, AFrom);
            var EmailTo   = new EmailAddress(ATo, ATo);
            var Message   = MailHelper.CreateSingleEmail(EmailFrom, EmailTo, ASubject, "", ABody);
            await Client.SendEmailAsync(Message);
        }
    }

}
