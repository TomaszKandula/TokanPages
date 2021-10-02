namespace TokanPages.Backend.Shared.Dto.Content.Common
{
    using System;

    public class Section
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        
        public dynamic Value { get; set; }
        
        public string Prop { get; set; }
        
        public string Text { get; set; }
    }
}