namespace TokanPages.Backend.Domain.Contracts;

public interface IAuditable
{
	Guid CreatedBy { get; set; }

	DateTime CreatedAt { get; set; }

	Guid? ModifiedBy { get; set; }

	DateTime? ModifiedAt { get; set; }
}