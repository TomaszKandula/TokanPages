﻿using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Logic.Mailer.Model;
using TokanPages.BackEnd.Shared.Models.Emails;

namespace TokanPages.BackEnd.Logic.Mailer
{

    public interface IMailer
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Task<MailerResult> Send();
        public bool FieldsCheck();
        Task<string> GetTemplateWithValues(string ATemplate, List<ValueTag> AValueTag);
    }

}
