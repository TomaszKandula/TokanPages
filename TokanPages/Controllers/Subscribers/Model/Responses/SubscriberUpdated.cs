﻿using System.Text.Json.Serialization;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Controllers.Subscribers.Model.Responses
{

    public class SubscriberUpdated
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public SubscriberUpdated()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
