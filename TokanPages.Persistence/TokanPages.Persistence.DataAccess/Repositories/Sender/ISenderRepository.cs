namespace TokanPages.Persistence.DataAccess.Repositories.Sender;

public interface ISenderRepository
{
    /// <summary>
    /// Creates a new business inquiry entry in the database.
    /// </summary>
    /// <param name="jsonData">A stringify business message.</param>
    Task CreateBusinessInquiry(string jsonData);
}