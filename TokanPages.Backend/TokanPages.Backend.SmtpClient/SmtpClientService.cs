using DnsClient;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.SmtpClient.Models;

namespace TokanPages.Backend.SmtpClient
{
    public class SmtpClientService : SmtpClientObject, ISmtpClientService
    {
        private readonly ISmtpClient FSmtpClient;

        private readonly ILookupClient FLookupClient;
        
        private readonly SmtpServerSettingsModel FSmtpServerSettingsModel;

        public SmtpClientService(ISmtpClient ASmtpClient, ILookupClient ALookupClient, 
            SmtpServerSettingsModel ASmtpServerSettingsModel)
        {
            FSmtpClient = ASmtpClient;
            FLookupClient = ALookupClient;
            FSmtpServerSettingsModel = ASmtpServerSettingsModel;
        }

        public override string From { get; set; }
        
        public override List<string> Tos { get; set; }
        
        public override List<string> Ccs { get; set; }
        
        public override List<string> Bccs { get; set; }
        
        public override string Subject { get; set; }
        
        public override string PlainText { get; set; }
        
        public override string HtmlBody { get; set; }

        public override async Task<ActionResultModel> CanConnectAndAuthenticate(CancellationToken ACancellationToken = default)
        {
            try
            {
                var LSslOnConnect = FSmtpServerSettingsModel.IsSSL
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.None;
                
                await FSmtpClient.ConnectAsync(FSmtpServerSettingsModel.Server, 
                    FSmtpServerSettingsModel.Port, LSslOnConnect, ACancellationToken);

                if (!FSmtpClient.IsConnected)
                {
                    return new ActionResultModel
                    {
                        ErrorCode = nameof(ErrorCodes.NOT_CONNECTED_TO_SMTP),
                        ErrorDesc = ErrorCodes.NOT_CONNECTED_TO_SMTP
                    };
                }

                await FSmtpClient.AuthenticateAsync(FSmtpServerSettingsModel.Account, 
                    FSmtpServerSettingsModel.Password, ACancellationToken);

                if (!FSmtpClient.IsAuthenticated)
                {
                    return new ActionResultModel
                    {
                        ErrorCode = nameof(ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP),
                        ErrorDesc = ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP
                    };
                }

                await FSmtpClient.DisconnectAsync(true, ACancellationToken);
                return new ActionResultModel { IsSucceeded = true };
            }
            catch (Exception LException)
            {
                return new ActionResultModel
                {
                    ErrorCode = nameof(ErrorCodes.SMTP_CLIENT_ERROR),
                    ErrorDesc = ErrorCodes.SMTP_CLIENT_ERROR,
                    InnerMessage = LException.Message
                };
            }
        }

        public override async Task<ActionResultModel> Send(CancellationToken ACancellationToken = default)
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

                var LSslOnConnect = FSmtpServerSettingsModel.IsSSL
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.None;
                
                await FSmtpClient.ConnectAsync(FSmtpServerSettingsModel.Server, FSmtpServerSettingsModel.Port, LSslOnConnect, ACancellationToken);
                await FSmtpClient.AuthenticateAsync(FSmtpServerSettingsModel.Account, FSmtpServerSettingsModel.Password, ACancellationToken);

                await FSmtpClient.SendAsync(LNewMail, ACancellationToken);
                await FSmtpClient.DisconnectAsync(true, ACancellationToken);

                return new ActionResultModel { IsSucceeded = true };
            } 
            catch (Exception LException)
            {
                return new ActionResultModel
                {
                    ErrorCode = nameof(ErrorCodes.SMTP_CLIENT_ERROR),
                    ErrorDesc = ErrorCodes.SMTP_CLIENT_ERROR,
                    InnerMessage = LException.Message
                };
            }
        }

        public override List<EmailAddressModel> IsAddressCorrect(IEnumerable<string> AEmailAddress)
        {
            var LResults = new List<EmailAddressModel>();

            foreach (var LItem in AEmailAddress)
            {
                try
                {
                    var LEmailAddress = new MailAddress(LItem);
                    LResults.Add(new EmailAddressModel { EmailAddress = LEmailAddress.Address, IsValid = true });
                }
                catch (FormatException)
                {
                    LResults.Add(new EmailAddressModel { EmailAddress = LItem, IsValid = false });
                }
            }
            
            return LResults;
        }

        public override async Task<bool> IsDomainCorrect(string AEmailAddress)
        {
            try
            {
                var LGetEmailDomain = AEmailAddress.Split("@");
                var LEmailDomain = LGetEmailDomain[1];

                var LCheckRecordA = await FLookupClient.QueryAsync(LEmailDomain, QueryType.A).ConfigureAwait(false);
                var LCheckRecordAaaa = await FLookupClient.QueryAsync(LEmailDomain, QueryType.AAAA).ConfigureAwait(false);
                var LCheckRecordMx = await FLookupClient.QueryAsync(LEmailDomain, QueryType.MX).ConfigureAwait(false);

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
