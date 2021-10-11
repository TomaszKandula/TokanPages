namespace TokanPages.Backend.Shared.Dto.Users
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class GetUserRoleDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}