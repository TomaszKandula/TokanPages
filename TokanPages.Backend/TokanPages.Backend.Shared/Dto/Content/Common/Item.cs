#nullable enable

namespace TokanPages.Backend.Shared.Dto.Content.Common;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Item : Subitem
{
    public List<Subitem>? Subitems { get; set; }
}