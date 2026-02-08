using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess;

public class DbOperations : IDbOperations
{
    private readonly ILoggerService _loggerService;

    private readonly ISqlGenerator _sqlGenerator;

    private readonly IHostEnvironment  _environment;

    private readonly AppSettingsModel _appSettings;

    public DbOperations(ILoggerService loggerService, IOptions<AppSettingsModel> options, ISqlGenerator sqlGenerator, IHostEnvironment environment)
    {
        _loggerService = loggerService;
        _appSettings = options.Value;
        _sqlGenerator = sqlGenerator;
        _environment = environment;
    }

    public async Task<IEnumerable<T>> Retrieve<T>(object? filterBy = null, object? orderBy = null)
    {
        await using var connection = new SqlConnection(_appSettings.DbDatabaseContext);
        var sql = _sqlGenerator.GenerateQueryStatement<T>(filterBy, orderBy);
        var watch = new Stopwatch();
        try
        {
            watch.Start();
            var result = await connection.QueryAsync<T>(sql, filterBy);
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

    public async Task Insert<T>(T entity)
    {
        var (query, parameters) = _sqlGenerator.GenerateInsertStatement(entity);
        await ExecuteSqlTransaction(query, parameters);
    }

    public async Task Insert<T>(List<T> entities)
    {
        var (query, parameters) = _sqlGenerator.GenerateInsertStatement(entities);
        await ExecuteSqlTransaction(query, parameters);
    }

    public async Task Update<T>(object updateBy, object filterBy)
    {
        var (query, parameters) = _sqlGenerator.GenerateUpdateStatement<T>(updateBy, filterBy);
        await ExecuteSqlTransaction(query, parameters);
    }

    public async Task Delete<T>(object deleteBy)
    {
        var (query, parameters) = _sqlGenerator.GenerateDeleteStatement<T>(deleteBy);
        await ExecuteSqlTransaction(query, parameters);
    }

    public async Task Delete<T>(HashSet<object> ids)
    {
        var (query, parameters) = _sqlGenerator.GenerateDeleteStatement<T>(ids);
        await ExecuteSqlTransaction(query, parameters);
    }

    private async Task ExecuteSqlTransaction(string sql, object? parameters = null)
    {
        var watch = new Stopwatch();
        await using var connection = new SqlConnection(_appSettings.DbDatabaseContext);
        try
        {
            watch.Start();
            var query = TransactionTemplate.Replace("{QUERY}", sql);
            await connection.ExecuteAsync(query, parameters);
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