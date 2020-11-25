using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using TokanPages.BackEnd.Shared;
using System.Collections.Generic;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Logic.Subscribers.Model;
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

            var LItems = await FCosmosDbService.GetItems<SubscribersModel>("select * from Subscribers");
            if (LItems == null || !LItems.Any()) return null;

            var LResult = new List<SubscriberItem>();
            foreach (var LItem in LItems)
            {

                var SubscriberItem = new SubscriberItem
                {
                    Id          = LItem.Id,
                    Email       = LItem.Email,
                    Count       = LItem.Count,
                    Status      = LItem.Status,
                    Registered  = LItem.Registered,
                    LastUpdated = LItem.LastUpdated
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

        public async Task<NewSubscriber> AddNewSubscriber(SubscriberRequest PayLoad)
        {

            var LQuery = $"select * from Subscribers s where s.email = \"{PayLoad.Email}\"";
            var LItems = await FCosmosDbService.GetItems<SubscribersModel>(LQuery);
            if (LItems.Any())
            {
                var LResponse = new NewSubscriber() { NewId = string.Empty };
                LResponse.Error.ErrorCode = Constants.Errors.EmailAlreadyRegistered.ErrorCode;
                LResponse.Error.ErrorDesc = Constants.Errors.EmailAlreadyRegistered.ErrorDesc;
                return LResponse;
            }

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

            var LResult = await FCosmosDbService.AddItem<SubscribersModel>(NewId, InsertNew);
            if (LResult == HttpStatusCode.Created)
            {
                return new NewSubscriber
                {
                    NewId = NewId 
                };
            }
            else 
            {
                return new NewSubscriber
                {
                    NewId = string.Empty,
                    Error = new Shared.Models.ErrorHandler 
                    { 
                        ErrorCode = Constants.Errors.UnableToPost.ErrorCode,
                        ErrorDesc = $"{Constants.Errors.UnableToPost.ErrorDesc} Returned status code: {LResult}."
                    }
                };
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
                LastUpdated = DateTime.Now
            };

            return await FCosmosDbService.UpdateItem<SubscribersModel>(PayLoad.Id, UpdatedSubscriber);
        
        }

        public async Task<HttpStatusCode> DeleteSubscriber(string Id)
        {
            return await FCosmosDbService.DeleteItem<SubscribersModel>(Id);
        }

    }

}
