namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;
    using Common;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ActivateAccountDto : BaseClass
    {
        public ContentActivation OnProcessing { get; set; }
        
        public ContentActivation OnSuccess { get; set; }
        
        public ContentActivation OnError { get; set; }
    }
}