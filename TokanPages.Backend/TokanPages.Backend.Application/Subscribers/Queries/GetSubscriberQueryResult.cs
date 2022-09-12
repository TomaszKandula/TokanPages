﻿namespace TokanPages.Backend.Application.Subscribers.Queries;

using System;

public class GetSubscriberQueryResult : GetAllSubscribersQueryResult
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}