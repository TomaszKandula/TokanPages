using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.WebApi.Dto.Articles;

namespace TokanPages.WebApi.Controllers.Mappers;

/// <summary>
/// Articles mapper
/// </summary>
[ExcludeFromCodeCoverage]
public static class ArticlesMapper
{
    /// <summary>
    /// Maps request DTO to given command 
    /// </summary>
    /// <param name="model">AddArticleDto</param>
    /// <returns>AddArticleCommand</returns>
    public static AddArticleCommand MapToAddArticleCommand(AddArticleDto model) => new()
    {
        Title = model.Title,
        Description = model.Description,
        TextToUpload = model.TextToUpload,
        ImageToUpload = model.ImageToUpload
    };

    /// <summary>
    /// Maps request DTO to given command 
    /// </summary>
    /// <param name="model">UpdateArticleContentDto</param>
    /// <returns>UpdateArticleContentCommand</returns>
    public static UpdateArticleContentCommand MapToUpdateArticleCommand(UpdateArticleContentDto model) => new()
    {
        Id = model.Id,
        Title = model.Title,
        Description = model.Description,
        TextToUpload = model.TextToUpload,
        ImageToUpload = model.ImageToUpload
    };

    /// <summary>
    /// Maps request DTO to given command 
    /// </summary>
    /// <param name="model">UpdateArticleCountDto</param>
    /// <returns>UpdateArticleCountCommand</returns>
    public static UpdateArticleCountCommand MapToUpdateArticleCommand(UpdateArticleCountDto model) => new()
    {
        Id = model.Id
    };

    /// <summary>
    /// Maps request DTO to given command 
    /// </summary>
    /// <param name="model">UpdateArticleLikesDto</param>
    /// <returns>UpdateArticleLikesCommand</returns>
    public static UpdateArticleLikesCommand MapToUpdateArticleCommand(UpdateArticleLikesDto model) => new() 
    {
        Id = model.Id,
        AddToLikes = model.AddToLikes 
    };

    /// <summary>
    /// Maps request DTO to given command 
    /// </summary>
    /// <param name="model">UpdateArticleVisibilityDto</param>
    /// <returns>UpdateArticleVisibilityCommand</returns>
    public static UpdateArticleVisibilityCommand MapToUpdateArticleCommand(UpdateArticleVisibilityDto model) => new()
    {
        Id = model.Id, 
        IsPublished = model.IsPublished
    };

    /// <summary>
    /// Maps request DTO to given command 
    /// </summary>
    /// <param name="model">RemoveArticleDto</param>
    /// <returns>RemoveArticleCommand</returns>
    public static RemoveArticleCommand MapToRemoveArticleCommand(RemoveArticleDto model) => new() 
    { 
        Id = model.Id
    };
}