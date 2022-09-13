using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Core.Extensions;

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