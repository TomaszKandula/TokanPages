using System;

namespace TokanPages.Backend.Domain.Entities
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string UserAlias { get; set; }
        public bool IsActivated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? LastLogged { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
