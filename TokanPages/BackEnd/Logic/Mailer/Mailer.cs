using System;
using System.Net;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Logic.Mailer
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
        public string Body { get; set; }

        public async Task<MailerResult> Send() 
        {

            try 
            {

                if (!FieldsCheck()) 
                {
                    return new MailerResult
                    {
                        ErrorMessage = "No field can be empty.",
                        ErrorCode    = "empty_field"
                    };
                }

                return await Execute(From, To, Subject, Body);

            } 
            catch (Exception Exception) 
            {
                return new MailerResult
                {
                    ErrorCode    = Exception.HResult.ToString(),
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

        private async Task<MailerResult> Execute(string AFrom, string ATo, string ASubject, string ABody)
        {

            var Client    = new SendGridClient(FSendGridKeys.ApiKey1);
            var EmailFrom = new EmailAddress(AFrom, AFrom);
            var EmailTo   = new EmailAddress(ATo, ATo);
            var Message   = MailHelper.CreateSingleEmail(EmailFrom, EmailTo, ASubject, "", ABody);
            var LResponse = await Client.SendEmailAsync(Message);

            if (LResponse.StatusCode != HttpStatusCode.OK) 
            {
                return new MailerResult
                {
                    IsSucceeded  = false,
                    ErrorCode    = "unsuccessful_response",
                    ErrorMessage = $"Twilio SendGrid responded with '{LResponse.StatusCode}', returned details: '{LResponse.Body}'."
                };
            }

            return new MailerResult
            {
                IsSucceeded = true
            };

        }
    }

}
