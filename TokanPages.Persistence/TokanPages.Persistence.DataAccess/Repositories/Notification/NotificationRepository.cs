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

    /// <inheritdoc/>
    public async Task<PushNotification?> GetPushNotification(string pnsHandle)
    {
        var filterBy = new { Handle = pnsHandle };
        var data = await DbOperations.Retrieve<PushNotification>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task DeletePushNotification(Guid key)
    {
        var deleteBy = new { Id = key };
        await DbOperations.Delete<PushNotification>(deleteBy);
    }

    /// <inheritdoc/>
    public async Task DeletePushNotifications(List<Guid> keys)
    {
        var ids = new HashSet<Guid>(keys);
        await DbOperations.Delete<PushNotification>(ids);
    }

    /// <inheritdoc/>
    public async Task<List<PushNotificationTag>> GetPushNotificationTags(Guid installationId)
    {
        var filterBy = new { PushNotificationId = installationId };
        var data = await DbOperations.Retrieve<PushNotificationTag>(filterBy);
        return data.ToList();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task DeletePushNotificationTag(Guid pushNotificationId)
    {
        var deleteBy = new { PushNotificationId = pushNotificationId };
        await DbOperations.Delete<PushNotificationTag>(deleteBy);
    }

    /// <inheritdoc/>
    public async Task DeletePushNotificationTags(List<Guid> keys)
    {
        var uids = new HashSet<Guid>(keys);
        await DbOperations.Delete<PushNotificationTag>(uids);
    }

    /// <inheritdoc/>
    public async Task CreatePushNotificationLogs(List<PushNotificationLogDto> data)
    {
        var entities = new List<PushNotificationLog>();
        foreach (var item in data)
        {
            var entry = new PushNotificationLog
            {
                Id = Guid.NewGuid(),
                RegistrationId =  item.RegistrationId,
                Handle = item.Handle,
                Platform = item.Platform,
                Payload = item.Payload,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = Guid.Empty
            };

            entities.Add(entry);
        }

        await DbOperations.Insert(entities);
    }
}