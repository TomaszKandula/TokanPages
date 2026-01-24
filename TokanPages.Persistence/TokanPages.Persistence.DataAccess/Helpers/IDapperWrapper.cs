namespace TokanPages.Persistence.DataAccess.Helpers;

public interface IDapperWrapper
{
    Task<IEnumerable<T>> Retrieve<T>(object filterBy);

    Task Insert<T>(T entity, CancellationToken cancellationToken = default);

    Task Update<T>(object updateBy, CancellationToken cancellationToken = default);

    Task Delete<T>(object deleteBy, CancellationToken cancellationToken = default);
}