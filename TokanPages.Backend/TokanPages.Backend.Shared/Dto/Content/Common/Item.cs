#nullable enable

namespace TokanPages.Backend.Shared.Dto.Content.Common
{
    using System.Collections.Generic;

    public class Item : Subitem
    {
        public List<Subitem>? Subitems { get; set; }
    }
}