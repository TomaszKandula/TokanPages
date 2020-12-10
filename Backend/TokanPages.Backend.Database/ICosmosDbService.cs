﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace TokanPages.Backend.Database
{

    public interface ICosmosDbService
    {
        void InitContainer<T>();
        void InitContainer(Container YourContainer);
        Task<HttpStatusCode> CreateDatabase(string ADatabaseName);
        Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, Guid AId);
        Task<HttpStatusCode> IsItemExists<T>(Guid Id) where T : class;
        Task<T> GetItem<T>(Guid AId) where T : class;
        Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class;
        Task<HttpStatusCode> AddItem<T>(Guid AId, T AItem);
        Task<HttpStatusCode> UpdateItem<T>(Guid AId, T AItem);
        Task<HttpStatusCode> DeleteItem<T>(Guid AId);
    }

}
