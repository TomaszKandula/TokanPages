using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Storage;
using TokanPages.BackEnd.SendGrid;
using TokanPages.BackEnd.Logic.Mailer.Model;
using TokanPages.BackEnd.Shared.Models.Emails;

namespace TokanPages.BackEnd.Logic.Mailer
{

    public class Mailer : IMailer
    {

        private readonly ISendGridService     FSendGridService;
        private readonly IAzureStorageService FAzureStorageService;

        public Mailer(ISendGridService ASendGridService, IAzureStorageService AAzureStorageService) 
        {
            FSendGridService     = ASendGridService;
            FAzureStorageService = AAzureStorageService;
        }

        public string From { get; set; }
        public List<string> Tos { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public async Task<MailerResult> Send() 
        {

            try 
            {

                if (!ValidateInputs()) 
                {
                    return new MailerResult
                    {
                        ErrorMessage = "No field can be empty.",
                        ErrorCode    = "empty_field"
                    };
                }

                return await Execute(From, Tos, Subject, Body);

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

        public bool ValidateInputs()
        {

            if (string.IsNullOrEmpty(From)
                || !Tos.Any()
                || string.IsNullOrEmpty(Subject)
                || string.IsNullOrEmpty(Body)
                || string.IsNullOrWhiteSpace(From)
                || Tos == null
                || string.IsNullOrWhiteSpace(Subject)
                || string.IsNullOrWhiteSpace(Body))
            {
                return false;
            }

            return true;

        }

        public async Task<string> MakeBody(string ATemplate, List<ValueTag> AValueTag)
        {

            var LStorageUrl = $"{FAzureStorageService.GetBaseUrl}{ATemplate}";
            var LTemplate   = await GetFileFromUrl(LStorageUrl);

            if (AValueTag == null || !AValueTag.Any()) return null;

            foreach (var AItem in AValueTag) 
            {
                LTemplate = LTemplate.Replace(AItem.Tag, AItem.Value);
            }

            return LTemplate;
        
        }

        private async Task<string> GetFileFromUrl(string Url) 
        {
            try 
            {
                var LHttpClient = new HttpClient();
                var LResponse = await LHttpClient.GetAsync(Url);
                return await LResponse.Content.ReadAsStringAsync();
            }
            catch
            {
                return Url;
            }
        }

        private async Task<MailerResult> Execute(string AFrom, List<string> ATos, string ASubject, string ABody)
        {

            FSendGridService.From     = AFrom;
            FSendGridService.Tos      = ATos;
            FSendGridService.Subject  = ASubject;
            FSendGridService.HtmlBody = ABody;

            var LResponse = await FSendGridService.Send();

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
