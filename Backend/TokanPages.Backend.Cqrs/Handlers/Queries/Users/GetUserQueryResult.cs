using System;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    public class GetUserQueryResult : GetAllUsersQueryResult
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Registered { get; set; }

        public DateTime? LastLogged { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
