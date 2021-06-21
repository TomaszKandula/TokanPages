using DnsClient;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient.Models;
using TokanPages.Backend.SmtpClient.Settings;

namespace TokanPages.Backend.SmtpClient
{
    public class SmtpClientService : SmtpClientObject, ISmtpClientService
    {
        private const string NOT_CONNECTED_TO_SMTP = "Client is not connected to the SMTP server";

        private const string NOT_AUTHENTICATED_WITH_SMTP = "Client is not authenticated with the SMTP server";
        
        private readonly SmtpServerSettings FSmtpServerSettings;

        public SmtpClientService(SmtpServerSettings ASmtpServerSettings)
            => FSmtpServerSettings = ASmtpServerSettings;

        public override string From { get; set; }
        
        public override List<string> Tos { get; set; }
        
        public override List<string> Ccs { get; set; }
        
        public override List<string> Bccs { get; set; }
        
        public override string Subject { get; set; }
        
        public override string PlainText { get; set; }
        
        public override string HtmlBody { get; set; }

        public override async Task<SendActionResult> CanConnectAndAuthenticate(CancellationToken ACancellationToken = default)
        {
            try
            {
                var LServer = await ConnectAndAuthenticate(ACancellationToken);

                if (!LServer.IsConnected)
                {
                    return new SendActionResult
                    {
                        IsSucceeded = false,
                        ErrorCode = nameof(NOT_CONNECTED_TO_SMTP),
                        ErrorDesc = NOT_CONNECTED_TO_SMTP
                    };
                }

                if (!LServer.IsAuthenticated)
                {
                    return new SendActionResult
                    {
                        IsSucceeded = false,
                        ErrorCode = nameof(NOT_AUTHENTICATED_WITH_SMTP),
                        ErrorDesc = NOT_AUTHENTICATED_WITH_SMTP
                    };
                }

                await LServer.DisconnectAsync(true, ACancellationToken);
                return new SendActionResult { IsSucceeded = true };
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

        public override async Task<SendActionResult> Send(CancellationToken ACancellationToken = default)
        {
            try
            {
                var LNewMail = new MimeMessage();

                LNewMail.From.Add(MailboxAddress.Parse(From));
                LNewMail.Subject = Subject;

                foreach (var LItem in Tos) 
                    LNewMail.To.Add(MailboxAddress.Parse(LItem));
                
                if (Ccs != null && !Ccs.Any())
                    foreach (var LItem in Ccs) LNewMail.Cc.Add(MailboxAddress.Parse(LItem));

                if (Bccs != null && !Bccs.Any())
                    foreach (var LItem in Bccs) LNewMail.Bcc.Add(MailboxAddress.Parse(LItem));

                if (!string.IsNullOrEmpty(PlainText)) 
                    LNewMail.Body = new TextPart(TextFormat.Plain) { Text = PlainText };

                if (!string.IsNullOrEmpty(HtmlBody)) 
                    LNewMail.Body = new TextPart(TextFormat.Html) { Text = HtmlBody };

                var LServer = await ConnectAndAuthenticate(ACancellationToken);
                await LServer.SendAsync(LNewMail, ACancellationToken);
                await LServer.DisconnectAsync(true, ACancellationToken);

                return new SendActionResult { IsSucceeded = true };
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

        public override List<CheckActionResult> IsAddressCorrect(IEnumerable<string> AEmailAddress)
        {
            var LResults = new List<CheckActionResult>();

            foreach (var LItem in AEmailAddress)
            {
                try
                {
                    var LEmailAddress = new MailAddress(LItem);
                    LResults.Add(new CheckActionResult { EmailAddress = LEmailAddress.Address, IsValid = true });
                }
                catch (FormatException)
                {
                    LResults.Add(new CheckActionResult { EmailAddress = LItem, IsValid = false });
                }
            }
            
            return LResults;
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

        private async Task<MailKit.Net.Smtp.SmtpClient> ConnectAndAuthenticate(CancellationToken ACancellationToken)
        {
            using var LServer = new MailKit.Net.Smtp.SmtpClient();
            await LServer.ConnectAsync(FSmtpServerSettings.Server, FSmtpServerSettings.Port, SecureSocketOptions.SslOnConnect, ACancellationToken);
            await LServer.AuthenticateAsync(FSmtpServerSettings.Account, FSmtpServerSettings.Password, ACancellationToken);
            return LServer;
        }
    }
}
