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

    /// <inheritdoc/>
    public async Task<Newsletter?> GetNewsletter(Guid id)
    {
        var filterBy = new { Id = id };
        var data = await DbOperations.Retrieve<Newsletter>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<Newsletter?> GetNewsletter(string email)
    {
        var filterBy = new { Email = email };
        var data = await DbOperations.Retrieve<Newsletter>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<List<Newsletter>> GetNewsletters(bool isActive)
    {
        var filterBy = new { IsActive = isActive };
        var data = await DbOperations.Retrieve<Newsletter>(filterBy);
        return data.ToList();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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
            ModifiedBy = data.ModifiedBy,
            ModifiedAt = _dateTimeService.Now
        };

        await DbOperations.Update<Newsletter>(updateBy, filterBy);
    }

    /// <inheritdoc/>
    public async Task RemoveNewsletter(Guid id)
    {
        var deleteBy = new { Id = id };
        await DbOperations.Delete<Newsletter>(deleteBy);
    }

    /// <inheritdoc/>
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