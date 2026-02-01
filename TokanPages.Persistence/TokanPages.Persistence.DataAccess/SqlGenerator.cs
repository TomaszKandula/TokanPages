using System.Reflection;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess;

/// <inheritdoc/>
public class SqlGenerator : ISqlGenerator
{
    /// <inheritdoc/>
    public string GetTableName<T>()
    {
        var table = typeof(T).Name;

        var entityAttributes = (DatabaseTableAttribute[])typeof(T).GetCustomAttributes(typeof(DatabaseTableAttribute), true);
        if (entityAttributes.Length == 0)
            return table;

        var attributes = entityAttributes[0];
        var hasNoTableName = string.IsNullOrWhiteSpace(attributes.TableName);
        table = hasNoTableName ? $"{attributes.Schema}.{table}" : $"{attributes.Schema}.{attributes.TableName}";

        return table;
    }

    /// <inheritdoc/>
    public string GenerateQueryStatement<T>(object filterBy)
    {
        const string template = "SELECT {0} FROM {1} WHERE {2}";

        var table = GetTableName<T>();
        var columns = typeof(T).GetProperties().Select(property => property.Name).ToList();

        var dictionary = filterBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(filterBy,null));

        var conditions = (
            from item in dictionary 
            select $"{item.Key}=@{item.Key}"
        ).ToList();

        return string.Format(template, string.Join(",", columns), table, string.Join(" AND ", conditions));
    }

    /// <inheritdoc/>
    public Tuple<string, object> GenerateInsertStatement<T>(T entity)
    {
        const string template = "INSERT INTO {0} ({1}) VALUES ({2})";

        var table = GetTableName<T>();
        var hasPrimaryKey = false;

        var columns = new List<string>();
        var values = new List<string>();
        var parameters = new Dictionary<string, object?>();

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value= property.GetValue(entity);
            var isPrimaryKeyFound = HasPrimaryKey(property);
            if (!hasPrimaryKey && isPrimaryKeyFound)
                hasPrimaryKey = true;

            columns.Add(property.Name);
            values.Add($"@{property.Name}");
            parameters.Add(property.Name, value);
        }

        var statement = string.Format(template, table, string.Join(",", columns), string.Join(",", values));
        var result = new Tuple<string, object>(statement, parameters);

        return !hasPrimaryKey ? throw MissingPrimaryKey : result;
    }

    /// <inheritdoc/>
    public Tuple<string, object> GenerateUpdateStatement<T>(object updateBy, object filterBy)
    {
        const string template = "UPDATE {0} SET {1} WHERE {2}";

        var table = GetTableName<T>();
        var entityProperties = typeof(T).GetProperties();
        var update = new List<string>();
        var condition = new List<string>();
        var parameters = new Dictionary<string, object?>();

        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        var updateDict = updateBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(updateBy,null));

        foreach (var item in updateDict)
        {
            update.Add($"{item.Key}=@{item.Key}");
            parameters.Add(item.Key, item.Value);
        }

        var filterDict = filterBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(filterBy,null));

        foreach (var item in filterDict)
        {
            condition.Add($"{item.Key}=@{item.Key}");
            parameters.Add(item.Key, item.Value);
        }

        if (condition.Count == 0)
            throw MissingWhereClause;

        var set = string.Join(",", update);
        var where = string.Join(" AND ", condition);
        var statement = string.Format(template, table, set, where);

        return new Tuple<string, object>(statement, parameters);
    }

    /// <inheritdoc/>
    public Tuple<string, object> GenerateDeleteStatement<T>(object deleteBy)
    {
        const string template = "DELETE FROM {0} WHERE {1}";

        var table = GetTableName<T>();
        var entityProperties = typeof(T).GetProperties();
        var conditions = new List<string>();
        var parameters = new Dictionary<string, object?>();

        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        var dictionary = deleteBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(deleteBy,null));

        foreach (var item in dictionary)
        {
            conditions.Add($"{item.Key}=@{item.Key}");
            parameters.Add(item.Key, item.Value);
        }

        if (conditions.Count == 0)
            throw MissingWhereClause;

        var statement = string.Format(template, table, string.Join(" AND ", conditions));
        return new Tuple<string, object>(statement, parameters);
    }

    private static GeneralException MissingPrimaryKey => new(nameof(ErrorCodes.MISSING_PRIMARYKEY), ErrorCodes.MISSING_PRIMARYKEY);

    private static GeneralException MissingWhereClause => new(nameof(ErrorCodes.MISSING_WHERE_CLAUSE), ErrorCodes.MISSING_WHERE_CLAUSE);

    private static bool HasPrimaryKey (PropertyInfo property) => Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute)) != null;
}