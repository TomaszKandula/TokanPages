using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Team : Entity<Guid>
{
    public Guid PlayerId  { get; set; }

    /* Navigation properties */
    public Player Player { get; set; }
}