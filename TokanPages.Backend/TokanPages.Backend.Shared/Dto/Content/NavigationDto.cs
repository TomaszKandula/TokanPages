namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class NavigationDto : BaseClass
{
    public string Logo { get; set; }

    public Menu Menu { get; set; }
}