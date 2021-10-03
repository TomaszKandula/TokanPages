namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;

    public class CookiesPromptDto : BaseClass
    {
        public string Caption { get; set; }

        public string Text { get; set; }

        public string Button { get; set; }

        public int Days { get; set; }
    }
}