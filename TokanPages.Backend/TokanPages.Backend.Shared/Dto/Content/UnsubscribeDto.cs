namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UnsubscribeDto : BaseClass
{
    public ContentUnsubscribe ContentPre { get; set; }

    public ContentUnsubscribe ContentPost { get; set; }
}