namespace TokanPages.Backend.Application.Users.Queries;

using System;

public class GetAllUsersQueryResult
{
    public Guid Id { get; set; }

    public string AliasName { get; set; } = "";

    public bool IsActivated { get; set; }

    public string Email { get; set; } = "";
}