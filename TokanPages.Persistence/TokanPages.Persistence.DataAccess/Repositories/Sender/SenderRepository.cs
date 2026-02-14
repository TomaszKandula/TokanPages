using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Sender.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Sender;

public class SenderRepository : RepositoryBase, ISenderRepository
{
    private readonly IDateTimeService _dateTimeService;
    
    public SenderRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    public async Task<Newsletter?> GetNewsletter(Guid id)
    {
        var filterBy = new { Id = id };
        var data = await DbOperations.Retrieve<Newsletter>(filterBy);
        return data.SingleOrDefault();
    }

    public async Task<Newsletter?> GetNewsletter(string email)
    {
        var filterBy = new { Email = email };
        var data = await DbOperations.Retrieve<Newsletter>(filterBy);
        return data.SingleOrDefault();
    }

    public async Task<List<Newsletter>> GetNewsletters(bool isActive)
    {
        var filterBy = new { IsActive = isActive };
        var data = await DbOperations.Retrieve<Newsletter>(filterBy);
        return data.ToList();
    }

    public async Task CreateNewsletter(string email, Guid? id = null)
    {
        var entity = new Newsletter
        {
            Id = id ?? Guid.NewGuid(),
            Email = email,
            Count = 0,
            IsActivated = true,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = Guid.Empty
        };

        await DbOperations.Insert(entity);
    }

    public async Task UpdateNewsletter(UpdateNewsletterDto data)
    {
        var filterBy = new
        {
            Id = data.Id
        };

        var updateBy = new
        {
            Email = data.Email,
            IsActivated = data.IsActivated,
            Count = data.Count,
            ModifiedAt = _dateTimeService.Now
        };

        await DbOperations.Update<Newsletter>(updateBy, filterBy);
    }

    public async Task RemoveNewsletter(Guid id)
    {
        var deleteBy = new { Id = id };
        await DbOperations.Delete<Newsletter>(deleteBy);
    }

    public async Task CreateBusinessInquiry(string jsonData)
    {
        var entity = new BusinessInquiry
        {
            Id = Guid.NewGuid(),
            JsonData =  jsonData,
            CreatedBy = Guid.Empty,
            CreatedAt = _dateTimeService.Now,
        };

        await DbOperations.Insert(entity);
    }
}