namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to remove user asset (image/video)
/// </summary>
public class RemoveUserMediaDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string UniqueBlobName { get; set; } = "";
}