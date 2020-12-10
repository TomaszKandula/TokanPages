using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Logic.Subscribers.Model;
using TokanPages.Controllers.Subscribers.Model;

namespace TokanPages.Logic.Subscribers
{

    public interface ISubscribers
    {
        Task<List<SubscriberItem>> GetAllSubscribers();
        Task<SubscriberItem> GetSingleSubscriber(Guid Id);
        Task<NewSubscriber> AddNewSubscriber(SubscriberRequest PayLoad);
        Task<HttpStatusCode> UpdateSubscriber(SubscriberRequest PayLoad);
        Task<HttpStatusCode> DeleteSubscriber(Guid Id);
    }

}
