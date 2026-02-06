namespace TokanPages.Persistence.DataAccess.Abstractions;

public interface IDbOperations
{
    Task<IEnumerable<T>> Retrieve<T>(object? filterBy = null, object? orderBy = null);

    Task Insert<T>(T entity);

    Task Insert<T>(List<T> entities);

    Task Update<T>(object updateBy, object filterBy);

    Task Delete<T>(object deleteBy);

    Task Delete<T>(HashSet<object> ids);
}