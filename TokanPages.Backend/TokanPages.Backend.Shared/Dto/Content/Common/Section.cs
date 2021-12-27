namespace TokanPages.Backend.Shared.Dto.Content.Common;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Section
{
    public Guid Id { get; set; }

    public string Type { get; set; }
        
    public dynamic Value { get; set; }
        
    public string Prop { get; set; }
        
    public string Text { get; set; }
}