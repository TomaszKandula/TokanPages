namespace TokanPages.Persistence.DataAccess.Abstractions;

public interface IDbOperations
{
    Task<IEnumerable<T>> Retrieve<T>(object filterBy);

    Task Insert<T>(T entity, CancellationToken cancellationToken = default);

    Task Update<T>(object updateBy, object filterBy, CancellationToken cancellationToken = default);

    Task Delete<T>(object deleteBy, CancellationToken cancellationToken = default);
}