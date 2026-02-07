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
    Task CreatePushNotification(PushNotificationDto data);

    /// <summary>
    /// Updates existing push notification details in the repository.
    /// </summary>
    /// <param name="data">Notification details.</param>
    Task UpdatePushNotification(PushNotificationDto data);

    /// <summary>
    /// Deletes existing push notification entry from the repository.
    /// </summary>
    /// <param name="primaryKey">Push notification table primary key.</param>
    Task DeletePushNotificationByPk(Guid primaryKey);

    /// <summary>
    /// Deletes existing push notification entries from the repository.
    /// </summary>
    /// <param name="primaryKeys">Push notification table primary keys.</param>
    /// <returns></returns>
    Task DeletePushNotificationsByPks(List<object> primaryKeys);

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
    /// Creates push notification tags in the repository.
    /// </summary>
    /// <param name="data">List of tags details to be saved.</param>
    Task CreatePushNotificationTags(List<PushNotificationTagDto> data);

    /// <summary>
    /// Deletes push notification tags by a given ID (foreign key).
    /// </summary>
    /// <param name="pushNotificationId">A foreign key linked to the primary key of the related 'PushNotification' entity.</param>
    Task DeletePushNotificationTagsByFk(Guid pushNotificationId);

    /// <summary>
    /// Deletes push notification tags by given IDs (primary keys).
    /// </summary>
    /// <param name="keys">List of primary keys of 'PushNotificationTag' entity.</param>
    Task DeletePushNotificationTagsByPks(List<object> keys);

    /// <summary>
    /// Creates push notification log entries in the repository.
    /// </summary>
    /// <param name="data">List of log details.</param>
    Task CreatePushNotificationLogs(List<PushNotificationLogDto> data);

    /// <summary>
    /// Returns web notification entity by a given status ID.
    /// </summary>
    /// <remarks>
    /// Status ID is a Primary Key of the given entity.
    /// </remarks>
    /// <param name="statusId">Status ID.</param>
    /// <returns>An entity if exists, otherwise null.</returns>
    Task<WebNotification?> GetWebNotificationByStatusId(Guid statusId);

    /// <summary>
    /// Creates web notification entry in the repository.
    /// </summary>
    /// <remarks>
    /// The primary key is optional. If not passed, then it will be created.
    /// </remarks>
    /// <param name="value">Stringify data to be saved.</param>
    /// <param name="primaryKey">Optional primary key of the entity.</param>
    Task CreateWebNotification(string value, Guid? primaryKey = null);

    /// <summary>
    /// Deletes web notification entry for a given ID.
    /// </summary>
    /// <remarks>
    /// ID is a Primary Key of the given entity.
    /// </remarks>
    /// <param name="id">A mandatory ID used to delete a database entry.</param>
    Task DeleteWebNotificationById(Guid id);
}