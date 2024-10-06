namespace TokanPages.Backend.Application.Content.Components.Models;

public class ContentModel
{
    public string ContentType { get; set; } = "";

    public string ContentName { get; set; } = "";

    public dynamic? Content { get; set; }
}