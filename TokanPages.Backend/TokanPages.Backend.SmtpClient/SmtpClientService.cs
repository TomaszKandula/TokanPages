namespace TokanPages.Backend.SmtpClient
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Shared.Models;
    using Shared.Resources;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit.Text;
    using DnsClient;
    using MimeKit;

    public class SmtpClientService : ISmtpClientService
    {
        private readonly ISmtpClient _smtpClient;

        private readonly ILookupClient _lookupClient;
        
        private readonly SmtpServer _smtpServer;

        public SmtpClientService(ISmtpClient smtpClient, ILookupClient lookupClient, SmtpServer smtpServer)
        {
            _smtpClient = smtpClient;
            _lookupClient = lookupClient;
            _smtpServer = smtpServer;
        }

        public virtual string From { get; set; }
        
        public virtual List<string> Tos { get; set; }
        
        public virtual List<string> Ccs { get; set; }
        
        public virtual List<string> Bccs { get; set; }
        
        public virtual string Subject { get; set; }
        
        public virtual string PlainText { get; set; }
        
        public virtual string HtmlBody { get; set; }

        public virtual async Task<ActionResult> CanConnectAndAuthenticate(CancellationToken cancellationToken = default)
        {
            try
            {
                var sslOnConnect = _smtpServer.IsSSL
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.None;
                
                await _smtpClient.ConnectAsync(_smtpServer.Server, 
                    _smtpServer.Port, sslOnConnect, cancellationToken);

                if (!_smtpClient.IsConnected)
                {
                    return new ActionResult
                    {
                        ErrorCode = nameof(ErrorCodes.NOT_CONNECTED_TO_SMTP),
                        ErrorDesc = ErrorCodes.NOT_CONNECTED_TO_SMTP
                    };
                }

                await _smtpClient.AuthenticateAsync(_smtpServer.Account, 
                    _smtpServer.Password, cancellationToken);

                if (!_smtpClient.IsAuthenticated)
                {
                    return new ActionResult
                    {
                        ErrorCode = nameof(ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP),
                        ErrorDesc = ErrorCodes.NOT_AUTHENTICATED_WITH_SMTP
                    };
                }

                await _smtpClient.DisconnectAsync(true, cancellationToken);
                return new ActionResult { IsSucceeded = true };
            }
            catch (Exception exception)
            {
                return new ActionResult
                {
                    ErrorCode = nameof(ErrorCodes.SMTP_CLIENT_ERROR),
                    ErrorDesc = ErrorCodes.SMTP_CLIENT_ERROR,
                    InnerMessage = exception.Message
                };
            }
        }

        public virtual async Task<ActionResult> Send(CancellationToken cancellationToken = default)
        {
            try
            {
                var newMail = new MimeMessage();

                newMail.From.Add(MailboxAddress.Parse(From));
                newMail.Subject = Subject;

                foreach (var item in Tos) 
                    newMail.To.Add(MailboxAddress.Parse(item));
                
                if (Ccs != null && !Ccs.Any())
                    foreach (var item in Ccs) newMail.Cc.Add(MailboxAddress.Parse(item));

                if (Bccs != null && !Bccs.Any())
                    foreach (var item in Bccs) newMail.Bcc.Add(MailboxAddress.Parse(item));

                if (!string.IsNullOrEmpty(PlainText)) 
                    newMail.Body = new TextPart(TextFormat.Plain) { Text = PlainText };

                if (!string.IsNullOrEmpty(HtmlBody)) 
                    newMail.Body = new TextPart(TextFormat.Html) { Text = HtmlBody };

                var sslOnConnect = _smtpServer.IsSSL
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.None;
                
                await _smtpClient.ConnectAsync(_smtpServer.Server, _smtpServer.Port, sslOnConnect, cancellationToken);
                await _smtpClient.AuthenticateAsync(_smtpServer.Account, _smtpServer.Password, cancellationToken);

                await _smtpClient.SendAsync(newMail, cancellationToken);
                await _smtpClient.DisconnectAsync(true, cancellationToken);

                return new ActionResult { IsSucceeded = true };
            } 
            catch (Exception exception)
            {
                return new ActionResult
                {
                    ErrorCode = nameof(ErrorCodes.SMTP_CLIENT_ERROR),
                    ErrorDesc = ErrorCodes.SMTP_CLIENT_ERROR,
                    InnerMessage = exception.Message
                };
            }
        }

        public virtual List<Email> IsAddressCorrect(IEnumerable<string> emailAddress)
        {
            var results = new List<Email>();

            foreach (var item in emailAddress)
            {
                try
                {
                    var mailAddress = new MailAddress(item);
                    results.Add(new Email { Address = mailAddress.Address, IsValid = true });
                }
                catch (FormatException)
                {
                    results.Add(new Email { Address = item, IsValid = false });
                }
            }
            
            return results;
        }

        public virtual async Task<bool> IsDomainCorrect(string emailAddress, CancellationToken cancellationToken = default)
        {
            try
            {
                var getEmailDomain = emailAddress.Split("@");
                var emailDomain = getEmailDomain[1];

                var checkRecordA = await _lookupClient.QueryAsync(emailDomain, QueryType.A, QueryClass.IN, cancellationToken);
                var checkRecordAaaa = await _lookupClient.QueryAsync(emailDomain, QueryType.AAAA, QueryClass.IN, cancellationToken);
                var checkRecordMx = await _lookupClient.QueryAsync(emailDomain, QueryType.MX, QueryClass.IN, cancellationToken);

                var recordA = checkRecordA.Answers.Where(record => @record.RecordType == DnsClient.Protocol.ResourceRecordType.A);
                var recordAaaa = checkRecordAaaa.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.AAAA);
                var recordMx = checkRecordMx.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.MX);

                var isRecordA = recordA.Any();
                var isRecordAaaa = recordAaaa.Any();
                var isRecordMx = recordMx.Any();

                return isRecordA || isRecordAaaa || isRecordMx;
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }
    }
}