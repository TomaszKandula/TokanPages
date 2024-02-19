using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class UploadedVideo : Entity<Guid>, IAuditable
{
    public Guid TicketId { get; set; }

    [Required]
    public string SourceBlobUri { get; set; }

    [Required]
    public string TargetVideoUri { get; set; }

    [Required]
    public string TargetThumbnailUri { get; set; }

    public VideoStatus Status { get; set; }

    public bool IsSourceDeleted { get; set; }

    [MaxLength(4000)]
    public string ProcessingWarning { get; set; }

    public long InputSizeInBytes { get; set; }

    public long OutputSizeInBytes { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}