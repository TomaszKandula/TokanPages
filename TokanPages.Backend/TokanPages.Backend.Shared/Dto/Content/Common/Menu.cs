namespace TokanPages.Backend.Shared.Dto.Content.Common;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Menu
{
    public string Image { get; set;  }

    public List<Item> Items { get; set; }
}