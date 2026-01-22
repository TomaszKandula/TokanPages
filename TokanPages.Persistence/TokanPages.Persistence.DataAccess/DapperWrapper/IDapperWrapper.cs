namespace TokanPages.Persistence.DataAccess.DapperWrapper;

public interface IDapperWrapper
{
    Task Insert<T>(T entity, CancellationToken cancellationToken = default);
    Task Update<T>(T entity, CancellationToken cancellationToken = default);
    Task Delete<T>(T entity, CancellationToken cancellationToken = default);
}