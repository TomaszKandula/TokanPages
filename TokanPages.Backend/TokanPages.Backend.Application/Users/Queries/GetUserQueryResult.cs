namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQueryResult
{
    public Guid Id { get; set; }

    public string AliasName { get; set; } = "";

    public bool IsActivated { get; set; }

    public string Email { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public DateTime Registered { get; set; }

    public DateTime? LastLogged { get; set; }

    public DateTime? LastUpdated { get; set; }
}