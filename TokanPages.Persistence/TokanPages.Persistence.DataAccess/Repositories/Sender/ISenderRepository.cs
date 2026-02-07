namespace TokanPages.Persistence.DataAccess.Repositories.Sender;

public interface ISenderRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jsonData"></param>
    /// <returns></returns>
    Task CreateBusinessInquiries(string jsonData);
}