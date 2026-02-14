using TokanPages.Backend.Domain.Entities.Notifications;
using TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification;

public interface INotificationRepository
{
    Task<PushNotification?> GetPushNotification(string pnsHandle);

    Task CreatePushNotification(PushNotificationDto data);

    Task UpdatePushNotification(PushNotificationDto data);

    Task DeletePushNotificationById(Guid id);

    Task DeletePushNotificationsByIds(List<object> ids);

    Task<List<PushNotificationTag>> GetPushNotificationTags(Guid installationId);

    Task CreatePushNotificationTag(PushNotificationTagDto data);

    Task CreatePushNotificationTags(List<PushNotificationTagDto> data);

    Task DeletePushNotificationTagsById(Guid id);

    Task DeletePushNotificationTagsByIds(List<object> ids);

    Task CreatePushNotificationLogs(List<PushNotificationLogDto> data);

    Task<WebNotification?> GetWebNotificationById(Guid id);

    Task CreateWebNotification(string value, Guid? id = null);

    Task UpdateWebNotification(Guid id, string value);

    Task DeleteWebNotificationById(Guid id);
}