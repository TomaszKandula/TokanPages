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
    public async Task CreateBusinessInquiries(string jsonData)
    {
        var entity = new BusinessInquiry
        {
            JsonData =  jsonData,
            CreatedBy = Guid.Empty,
            CreatedAt = _dateTimeService.Now,
        };

        await DbOperations.Insert(entity);
    }
}