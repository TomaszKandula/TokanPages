namespace TokanPages.Backend.Domain.Contracts;

using System;

public interface IAuditable
{
	Guid CreatedBy { get; set; }

	DateTime CreatedAt { get; set; }

	Guid? ModifiedBy { get; set; }

	DateTime? ModifiedAt { get; set; }
}