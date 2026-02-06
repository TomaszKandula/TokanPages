using TokanPages.Backend.Domain.Entities.Notifications;
using TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification;

public interface INotificationRepository
{
    Task<PushNotification?> GetPushNotification(string pnsHandle);

    Task CreatePushNotificationEntry(PushNotificationDto data);

    Task UpdatePushNotificationEntry(PushNotificationDto data);

    Task<List<PushNotificationTag>> GetPushNotificationTags(Guid installationId);

    Task CreatePushNotificationTag(PushNotificationTagDto data);

    Task DeletePushNotificationTag(List<object> ids);
}