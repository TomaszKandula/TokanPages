using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Content.Assets.Queries.Models;

public class ContentOutput
{
    public FileContentResult? FileContent { get; set; }

    public string FileName { get; set; } = "";
}