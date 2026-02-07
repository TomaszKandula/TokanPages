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
    /// Creates push notification details entry in the database.
    /// </summary>
    /// <param name="data">Notification details.</param>
    Task CreatePushNotification(PushNotificationDto data);

    /// <summary>
    /// Updates existing push notification details in the database.
    /// </summary>
    /// <param name="data">Notification details.</param>
    Task UpdatePushNotification(PushNotificationDto data);

    /// <summary>
    /// Deletes existing push notification entry by the given ID.
    /// </summary>
    /// <remarks>
    /// ID is the primary key of the given entity.
    /// </remarks>
    /// <param name="primaryKey">A mandatory ID.</param>
    Task DeletePushNotificationByPk(Guid primaryKey);

    /// <summary>
    /// Deletes existing push notification entries by the given IDs.
    /// </summary>
    /// <remarks>
    /// ID is the primary key of the given entity.
    /// </remarks>
    /// <param name="primaryKeys">A list of primary keys.</param>
    Task DeletePushNotificationsByPks(List<object> primaryKeys);

    /// <summary>
    /// Returns push notification tags by the installation ID.
    /// </summary>
    /// <param name="installationId">A mandatory installation ID.</param>
    /// <returns>List of tags.</returns>
    Task<List<PushNotificationTag>> GetPushNotificationTags(Guid installationId);

    /// <summary>
    /// Creates push notification tag entry in the database.
    /// </summary>
    /// <param name="data">Tag details.</param>
    Task CreatePushNotificationTag(PushNotificationTagDto data);

    /// <summary>
    /// Creates push notification tags in the database.
    /// </summary>
    /// <param name="data">List of tags details to be saved.</param>
    Task CreatePushNotificationTags(List<PushNotificationTagDto> data);

    /// <summary>
    /// Deletes push notification tags by the given ID.
    /// </summary>
    /// <remarks>
    /// ID is the foreign key linked to the primary key of the related 'PushNotification' entity.
    /// </remarks>
    /// <param name="id">A mandatory ID used to update an entry.</param>
    Task DeletePushNotificationTagsById(Guid id);

    /// <summary>
    /// Deletes push notification tags by given IDs (primary keys).
    /// </summary>
    /// <param name="ids">List of primary keys of 'PushNotificationTag' entity.</param>
    Task DeletePushNotificationTagsByIds(List<object> ids);

    /// <summary>
    /// Creates push notification log entries in the database.
    /// </summary>
    /// <param name="data">List of log details.</param>
    Task CreatePushNotificationLogs(List<PushNotificationLogDto> data);

    /// <summary>
    /// Returns web notification entity by the given ID.
    /// </summary>
    /// <remarks>
    /// ID is the primary key of the given entity.
    /// </remarks>
    /// <param name="id">A mandatory ID used to update an entry.</param>
    /// <returns>An entity if exists, otherwise null.</returns>
    Task<WebNotification?> GetWebNotificationById(Guid id);

    /// <summary>
    /// Creates web notification entry in the repository.
    /// </summary>
    /// <remarks>
    /// The primary key is optional. If not passed, then it will be created.
    /// </remarks>
    /// <param name="value">Stringify data to be saved.</param>
    /// <param name="primaryKey">An optional primary key of the entity.</param>
    Task CreateWebNotification(string value, Guid? primaryKey = null);

    /// <summary>
    /// Deletes web notification entry for the given ID.
    /// </summary>
    /// <remarks>
    /// ID is the primary key of the given entity.
    /// </remarks>
    /// <param name="id">A mandatory ID used to delete an entry.</param>
    Task DeleteWebNotificationById(Guid id);
}