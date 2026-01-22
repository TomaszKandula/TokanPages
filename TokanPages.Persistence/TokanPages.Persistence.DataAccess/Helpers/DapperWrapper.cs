using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Persistence.DataAccess.Helpers;

public class DapperWrapper : IDapperWrapper
{
    private readonly ILoggerService _loggerService;

    private readonly IConfiguration _configuration;

    private string ConnectionString => _configuration.GetValue<string>("Db_DatabaseContext") ?? "";

    public DapperWrapper(ILoggerService loggerService, IConfiguration configuration)
    {
        _loggerService = loggerService;
        _configuration = configuration;
    }

    public async Task Insert<T>(T entity, CancellationToken cancellationToken = default)
    {
        var sql = GenerateInsertStatement(entity);
        await ExecuteSqlTransaction(sql, cancellationToken);
    }

    public async Task Update<T>(T entity, CancellationToken cancellationToken = default)
    {
        await ExecuteSqlTransaction("sql", cancellationToken);//TODO: to be done
    }

    public async Task Delete<T>(T entity, CancellationToken cancellationToken = default)
    {
        var sql = GenerateDeleteStatement(entity);
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

    private static string GenerateInsertStatement<T>(T entity)
    {
        const string template = "INSERT INTO {0} ({1}) VALUES ({2})";

        var table = GetTableName<T>();
        var columns = new List<string>();
        var values = new List<string>();

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(entity)?.ToString();
            switch (value)
            {
                case null:
                    values.Add("NULL"); 
                    break;
                case "False":
                    values.Add("0");
                    break;
                case "True":
                    values.Add("1");
                    break;
                default:
                    values.Add("'" + value + "'");
                    break;
            }

            columns.Add(property.Name);
        }

        return string.Format(template, table, string.Join(",", columns), string.Join(",", values));
    }

    private static string GenerateDeleteStatement<T>(T entity)
    {
        const string template = "DELETE FROM {0} WHERE {1}";

        var table = GetTableName<T>();
        var conditions = new List<string>();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(entity)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                conditions.Add($"{property.Name} = '{value}'");
            }
        }

        return string.Format(template, table, string.Join(" AND ", conditions));
    }

    private static string GetTableName<T>()
    {
        var table = nameof(T);

        var entityAttributes = (DatabaseTableAttribute[])typeof(T).GetCustomAttributes(typeof(DatabaseTableAttribute), true);
        if (entityAttributes.Length == 0)
            return table;

        var attributes = entityAttributes[0];
        table = $"{attributes.Schema}.{attributes.TableName}";

        return table;
    }
}