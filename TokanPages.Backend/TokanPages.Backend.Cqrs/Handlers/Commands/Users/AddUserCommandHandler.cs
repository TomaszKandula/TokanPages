﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Services.CipheringService;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{   
    public class AddUserCommandHandler : TemplateHandler<AddUserCommand, Guid>
    {
        private const int CIPHER_LOG_ROUNDS = 12;
        
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IDateTimeService FDateTimeService;

        private readonly ICipheringService FCipheringService;

        public AddUserCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService, ICipheringService ACipheringService) 
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
            FCipheringService = ACipheringService;
        }

        public override async Task<Guid> Handle(AddUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LEmailCollection = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);
            
            if (LEmailCollection.Count == 1) 
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);
            
            var LNewUser = new Domain.Entities.Users
            { 
                EmailAddress = ARequest.EmailAddress,
                IsActivated = true,
                UserAlias = ARequest.UserAlias,
                FirstName = ARequest.FirstName,
                LastName = ARequest.LastName,
                Registered = FDateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FCipheringService.GetHashedPassword(ARequest.Password, FCipheringService.GenerateSalt(CIPHER_LOG_ROUNDS)) 
            };

            await FDatabaseContext.Users.AddAsync(LNewUser, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(LNewUser.Id);
        }
    }
}
