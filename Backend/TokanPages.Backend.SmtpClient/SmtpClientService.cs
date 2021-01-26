using DnsClient;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient.Settings;
using TokanPages.Backend.SmtpClient.Models;

namespace TokanPages.Backend.SmtpClient
{
    public class SmtpClientService : SmtpClientObject, ISmtpClientService
    {
        private readonly SmtpServerSettings FSmtpServerSettings;

        public SmtpClientService(SmtpServerSettings ASmtpServerSettings)
        {
            FSmtpServerSettings = ASmtpServerSettings;
        }

        public override string From { get; set; }
        public override List<string> Tos { get; set; }
        public override List<string> Ccs { get; set; }
        public override List<string> Bccs { get; set; }
        public override string Subject { get; set; }
        public override string PlainText { get; set; }
        public override string HtmlBody { get; set; }

        public override async Task<SendActionResult> Send()
        {
            try
            {
                var LNewMail = new MimeMessage();

                LNewMail.From.Add(MailboxAddress.Parse(From));
                LNewMail.Subject = Subject;

                foreach (var Item in Tos) 
                    LNewMail.To.Add(MailboxAddress.Parse(Item));
                
                if (Ccs != null && !Ccs.Any())
                    foreach (var Item in Ccs) LNewMail.Cc.Add(MailboxAddress.Parse(Item));

                if (Bccs != null && !Bccs.Any())
                    foreach (var Item in Bccs) LNewMail.Bcc.Add(MailboxAddress.Parse(Item));

                if (!string.IsNullOrEmpty(PlainText)) 
                    LNewMail.Body = new TextPart(TextFormat.Plain) { Text = PlainText };

                if (!string.IsNullOrEmpty(HtmlBody)) 
                    LNewMail.Body = new TextPart(TextFormat.Html) { Text = HtmlBody };

                using var LServer = new MailKit.Net.Smtp.SmtpClient();
                LServer.Connect(FSmtpServerSettings.Server, FSmtpServerSettings.Port, SecureSocketOptions.SslOnConnect);
                await LServer.AuthenticateAsync(FSmtpServerSettings.Account, FSmtpServerSettings.Password);
                await LServer.SendAsync(LNewMail);
                await LServer.DisconnectAsync(true);

                return new SendActionResult
                {
                    IsSucceeded = true
                };
            } 
            catch (Exception LException)
            {
                return new SendActionResult
                {
                    IsSucceeded = false,
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                };
            }
        }

        public override List<CheckActionResult> IsAddressCorrect(List<string> AEmailAddress)
        {
            var Results = new List<CheckActionResult>();
            foreach (var Item in AEmailAddress)
            {
                try
                {
                    var LEmailAddress = new MailAddress(Item);
                    Results.Add(new CheckActionResult { EmailAddress = Item, IsValid = true });
                }
                catch (FormatException)
                {
                    Results.Add(new CheckActionResult { EmailAddress = Item, IsValid = false });
                }
            }
            return Results;
        }

        public override async Task<bool> IsDomainCorrect(string AEmailAddress)
        {
            try
            {
                var LLookupClient = new LookupClient();

                var LGetEmailDomain = AEmailAddress.Split("@");
                var LEmailDomain = LGetEmailDomain[1];

                var LCheckRecordA = await LLookupClient.QueryAsync(LEmailDomain, QueryType.A).ConfigureAwait(false);
                var LCheckRecordAaaa = await LLookupClient.QueryAsync(LEmailDomain, QueryType.AAAA).ConfigureAwait(false);
                var LCheckRecordMx = await LLookupClient.QueryAsync(LEmailDomain, QueryType.MX).ConfigureAwait(false);

                var LRecordA = LCheckRecordA.Answers.Where(ARecord => ARecord.RecordType == DnsClient.Protocol.ResourceRecordType.A);
                var LRecordAaaa = LCheckRecordAaaa.Answers.Where(ARecord => ARecord.RecordType == DnsClient.Protocol.ResourceRecordType.AAAA);
                var LRecordMx = LCheckRecordMx.Answers.Where(ARecord => ARecord.RecordType == DnsClient.Protocol.ResourceRecordType.MX);

                var LIsRecordA = LRecordA.Any();
                var LIsRecordAaaa = LRecordAaaa.Any();
                var LIsRecordMx = LRecordMx.Any();

                return LIsRecordA || LIsRecordAaaa || LIsRecordMx;
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }
    }
}
