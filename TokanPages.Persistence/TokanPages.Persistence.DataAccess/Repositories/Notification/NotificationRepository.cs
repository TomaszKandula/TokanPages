using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities.Notifications;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification;

public class NotificationRepository : RepositoryBase, INotificationRepository
{
    private readonly IDateTimeService _dateTimeService;
    
    public NotificationRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    public async Task<PushNotification?> GetPushNotification(string pnsHandle)
    {
        var filterBy = new { Handle = pnsHandle };
        var data = await DbOperations.Retrieve<PushNotification>(filterBy);
        return data.SingleOrDefault();
    }

    public async Task CreatePushNotificationEntry(PushNotificationDto data)
    {
        var entity = new PushNotification
        {
            Id = data.Id,
            Handle = data.Handle,
            Platform = data.Platform,
            Description = "First PNS handle installation",
            CreatedAt = _dateTimeService.Now,
            CreatedBy = Guid.Empty,
            IsVerified = data.IsVerified,
            RegistrationId = data.RegistrationId
        };

        await DbOperations.Insert(entity);
    }

    public async Task UpdatePushNotificationEntry(PushNotificationDto data)
    {
        var filterBy = new { Id = data.Id };
        var updateBy = new
        {
            ModifiedAt = _dateTimeService.Now,
            ModifiedBy = data.ModifiedBy,
            Description = "PNS handle installation updated",
            IsVerified = data.IsVerified,
            RegistrationId = data.RegistrationId
        };

        await DbOperations.Update<PushNotification>(updateBy, filterBy);
    }

    public async Task<List<PushNotificationTag>> GetPushNotificationTags(Guid installationId)
    {
        var filterBy = new { PushNotificationId = installationId };
        var data = await DbOperations.Retrieve<PushNotificationTag>(filterBy);
        return data.ToList();
    }

    public async Task CreatePushNotificationTag(PushNotificationTagDto data)
    {
        var entity = new PushNotificationTag
        {
            Id = Guid.NewGuid(),
            PushNotificationId = data.PushNotificationId,
            Tag = data.Tag,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = Guid.Empty
        };

        await DbOperations.Insert(entity);
    }

    public async Task DeletePushNotificationTag(List<object> ids)
    {
        var uids = new HashSet<object>(ids);
        await DbOperations.Delete<PushNotificationTag>(uids);
    }
}