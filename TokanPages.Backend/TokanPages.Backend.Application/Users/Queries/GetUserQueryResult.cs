namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQueryResult : GetUsersQueryResult
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public DateTime Registered { get; set; }

    public DateTime? LastLogged { get; set; }

    public DateTime? LastUpdated { get; set; }
}