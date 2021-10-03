namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;

    public class UserSignoutDto : BaseClass
    {
        public string Caption { get; set; }

        public string OnProcessing { get; set; }

        public string OnFinish { get; set; }
    }
}