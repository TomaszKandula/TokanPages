using System.Globalization;
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
            let inputValue = ProcessValue(item.Value) 
            select $"{item.Key}={inputValue}"
        ).ToList();

        return string.Format(template, string.Join(",", columns), table, string.Join(" AND ", conditions));
    }

    /// <inheritdoc/>
    public string GenerateInsertStatement<T>(T entity)
    {
        const string template = "INSERT INTO {0} ({1}) VALUES ({2})";

        var table = GetTableName<T>();
        var columns = new List<string>();
        var values = new List<string>();
        var hasPrimaryKey = false;

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = ProcessValue(property.GetValue(entity));
            var isPrimaryKeyFound = HasPrimaryKey(property);
            if (!hasPrimaryKey && isPrimaryKeyFound)
                hasPrimaryKey = true;

            values.Add(value);
            columns.Add(property.Name);
        }

        var statement = string.Format(template, table, string.Join(",", columns), string.Join(",", values));
        return !hasPrimaryKey ? throw MissingPrimaryKey : statement;
    }

    /// <inheritdoc/>
    public string GenerateUpdateStatement<T>(object updateBy, object filterBy)
    {
        const string template = "UPDATE {0} SET {1} WHERE {2}";

        var table = GetTableName<T>();
        var entityProperties = typeof(T).GetProperties();
        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        var updateDict = updateBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(updateBy,null));

        var update = (
            from item in updateDict 
            let inputValue = ProcessValue(item.Value) 
            select $"{item.Key}={inputValue}"
        ).ToList();

        var filterDict = filterBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(filterBy,null));

        var condition = (
            from item in filterDict 
            let inputValue = ProcessValue(item.Value) 
            select $"{item.Key}={inputValue}"
        ).ToList();

        if (condition.Count == 0)
            throw MissingWhereClause;

        var set = string.Join(",", update);
        var where = string.Join(" AND ", condition);
        var statement = string.Format(template, table, set, where);

        return statement;
    }

    /// <inheritdoc/>
    public string GenerateDeleteStatement<T>(object deleteBy)
    {
        const string template = "DELETE FROM {0} WHERE {1}";

        var table = GetTableName<T>();
        var entityProperties = typeof(T).GetProperties();
        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        var dictionary = deleteBy
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(deleteBy,null));

        var conditions = (
            from item in dictionary 
            let inputValue = ProcessValue(item.Value) 
            select $"{item.Key}={inputValue}"
        ).ToList();

        if (conditions.Count == 0)
            throw MissingWhereClause;

        return string.Format(template, table, string.Join(" AND ", conditions));
    }

    private static GeneralException MissingPrimaryKey => new(nameof(ErrorCodes.MISSING_PRIMARYKEY), ErrorCodes.MISSING_PRIMARYKEY);

    private static GeneralException MissingWhereClause => new(nameof(ErrorCodes.MISSING_WHERE_CLAUSE), ErrorCodes.MISSING_WHERE_CLAUSE);

    private static bool HasPrimaryKey (PropertyInfo property) => Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute)) != null;

    private static string ProcessValue(object? value)
    {
        if (value == null)
            return "NULL";

        var valueType = value.GetType();

        if (valueType == typeof(bool))
            return (bool)value ? "1" : "0";

        if (valueType == typeof(DateTime))
            return $"'{(DateTime)value:yyyy-MM-dd HH:mm:ss}'";

        if (valueType == typeof(Guid))
            return $"'{(Guid)value:D}'";

        if (valueType == typeof(int))
            return ((int)value).ToString();

        if (valueType == typeof(decimal))
            return ((decimal)value).ToString(CultureInfo.InvariantCulture);

        if (valueType == typeof(double))
            return ((double)value).ToString(CultureInfo.InvariantCulture);

        if (valueType == typeof(float))
            return ((float)value).ToString(CultureInfo.InvariantCulture);

        return $"'{value}'";
    }
}