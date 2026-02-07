using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.DataAccess.Repositories.Sender.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Sender;

public interface ISenderRepository
{
    /// <summary>
    /// Returns newsletter entity by the given ID.
    /// </summary>
    /// <param name="id">Newsletter ID.</param>
    /// <returns>If found, returns an entity, otherwise null.</returns>
    Task<Newsletter?> GetNewsletter(Guid id);

    /// <summary>
    /// Returns newsletter entity by the given user email.
    /// </summary>
    /// <param name="email">User email address.</param>
    /// <returns>If found, returns an entity, otherwise null.</returns>
    Task<Newsletter?> GetNewsletter(string email);

    /// <summary>
    /// Returns list of newsletters that are active.
    /// </summary>
    /// <param name="isActive">If true, only active newsletters are returned.</param>
    /// <returns>List of active newsletters.</returns>
    Task<List<Newsletter>> GetNewsletters(bool isActive);

    /// <summary>
    /// Create a newsletter entry in the database.
    /// </summary>
    /// <param name="email">An email address.</param>
    /// <param name="id">An optional ID that can be generated externally.</param>
    Task CreateNewsletter(string email, Guid? id = null);

    /// <summary>
    /// Updates existing newsletter details by the given input data.
    /// </summary>
    /// <param name="data">Newsletter details to be updated.</param>
    Task UpdateNewsletter(UpdateNewsletterDto data);

    /// <summary>
    /// Creates a new business inquiry entry in the database.
    /// </summary>
    /// <param name="jsonData">A stringify business message.</param>
    Task CreateBusinessInquiry(string jsonData);
}