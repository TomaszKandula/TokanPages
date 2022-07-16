namespace TokanPages.Backend.Core.Extensions;

using System;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class FormFileExtensions
{
    public static byte[] GetByteArray(this IFormFile? file)
    {
        if (file is null) 
            return Array.Empty<byte>();

        using var fileStream = file.OpenReadStream();

        var bytes = new byte[file.Length];
        fileStream.Read(bytes, 0, (int)file.Length);

        return bytes;
    }
}