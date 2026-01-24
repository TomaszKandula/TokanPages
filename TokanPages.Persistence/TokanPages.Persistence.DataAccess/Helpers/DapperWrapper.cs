using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Persistence.DataAccess.Helpers;

public class DapperWrapper : IDapperWrapper
{
    private readonly ILoggerService _loggerService;

    private readonly ISqlGenerator _sqlGenerator;

    private readonly IHostEnvironment  _environment;

    private readonly IConfiguration _configuration;//TODO: replace w/IOption

    private string ConnectionString => _configuration.GetValue<string>("Db_DatabaseContext") ?? "";

    public DapperWrapper(ILoggerService loggerService, IConfiguration configuration, ISqlGenerator sqlGenerator, IHostEnvironment environment)
    {
        _loggerService = loggerService;
        _configuration = configuration;
        _sqlGenerator = sqlGenerator;
        _environment = environment;
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
        var watch = new Stopwatch();
        try
        {
            watch.Start();
            await db.ExecuteAsync(sql, transaction);
            await transaction.CommitAsync(cancellationToken);
            watch.Stop();

            if (_environment.IsDevelopment() || _environment.IsStaging())
            {
                _loggerService.LogDebug($"SQL Transaction:\n{sql}\nExecuted within {watch.ElapsedMilliseconds} ms.");
            }
            else
            {
                _loggerService.LogInformation($"SQL Transaction executed within {watch.ElapsedMilliseconds} ms.");
            }
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            _loggerService.LogFatal($"{exception.Message} {exception.InnerException?.Message}");
            throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
        }
    }
}