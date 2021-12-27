namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class DocumentDto : BaseClass
{
    public List<Section> Items { get; set; }
}