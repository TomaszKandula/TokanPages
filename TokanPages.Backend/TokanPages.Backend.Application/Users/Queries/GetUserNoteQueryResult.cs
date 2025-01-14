using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNoteQueryResult : IAuditable
{
    public string Note { get; set; } = "";

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}