namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;
    using Common;

    public class ActivateAccountDto : BaseClass
    {
        public ContentActivation OnProcessing { get; set; }
        
        public ContentActivation OnSuccess { get; set; }
        
        public ContentActivation OnError { get; set; }
    }
}