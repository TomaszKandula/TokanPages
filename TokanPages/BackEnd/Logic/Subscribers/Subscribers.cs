using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Controllers.Subscribers.Model;
using SubscribersModel = TokanPages.BackEnd.Database.Model.Subscribers;

namespace TokanPages.BackEnd.Logic.Subscribers
{

    public class Subscribers : ISubscribers
    {

        private readonly ICosmosDbService FCosmosDbService;

        public Subscribers(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
            FCosmosDbService.InitContainer<SubscribersModel>();
        }

        public async Task<List<SubscriberItem>> GetAllSubscribers() 
        {

            var LItems = await FCosmosDbService.GetItems<SubscribersModel>("select * from c");
            if (LItems == null || !LItems.Any()) return null;

            var LResult = new List<SubscriberItem>();
            foreach (var LItem in LItems)
            {

                var SubscriberItem = new SubscriberItem
                {
                    Id           = LItem.Id,
                    Email = LItem.Email,
                    Count        = LItem.Count,
                    Status       = LItem.Status,
                    Registered   = LItem.Registered,
                    LastUpdated  = LItem.LastUpdated
                };

                LResult.Add(SubscriberItem);

            }

            return LResult;
        
        }

        public async Task<SubscriberItem> GetSingleSubscriber(string Id)
        { 

            var LItem = await FCosmosDbService.GetItem<SubscribersModel>(Id);
            if (LItem == null) return null;

            return new SubscriberItem
            {
                Id          = LItem.Id,
                Email       = LItem.Email,
                Count       = LItem.Count,
                Status      = LItem.Status,
                Registered  = LItem.Registered,
                LastUpdated = LItem.LastUpdated
            };
        
        }

        public async Task<string> AddNewSubscriber(SubscriberRequest PayLoad)
        { 

            var NewId = Guid.NewGuid().ToString();
            var InsertNew = new SubscribersModel
            {
                Id          = NewId,
                Email       = PayLoad.Email,
                Count       = PayLoad.Count,
                Status      = PayLoad.Status,
                Registered  = DateTime.Now,
                LastUpdated = null
            };

            if (await FCosmosDbService.AddItem<SubscribersModel>(NewId, InsertNew) == HttpStatusCode.Created)
            {
                return NewId;
            }
            else 
            {
                return string.Empty;
            }
        
        }

        public async Task<HttpStatusCode> UpdateSubscriber(SubscriberRequest PayLoad)
        { 

            var UpdatedSubscriber = new SubscribersModel
            { 
                Id          = PayLoad.Id,
                Email       = PayLoad.Email,
                Count       = PayLoad.Count,
                Status      = PayLoad.Status,
                LastUpdated = PayLoad.LastUpdated
            };

            return await FCosmosDbService.UpdateItem<SubscribersModel>(PayLoad.Id, UpdatedSubscriber);
        
        }

        public async Task<HttpStatusCode> DeleteSubscriber(string Id)
        {
            return await FCosmosDbService.DeleteItem<SubscribersModel>(Id);
        }

    }

}
