using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.DataAccess.Repositories.Sender.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Sender;

public interface ISenderRepository
{
    Task<Newsletter?> GetNewsletter(Guid id);

    Task<Newsletter?> GetNewsletter(string email);

    Task<List<Newsletter>> GetNewsletters(bool isActive);

    Task CreateNewsletter(string email, Guid? id = null);

    Task UpdateNewsletter(UpdateNewsletterDto data);

    Task RemoveNewsletter(Guid id);
    
    Task CreateBusinessInquiry(string jsonData);
}