using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Players")]
public class Player : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid PositionId { get; set; }

    [Required]
    [MaxLength(255)]
    public string NickName { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }
    
    public DateTime Birthday { get; set; }
}