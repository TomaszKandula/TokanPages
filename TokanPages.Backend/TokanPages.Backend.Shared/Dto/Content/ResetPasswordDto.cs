namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ResetPasswordDto : BaseClass
    {
        public string Caption { get; set; }

        public string Button { get; set; }
    }
}