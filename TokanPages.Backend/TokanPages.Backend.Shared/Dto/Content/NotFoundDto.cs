namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class NotFoundDto : BaseClass
    {
        public string Code { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public string Button { get; set; }
    }
}