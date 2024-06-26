﻿namespace TokanPages.Backend.Application.Users.Queries;

public class GetUsersQueryResult
{
    public Guid Id { get; set; }

    public string AliasName { get; set; } = "";

    public bool IsActivated { get; set; }

    public string Email { get; set; } = "";
}