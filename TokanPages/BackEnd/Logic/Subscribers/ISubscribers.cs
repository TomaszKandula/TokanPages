using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Controllers.Subscribers.Model;

namespace TokanPages.BackEnd.Logic.Subscribers
{

    public interface ISubscribers
    {
        Task<List<SubscriberItem>> GetAllSubscribers();
        Task<SubscriberItem> GetSingleSubscriber(string Id);
        Task<string> AddNewSubscriber(SubscriberRequest PayLoad);
        Task<HttpStatusCode> UpdateSubscriber(SubscriberRequest PayLoad);
        Task<HttpStatusCode> DeleteSubscriber(string Id);
    }

}
