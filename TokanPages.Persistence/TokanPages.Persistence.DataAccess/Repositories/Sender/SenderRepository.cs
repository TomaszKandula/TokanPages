using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.DataAccess.Abstractions;

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