namespace TokanPages.Backend.Shared.Dto.Users
{
    using System;

    public class UpdateUserPasswordDto
    {
        public Guid Id { get; set; }

        public Guid? ResetId { get; set; }
        
        public string NewPassword { get; set; }
    }
}