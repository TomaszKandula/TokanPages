using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Persistence.DataAccess.Helpers;

public class DapperWrapper : IDapperWrapper
{
    private readonly ILoggerService _loggerService;

    private readonly ISqlGenerator _sqlGenerator;

    private readonly IConfiguration _configuration;

    private string ConnectionString => _configuration.GetValue<string>("Db_DatabaseContext") ?? "";

    public DapperWrapper(ILoggerService loggerService, IConfiguration configuration, ISqlGenerator sqlGenerator)
    {
        _loggerService = loggerService;
        _configuration = configuration;
        _sqlGenerator = sqlGenerator;
    }

    public async Task Insert<T>(T entity, CancellationToken cancellationToken = default)
    {
        var sql = _sqlGenerator.GenerateInsertStatement(entity);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    public async Task Update<T>(T entity, CancellationToken cancellationToken = default)
    {
        var sql = _sqlGenerator.GenerateUpdateStatement(entity);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    public async Task Delete<T>(T entity, CancellationToken cancellationToken = default)
    {
        var sql = _sqlGenerator.GenerateDeleteStatement(entity);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    private async Task ExecuteSqlTransaction(string sql, CancellationToken cancellationToken = default)
    {
        await using var db = new SqlConnection(ConnectionString);
        var transaction = await db.BeginTransactionAsync(cancellationToken);
        try
        {
            await db.ExecuteAsync(sql, transaction);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            _loggerService.LogFatal($"{exception.Message} {exception.InnerException?.Message}");
            throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
        }
    }
}