﻿using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Logic.Mailer;
using TokanPages.BackEnd.Logic.MailChecker;
using BackEnd.UnitTests.Mocks.SendGrid;
using BackEnd.UnitTests.Mocks.AzureStorage;

namespace BackEnd.UnitTests
{

    public class LogicTest_MailChecker
    {

        [Fact]
        public void Should_CheckAddressFormat()
        {

            // Arrange
            var FakeSendGridService = new FakeSendGridService();
            var FakeAzureStorageService = new FakeAzureStorageService();
            var FMailer = new Mailer(FakeSendGridService, FakeAzureStorageService);
            var FMailChecker = new MailChecker();

            var LTestEmails = new List<string>()
            {
                "this is not an email",
                "tom@tomkandula.com",
                "contact@tomkandula.com",
                "tomasz.kandula@gmail.com"
            };

            // Act
            var LResults = FMailChecker.IsAddressCorrect(LTestEmails);

            // Assert
            LResults.Should().HaveCount(4);
            LResults.Select(Addresses => Addresses.IsValid).Contains(false).Should().BeTrue();

        }

        [Fact]
        public async Task Should_InvalidateDomain()
        {

            // Arrange
            var FMailChecker = new MailChecker();
            var LTestEmail = "john@fakedomain.oi";

            // Act
            var LResult = await FMailChecker.IsDomainCorrect(LTestEmail);

            // Assert
            LResult.Should().Be(false);

        }

    }

}
