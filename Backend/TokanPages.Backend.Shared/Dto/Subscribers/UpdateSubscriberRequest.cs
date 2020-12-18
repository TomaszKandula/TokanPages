using System;

namespace TokanPages.Backend.Shared.Dto.Subscribers
{

    public class UpdateSubscriberRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsActivated { get; set; }
        public int Count { get; set; }
    }

}
