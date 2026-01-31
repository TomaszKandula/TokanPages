using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Persistence.DataAccess.Helpers;

public class DbOperations : IDbOperations
{
    private readonly ILoggerService _loggerService;

    private readonly ISqlGenerator _sqlGenerator;

    private readonly IHostEnvironment  _environment;

    private readonly AppSettings _appSettings;

    public DbOperations(ILoggerService loggerService, IOptions<AppSettings> options, ISqlGenerator sqlGenerator, IHostEnvironment environment)
    {
        _loggerService = loggerService;
        _appSettings = options.Value;
        _sqlGenerator = sqlGenerator;
        _environment = environment;
    }

    public async Task<IEnumerable<T>> Retrieve<T>(object filterBy)
    {
        await using var connection = new SqlConnection(_appSettings.DbDatabaseContext);
        var sql = _sqlGenerator.GenerateQueryStatement<T>(filterBy);
        var watch = new Stopwatch();
        try
        {
            watch.Start();
            var result = await connection.QueryAsync<T>(sql);
            watch.Stop();

            if (_environment.IsProduction())
            {
                _loggerService.LogInformation($"SQL statement executed within {watch.ElapsedMilliseconds} ms.");
            }
            else
            {
                _loggerService.LogDebug($"SQL statement:\n{sql}\nExecuted within {watch.ElapsedMilliseconds} ms.");
            }

            return result;            
        }
        catch (Exception exception)
        {
            _loggerService.LogFatal($"{exception.Message}\n{exception.InnerException?.Message}");
            throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
        }
    }

    public async Task Insert<T>(T entity, CancellationToken cancellationToken = default)
    {
        var sql = _sqlGenerator.GenerateInsertStatement(entity);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    public async Task Update<T>(object updateBy, object filterBy, CancellationToken cancellationToken = default)
    {
        var sql = _sqlGenerator.GenerateUpdateStatement<T>(updateBy, filterBy);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    public async Task Delete<T>(object deleteBy, CancellationToken cancellationToken = default)
    {
        var sql = _sqlGenerator.GenerateDeleteStatement<T>(deleteBy);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    private async Task ExecuteSqlTransaction(string sql, CancellationToken cancellationToken = default)
    {
        var watch = new Stopwatch();
        await using var connection = new SqlConnection(_appSettings.DbDatabaseContext);
        try
        {
            watch.Start();
            var query = TransactionTemplate.Replace("{QUERY}", sql);
            await connection.ExecuteAsync(query, cancellationToken);
            watch.Stop();

            if (_environment.IsProduction())
            {
                _loggerService.LogInformation($"SQL Transaction executed within {watch.ElapsedMilliseconds} ms.");
            }
            else
            {
                _loggerService.LogDebug($"SQL Transaction:\n{query}\nExecuted within {watch.ElapsedMilliseconds} ms.");
            }
        }
        catch (Exception exception)
        {
            _loggerService.LogFatal($"{exception.Message}\n{exception.InnerException?.Message}");
            throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
        }
    }

    private static string TransactionTemplate => @"
	    SET ANSI_NULLS ON
	    SET QUOTED_IDENTIFIER ON
	    SET ARITHABORT ON
	    SET XACT_ABORT ON
	    SET NOCOUNT ON

        BEGIN TRY
            BEGIN TRANSACTION

            {QUERY}

            OPTION (RECOMPILE)
            COMMIT TRANSACTION
        END TRY
        BEGIN CATCH
		    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	        DECLARE @ErrorMsg NVARCHAR(2048) = error_message()
	        RAISERROR (@ErrorMsg, 16, 1)
        END CATCH
    ";
}