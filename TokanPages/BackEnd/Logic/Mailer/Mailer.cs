using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using TokanPages.BackEnd.Storage;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Logic.Mailer.Model;
using TokanPages.BackEnd.Shared.Models.Emails;

namespace TokanPages.BackEnd.Logic.Mailer
{

    public class Mailer : IMailer
    {

        private readonly SendGridKeys FSendGridKeys;
        private readonly IAzureStorageService FAzureStorageService;

        public Mailer(SendGridKeys ASendGridKeys, IAzureStorageService AAzureStorageService) 
        {
            FSendGridKeys        = ASendGridKeys;
            FAzureStorageService = AAzureStorageService;
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

        public async Task<string> GetTemplateWithValues(string ATemplate, List<ValueTag> AValueTag)
        {

            var LStorageUrl = $"{FAzureStorageService.ReturnBasicUrl}{ATemplate}";
            var LTemplate   = await GetFileFromUrl(LStorageUrl);

            if (AValueTag == null || !AValueTag.Any()) return null;

            foreach (var AItem in AValueTag) 
            {
                LTemplate = LTemplate.Replace(AItem.Tag, AItem.Value);
            }

            return LTemplate;
        
        }

        public async Task<string> GetFileFromUrl(string Url) 
        {        
            var LHttpClient = new HttpClient();
            var LResponse = await LHttpClient.GetAsync(Url);
            return await LResponse.Content.ReadAsStringAsync();
        }

        private async Task<MailerResult> Execute(string AFrom, string ATo, string ASubject, string ABody)
        {

            var Client    = new SendGridClient(FSendGridKeys.ApiKey1);
            var EmailFrom = new EmailAddress(AFrom, AFrom);
            var EmailTo   = new EmailAddress(ATo, ATo);
            var Message   = MailHelper.CreateSingleEmail(EmailFrom, EmailTo, ASubject, "", ABody);
            var LResponse = await Client.SendEmailAsync(Message);

            if (LResponse.StatusCode != HttpStatusCode.Accepted) 
            {
                return new MailerResult
                {
                    IsSucceeded  = false,
                    ErrorCode    = "unsuccessful_response",
                    ErrorMessage = $"Twilio SendGrid responded with '{LResponse.StatusCode}', returned details: '{LResponse.Body.ReadAsStringAsync().Result}'."
                };
            }

            return new MailerResult
            {
                IsSucceeded = true
            };

        }
    }

}
