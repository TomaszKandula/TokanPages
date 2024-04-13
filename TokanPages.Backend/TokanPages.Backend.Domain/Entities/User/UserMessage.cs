using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserMessage : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string ChatKey { get; set; }

    [Required]
    public string ChatData { get; set; }    

    public bool IsArchived { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}