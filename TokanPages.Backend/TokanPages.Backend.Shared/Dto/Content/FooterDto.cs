namespace TokanPages.Backend.Shared.Dto.Content
{
    using System.Collections.Generic;
    using Base;
    using Common;

    public class FooterDto : BaseClass
    {
        public string Terms { get; set; }

        public string Policy { get; set; }

        public string Copyright { get; set; }

        public string Reserved { get; set; }

        public List<Icon> Icons { get; set; }
    }
}