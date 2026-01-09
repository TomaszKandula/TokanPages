using TokanPages.Backend.Application.Users.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Users cache contract
/// </summary>
public interface IUsersCache
{
    /// <summary>
    /// Returns registered users
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object list</returns>
    Task<List<GetUsersQueryResult>> GetUsers(bool noCache = false);

    /// <summary>
    /// Returns single user data
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetUserQueryResult> GetUser(Guid id, bool noCache = false);

    /// <summary>
    /// Returns list of user files.
    /// </summary>
    /// <param name="isVideoFile">Tells if should search for videos</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>List of blobs</returns>
    Task<GetUserFileListResult> GetUserFileList(bool isVideoFile, bool noCache = false);

    /// <summary>
    /// Returns user notes for given user ID (taken from authorization header).
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object with list of notes.</returns>
    Task<GetUserNotesQueryResult> GetUserNotes(bool noCache = false);

    /// <summary>
    /// Returns user note for given ID.
    /// </summary>
    /// <param name="id">User Note ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    Task<GetUserNoteQueryResult> GetUserNote(Guid id, bool noCache = false);
}