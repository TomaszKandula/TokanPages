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
    public async Task CreatePushNotification(PushNotificationDto data)
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
    public async Task UpdatePushNotification(PushNotificationDto data)
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
    public async Task DeletePushNotificationByPk(Guid primaryKey)
    {
        var deleteBy = new { Id = primaryKey };
        await DbOperations.Delete<PushNotification>(deleteBy);
    }

    /// <inheritdoc/>
    public async Task DeletePushNotificationsByPks(List<object> primaryKeys)
    {
        var ids = new HashSet<object>(primaryKeys);
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
    public async Task CreatePushNotificationTags(List<PushNotificationTagDto> data)
    {
         var entities = new List<PushNotificationTag>();
         foreach (var item in data)
         {
             var entry = new PushNotificationTag
             {
                 Id = Guid.NewGuid(),
                 PushNotificationId = item.PushNotificationId,
                 Tag = item.Tag,
                 CreatedAt = _dateTimeService.Now,
                 CreatedBy = Guid.Empty
             };

             entities.Add(entry);
         }

         await DbOperations.Insert(entities);
    }

    /// <inheritdoc/>
    public async Task DeletePushNotificationTagsByFk(Guid pushNotificationId)
    {
        var deleteBy = new { PushNotificationId = pushNotificationId };
        await DbOperations.Delete<PushNotificationTag>(deleteBy);
    }

    /// <inheritdoc/>
    public async Task DeletePushNotificationTagsByPks(List<object> keys)
    {
        var uids = new HashSet<object>(keys);
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
                RegistrationId = item.RegistrationId,
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

    /// <inheritdoc/>
    public async Task<WebNotification?> GetWebNotificationById(Guid id)
    {
        var filterBy = new { Id = id };
        var data = await DbOperations.Retrieve<WebNotification>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task CreateWebNotification(string value, Guid? primaryKey = null)
    {
        var entity = new WebNotification
        {
            Id = primaryKey ?? Guid.NewGuid(),
            Value = value
        };

        await DbOperations.Insert(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteWebNotificationById(Guid id)
    {
        var deleteBy = new { Id = id };
        await DbOperations.Delete<WebNotification>(deleteBy);
    }
}