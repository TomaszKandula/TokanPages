namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserSignupDto : BaseClass
{
    public string Caption { get; set; }

    public string Button { get; set; }

    public string Link { get; set; }

    public string Consent { get; set; }

    public string LabelFirstName { get; set; }

    public string LabelLastName { get; set; }

    public string LabelEmail { get; set; }

    public string LabelPassword { get; set; }
}