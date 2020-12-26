﻿using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;

namespace Backend.IntegrationTests.Handlers.Users
{

    public class AddUserCommandHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public AddUserCommandHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        //...

    }

}
