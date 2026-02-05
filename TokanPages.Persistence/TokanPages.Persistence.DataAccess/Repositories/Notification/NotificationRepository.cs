using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification;

public class NotificationRepository : RepositoryBase, INotificationRepository
{
    private readonly IDateTimeService _dateTimeService;
    
    public NotificationRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;
}