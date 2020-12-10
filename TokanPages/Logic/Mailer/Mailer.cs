using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Logic.Mailer.Model;

namespace TokanPages.Logic.Mailer
{

    public class Mailer : IMailer
    {

        private readonly ISmtpClientService   FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;

        public Mailer(ISmtpClientService ASmtpClientService, IAzureStorageService AAzureStorageService) 
        {
            FSmtpClientService   = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
        }

        public string From { get; set; }
        public List<string> Tos { get; set; }
        public List<string> Ccs { get; set; }
        public List<string> Bccs { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public async Task<ActionResult> Send() 
        {

            try 
            {

                if (!ValidateInputs()) 
                {
                    return new ActionResult
                    {
                        ErrorDesc = "No field can be empty.",
                        ErrorCode = "empty_field"
                    };
                }

                return await Execute(From, Tos, Ccs, Bccs, Subject, Body);

            } 
            catch (Exception Exception) 
            {
                return new ActionResult
                {
                    ErrorCode = Exception.HResult.ToString(),
                    ErrorDesc = Exception.Message
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

        private async Task<ActionResult> Execute(string AFrom, List<string> ATos, List<string> ACcs, List<string> ABccs, string ASubject, string ABody)
        {

            FSmtpClientService.From     = AFrom;
            FSmtpClientService.Tos      = ATos;
            FSmtpClientService.Ccs      = ACcs;
            FSmtpClientService.Bccs     = ABccs;
            FSmtpClientService.Subject  = ASubject;
            FSmtpClientService.HtmlBody = ABody;

            return await FSmtpClientService.Send();

        }
    }

}
