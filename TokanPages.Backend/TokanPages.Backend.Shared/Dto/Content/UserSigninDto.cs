namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserSigninDto : BaseClass
{
    public string Caption { get; set; }

    public string Button { get; set; }
        
    public string Link1 { get; set; }
        
    public string Link2 { get; set; }

    public string LabelEmail { get; set; }

    public string LabelPassword { get; set; }
}