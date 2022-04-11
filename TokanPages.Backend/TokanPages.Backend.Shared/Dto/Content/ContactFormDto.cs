namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ContactFormDto : BaseClass
{
    public string Caption { get; set; }

    public string Text { get; set; }

    public string Button { get; set; }

    public string Consent { get; set; }

    public string LabelFirstName { get; set; }

    public string LabelLastName { get; set; }

    public string LabelEmail { get; set; }

    public string LabelSubject { get; set; }

    public string LabelMessage { get; set; }
}