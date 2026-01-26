using System.Globalization;
using System.Reflection;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Persistence.DataAccess.Helpers;

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
        var columns = new List<string>();
        var conditions = new List<string>();

        var entityProperties = typeof(T).GetProperties();
        foreach (var property in entityProperties)
            columns.Add(property.Name);

        var objectProperties = filterBy.GetType().GetProperties();
        var dictionary = objectProperties.ToDictionary(info => info.Name, info => info.GetValue(filterBy,null));

        foreach (var item in dictionary)
        {
            var inputValue = ProcessValue(item.Value);
            conditions.Add($"{item.Key}={inputValue}");
        }

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
    public string GenerateUpdateStatement<T>(object updateBy)
    {
        const string template = "UPDATE {0} SET {1} WHERE {2}";

        var table = GetTableName<T>();
        var update = new List<string>();
        var condition = string.Empty;
        var primaryKeyName = string.Empty;
        var entityProperties = typeof(T).GetProperties();

        foreach (var property in entityProperties)
        {
            var hasPrimaryKey = HasPrimaryKey(property);
            if (hasPrimaryKey)
                primaryKeyName =  property.Name;
        }

        var objectProperties = updateBy.GetType().GetProperties();
        var dictionary = objectProperties.ToDictionary(info => info.Name, info => info.GetValue(updateBy,null));

        foreach (var item in dictionary)
        {
            var inputValue = ProcessValue(item.Value);
            if (primaryKeyName == item.Key)
            {
                condition = $"{primaryKeyName}={inputValue}";
            }
            else
            {
                update.Add($"{item.Key}={inputValue}");
            }
        }

        var statement = string.Format(template, table, string.Join(",", update), condition);
        return string.IsNullOrWhiteSpace(condition) ? throw MissingPrimaryKey : statement;
    }

    /// <inheritdoc/>
    public string GenerateDeleteStatement<T>(object deleteBy)
    {
        const string template = "DELETE FROM {0} WHERE {1}";

        var table = GetTableName<T>();
        var conditions = new List<string>();
        var entityProperties = typeof(T).GetProperties();
        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);

        var objectProperties = deleteBy.GetType().GetProperties();
        var dictionary = objectProperties.ToDictionary(info => info.Name, info => info.GetValue(deleteBy,null));

        foreach (var item in dictionary)
        {
            var inputValue = ProcessValue(item.Value);
            conditions.Add($"{item.Key}={inputValue}");
        }

        var statement = string.Format(template, table, string.Join(" AND ", conditions));
        return !isPrimaryKeyFound ? throw MissingPrimaryKey : statement;
    }

    private static GeneralException MissingPrimaryKey => new(nameof(ErrorCodes.MISSING_PRIMARYKEY), ErrorCodes.MISSING_PRIMARYKEY);

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