using TokanPages.Backend.Domain.Entities;

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
    /// Returns list of newsletters that are active.
    /// </summary>
    /// <param name="isActive">If true, only active newsletters are returned.</param>
    /// <returns>Lif of active newsletters.</returns>
    Task<List<Newsletter>> GetNewsletters(bool isActive);

    /// <summary>
    /// Creates a new business inquiry entry in the database.
    /// </summary>
    /// <param name="jsonData">A stringify business message.</param>
    Task CreateBusinessInquiry(string jsonData);
}