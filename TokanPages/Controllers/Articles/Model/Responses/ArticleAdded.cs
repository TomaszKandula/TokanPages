﻿using System;
using System.Text.Json.Serialization;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Controllers.Articles.Model.Responses
{

    public class ArticleAdded
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("newUid")]
        public Guid NewUid { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ArticleAdded()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
