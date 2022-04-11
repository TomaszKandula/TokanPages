namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserSignoutDto : BaseClass
{
    public string Caption { get; set; }

    public string OnProcessing { get; set; }

    public string OnFinish { get; set; }

    public string ButtonText { get; set; }
}