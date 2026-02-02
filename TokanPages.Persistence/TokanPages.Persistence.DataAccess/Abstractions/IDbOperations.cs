namespace TokanPages.Persistence.DataAccess.Abstractions;

public interface IDbOperations
{
    Task<IEnumerable<T>> Retrieve<T>(object? filterBy = null, object? orderBy = null);

    Task Insert<T>(T entity);

    Task Update<T>(object updateBy, object filterBy);

    Task Delete<T>(object deleteBy);
}