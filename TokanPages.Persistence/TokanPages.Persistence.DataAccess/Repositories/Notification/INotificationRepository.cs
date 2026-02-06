using TokanPages.Backend.Domain.Entities.Notifications;
using TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification;

public interface INotificationRepository
{
    /// <summary>
    /// Returns push notification details.
    /// </summary>
    /// <param name="pnsHandle">Unique notification handle.</param>
    /// <returns>If found, returns notification details, otherwise null.</returns>
    Task<PushNotification?> GetPushNotification(string pnsHandle);

    /// <summary>
    /// Creates push notification details entry in the repository.
    /// </summary>
    /// <param name="data">Notification details.</param>
    Task CreatePushNotificationEntry(PushNotificationDto data);

    /// <summary>
    /// Updates existing push notification details in the repository.
    /// </summary>
    /// <param name="data">Notification details.</param>
    Task UpdatePushNotificationEntry(PushNotificationDto data);

    /// <summary>
    /// Deletes existing push notification entry from the repository.
    /// </summary>
    /// <param name="key">Push notification table primary key.</param>
    Task DeletePushNotification(Guid key);

    /// <summary>
    /// Deletes existing push notification entries from the repository.
    /// </summary>
    /// <param name="keys">List of PushNotification primary keys.</param>
    /// <returns></returns>
    Task DeletePushNotifications(List<Guid> keys);

    /// <summary>
    /// Returns push notification tags by an installation ID.
    /// </summary>
    /// <param name="installationId">Installation ID.</param>
    /// <returns>List of tags.</returns>
    Task<List<PushNotificationTag>> GetPushNotificationTags(Guid installationId);

    /// <summary>
    /// Creates push notification tag entry in the repository.
    /// </summary>
    /// <param name="data">Tag details.</param>
    Task CreatePushNotificationTag(PushNotificationTagDto data);

    /// <summary>
    /// Deletes push notification tag by given ID.
    /// </summary>
    /// <param name="pushNotificationId">A foreign key linked to the primary key of the related PushNotification entity.</param>
    Task DeletePushNotificationTag(Guid pushNotificationId);

    /// <summary>
    /// Deletes push notification tags by given IDs.
    /// </summary>
    /// <param name="keys">List of PushNotificationTag primary keys.</param>
    Task DeletePushNotificationTags(List<Guid> keys);
}