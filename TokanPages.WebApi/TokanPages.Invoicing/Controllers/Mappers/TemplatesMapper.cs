using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Invoicing.Templates.Commands;
using TokanPages.Invoicing.Dto.Templates;

namespace TokanPages.Invoicing.Controllers.Mappers;

/// <summary>
/// Template mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class TemplatesMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Template object.</param>
    /// <returns>Command object.</returns>
    public static ReplaceInvoiceTemplateCommand MapToReplaceInvoiceTemplateCommandRequest(ReplaceInvoiceTemplateDto model) => new()
    {
        Id = model.Id,
        Data = GetFileContent(model.Data),
        DataType = model.Data != null ? model.Data?.ContentType : string.Empty,
        Description = model.Description
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Template object.</param>
    /// <returns>Command object.</returns>
    public static AddInvoiceTemplateCommand MapToAddInvoiceTemplateCommandRequest(AddInvoiceTemplateDto model) => new()
    {
        Name = model.Name,
        Data = GetFileContent(model.Data),
        DataType = model.Data != null ? model.Data?.ContentType : string.Empty,
        Description = model.Description
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Template object.</param>
    /// <returns>Command object.</returns>
    public static RemoveInvoiceTemplateCommand MapToRemoveInvoiceTemplateCommandRequest(RemoveInvoiceTemplateDto model) => new()
    {
        Id = model.Id
    };

    private static byte[] GetFileContent(IFormFile? file)
    {
        if (file is null)
            return Array.Empty<byte>();

        using var fileStream = file.OpenReadStream();

        var bytes = new byte[file.Length];
        fileStream.Read(bytes, 0, (int)file.Length);

        return bytes;
    }
}