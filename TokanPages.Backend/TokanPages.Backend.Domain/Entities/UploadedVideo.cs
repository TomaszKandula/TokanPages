using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UploadedVideos")]
public class UploadedVideo : Entity<Guid>, IAuditable
{
    public required Guid TicketId { get; set; }

    public required string SourceBlobUri { get; set; }

    public required string TargetVideoUri { get; set; }

    public required string TargetThumbnailUri { get; set; }

    public required VideoStatus Status { get; set; }

    public required bool IsSourceDeleted { get; set; }

    public string? ProcessingWarning { get; set; }

    public required long InputSizeInBytes { get; set; }

    public required long OutputSizeInBytes { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}