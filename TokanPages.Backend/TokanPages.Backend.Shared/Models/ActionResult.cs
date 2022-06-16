namespace TokanPages.Backend.Shared.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ActionResult //TODO: relocate model
{
    public bool IsSucceeded { get; set; }

    public string ErrorCode { get; set; } = "";

    public string ErrorDesc { get; set; } = "";

    public string InnerMessage { get; set; } = "";
}