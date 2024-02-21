using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Gateway.Dto.Articles;

namespace TokanPages.Gateway.Controllers.Mappers;

/// <summary>
/// Articles mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ArticlesMapper
{
    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AddArticleCommand MapToAddArticleCommand(AddArticleDto model) => new()
    {
        Title = model.Title,
        Description = model.Description,
        TextToUpload = model.TextToUpload,
        ImageToUpload = model.ImageToUpload
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateArticleContentCommand MapToUpdateArticleCommand(UpdateArticleContentDto model) => new()
    {
        Id = model.Id,
        Title = model.Title,
        Description = model.Description,
        TextToUpload = model.TextToUpload,
        ImageToUpload = model.ImageToUpload
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateArticleCountCommand MapToUpdateArticleCommand(UpdateArticleCountDto model) => new()
    {
        Id = model.Id
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateArticleLikesCommand MapToUpdateArticleCommand(UpdateArticleLikesDto model) => new() 
    {
        Id = model.Id,
        AddToLikes = model.AddToLikes 
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateArticleVisibilityCommand MapToUpdateArticleCommand(UpdateArticleVisibilityDto model) => new()
    {
        Id = model.Id, 
        IsPublished = model.IsPublished
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RemoveArticleCommand MapToRemoveArticleCommand(RemoveArticleDto model) => new() 
    { 
        Id = model.Id
    };
}