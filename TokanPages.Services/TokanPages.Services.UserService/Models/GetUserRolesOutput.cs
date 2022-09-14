using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class GetUserRolesOutput
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";
}