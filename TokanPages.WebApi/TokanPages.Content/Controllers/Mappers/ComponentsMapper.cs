using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Content.Components.Commands;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Content.Dto.Components;

namespace TokanPages.Content.Controllers.Mappers;

/// <summary>
/// Command mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ComponentsMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static RequestPageDataCommand MapToRequestPageDataCommand(RequestPageDataDto model) => new()
    {
        Components = model.Components.Select(item => new ContentModel
            {
                ContentName = item,
                ContentType = "component"
            })
            .ToList(),
        Language = model.Language
    };
}