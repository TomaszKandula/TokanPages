namespace TokanPages.Backend.Shared.Dto.Content
{
    using System.Collections.Generic;
    using Base;
    using Common;

    public class DocumentDto : BaseClass
    {
        public List<Section> Items { get; set; }
    }
}