namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;

    public class HeaderDto : BaseClass
    {
        public string Photo { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }
    }
}