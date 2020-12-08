using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Logic.Subscribers.Model;
using TokanPages.BackEnd.Controllers.Subscribers.Model;

namespace TokanPages.BackEnd.Logic.Subscribers
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
