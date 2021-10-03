namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;
    using Common;

    public class NavigationDto : BaseClass
    {
        public string Logo { get; set; }

        public Menu Menu { get; set; }
    }
}