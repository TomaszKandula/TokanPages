using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;
using DnsClient;

namespace TokanPages.Logic.MailChecker
{

    public class MailChecker : IMailChecker
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AEmailAddress"></param>
        /// <returns></returns>
        public List<CheckerResult> IsAddressCorrect(List<string> AEmailAddress)
        {

            var Results = new List<CheckerResult>();

            foreach (var Item in AEmailAddress)
            {

                try
                {
                    var LEmailAddress = new MailAddress(Item);
                    Results.Add(new CheckerResult { EmailAddress = Item, IsValid = true });
                }
                catch (FormatException)
                {
                    Results.Add(new CheckerResult { EmailAddress = Item, IsValid = false });
                }

            }

            return Results;

        }

        /// <summary>
        /// Check if given address email have valid domain.
        /// </summary>
        /// <seealso href="https://dnsclient.michaco.net"/>
        /// <param name="AEmailAddress"></param>
        /// <returns></returns>
        public async Task<bool> IsDomainCorrect(string AEmailAddress)
        {

            try
            {

                var LLookupClient = new LookupClient();

                var LGetEmailDomain = AEmailAddress.Split("@");
                var LEmailDomain    = LGetEmailDomain[1];

                var LCheckRecordA    = await LLookupClient.QueryAsync(LEmailDomain, QueryType.A).ConfigureAwait(false);
                var LCheckRecordAaaa = await LLookupClient.QueryAsync(LEmailDomain, QueryType.AAAA).ConfigureAwait(false);
                var LCheckRecordMx   = await LLookupClient.QueryAsync(LEmailDomain, QueryType.MX).ConfigureAwait(false);

                var LRecordA    = LCheckRecordA.Answers.Where(ARecord => ARecord.RecordType == DnsClient.Protocol.ResourceRecordType.A);
                var LRecordAaaa = LCheckRecordAaaa.Answers.Where(ARecord => ARecord.RecordType == DnsClient.Protocol.ResourceRecordType.AAAA);
                var LRecordMx   = LCheckRecordMx.Answers.Where(ARecord => ARecord.RecordType == DnsClient.Protocol.ResourceRecordType.MX);

                var LIsRecordA    = LRecordA.Any();
                var LIsRecordAaaa = LRecordAaaa.Any();
                var LIsRecordMx   = LRecordMx.Any();

                return LIsRecordA || LIsRecordAaaa || LIsRecordMx;

            }
            catch (DnsResponseException)
            {
                return false;
            }

        }

    }

}
