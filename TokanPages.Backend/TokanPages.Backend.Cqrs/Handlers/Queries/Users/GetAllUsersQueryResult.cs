using System;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    public class GetAllUsersQueryResult
    {
        public Guid Id { get; set; }

        public string AliasName { get; set; }

        public bool IsActivated { get; set; }

        public string Email { get; set; }
    }
}
