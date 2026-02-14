using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities.Notifications;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
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

    public async Task UpdatePushNotification(PushNotificationDto data)
    {
        var updateBy = new
        {
            ModifiedAt = _dateTimeService.Now,
            Description = "PNS handle installation updated",
            IsVerified = data.IsVerified,
            RegistrationId = data.RegistrationId
        };

        var filterBy = new
        {
            Id = data.Id
        };

        await DbOperations.Update<PushNotification>(updateBy, filterBy);
    }

    public async Task DeletePushNotificationById(Guid id)
    {
        var deleteBy = new { Id = id };
        await DbOperations.Delete<PushNotification>(deleteBy);
    }

    public async Task DeletePushNotificationsByIds(List<object> ids)
    {
        var uids = new HashSet<object>(ids);
        await DbOperations.Delete<PushNotification>(uids);
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

    public async Task DeletePushNotificationTagsById(Guid id)
    {
        var deleteBy = new { PushNotificationId = id };
        await DbOperations.Delete<PushNotificationTag>(deleteBy);
    }

    public async Task DeletePushNotificationTagsByIds(List<object> ids)
    {
        var uids = new HashSet<object>(ids);
        await DbOperations.Delete<PushNotificationTag>(uids);
    }

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

    public async Task<WebNotification?> GetWebNotificationById(Guid id)
    {
        var filterBy = new { Id = id };
        var data = await DbOperations.Retrieve<WebNotification>(filterBy);
        return data.SingleOrDefault();
    }

    public async Task CreateWebNotification(string value, Guid? id = null)
    {
        var entity = new WebNotification
        {
            Id = id ?? Guid.NewGuid(),
            Value = value
        };

        await DbOperations.Insert(entity);
    }

    public async Task UpdateWebNotification(Guid id, string value)
    {
        var updateBy = new { Value = value };
        var filterBy = new { Id = id };
        await DbOperations.Update<WebNotification>(updateBy, filterBy);
    }

    public async Task DeleteWebNotificationById(Guid id)
    {
        var deleteBy = new { Id = id };
        await DbOperations.Delete<WebNotification>(deleteBy);
    }
}