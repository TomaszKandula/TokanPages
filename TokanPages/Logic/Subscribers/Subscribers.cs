using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Database;
using TokanPages.Backend.Shared.Models;
using TokanPages.Logic.Subscribers.Model;
using TokanPages.Controllers.Subscribers.Model;
using SubscribersModel = TokanPages.Backend.Domain.Entities.Subscribers;

namespace TokanPages.Logic.Subscribers
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

            var LItems = await FCosmosDbService.GetItems<SubscribersModel>($"select * from {typeof(SubscribersModel).Name}");
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

        public async Task<SubscriberItem> GetSingleSubscriber(Guid Id)
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

            var LModelName = typeof(SubscribersModel).Name;
            var LModelSymbol = LModelName[0..1].ToLower();
            var LQuery = $"select * from {LModelName} {LModelSymbol} where {LModelSymbol}.email = \"{PayLoad.Email}\"";
            var LItems = await FCosmosDbService.GetItems<SubscribersModel>(LQuery);
            if (LItems.Any())
            {
                var LResponse = new NewSubscriber() { NewId = Guid.Empty };
                LResponse.Error.ErrorCode = Constants.Errors.EmailAlreadyRegistered.ErrorCode;
                LResponse.Error.ErrorDesc = Constants.Errors.EmailAlreadyRegistered.ErrorDesc;
                return LResponse;
            }

            var NewId = Guid.NewGuid();
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
                    NewId = Guid.Empty,
                    Error = new ErrorHandler 
                    { 
                        ErrorCode = Constants.Errors.UnableToPost.ErrorCode,
                        ErrorDesc = $"{Constants.Errors.UnableToPost.ErrorDesc} Returned status code: {LResult}."
                    }
                };
            }
        
        }

        public async Task<HttpStatusCode> UpdateSubscriber(SubscriberRequest PayLoad)
        {

            var LResult = await FCosmosDbService.IsItemExists<SubscribersModel>(PayLoad.Id);
            if (LResult != HttpStatusCode.OK) return LResult;

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

        public async Task<HttpStatusCode> DeleteSubscriber(Guid Id)
        {
            var LResult = await FCosmosDbService.IsItemExists<SubscribersModel>(Id);
            if (LResult != HttpStatusCode.OK) return LResult;
            return await FCosmosDbService.DeleteItem<SubscribersModel>(Id);
        }

    }

}
