﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Controllers.Subscribers.Model.Responses
{

    public class ReturnSubscribers
    {

        [JsonPropertyName("subscribers")]
        public List<SubscriberItem> Subscribers { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ReturnSubscribers() 
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
