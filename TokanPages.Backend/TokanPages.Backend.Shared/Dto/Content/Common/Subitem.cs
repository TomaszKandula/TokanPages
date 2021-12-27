#nullable enable

namespace TokanPages.Backend.Shared.Dto.Content.Common;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Subitem
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public string? Value { get; set; }

    public string? Link { get; set; }

    public string? Icon { get; set; }

    public bool? Enabled { get; set; }
}