using System.Reflection;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Abstractions;

namespace TokanPages.Persistence.DataAccess;

/// <inheritdoc/>
public class SqlGenerator : ISqlGenerator
{
    private static GeneralException MissingPrimaryKey => new(nameof(ErrorCodes.MISSING_PRIMARYKEY), ErrorCodes.MISSING_PRIMARYKEY);

    private static GeneralException MissingWhereClause => new(nameof(ErrorCodes.MISSING_WHERE_CLAUSE), ErrorCodes.MISSING_WHERE_CLAUSE);

    private static bool HasPrimaryKey (PropertyInfo property) => Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute)) != null;

    private static Attribute? GetPrimaryKey(PropertyInfo property) => Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));

    private static Dictionary<string, object?> GetDictionary(object entity)
        => entity
            .GetType()
            .GetProperties()
            .ToDictionary(info => info.Name, info => info.GetValue(entity,null));

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
    public string GenerateQueryStatement<T>(object? filterBy = null, object? orderBy = null)
    {
        const string template = "SELECT {0} FROM {1}";

        var table = GetTableName<T>();
        var whereConditions = new List<string>();
        var orderConditions = new List<string>();

        var whereClause = string.Empty;
        var orderByClause = string.Empty;

        var properties = typeof(T).GetProperties();
        var columns = properties.Select(property => property.Name).ToList();
        var tableColumns = string.Join(",", columns);

        if (filterBy != null)
        {
            var dictionary = GetDictionary(filterBy);
            foreach (var item in dictionary)
            {
                whereConditions.Add($"{item.Key}=@{item.Key}");
                if (!columns.Contains(item.Key))
                    throw new ArgumentOutOfRangeException(paramName: $"{item.Key}", message: ErrorCodes.INVALID_COLUMN_NAME);
            }

            whereClause = string.Join(" AND ", whereConditions);
        }

        if (orderBy != null)
        {
            var dictionary = GetDictionary(orderBy);
            foreach (var item in dictionary)
            {
                var value = item.Value as string;
                if (value != "ASC" && value != "DESC")
                    throw new ArgumentException(ErrorCodes.INVALID_ARGUMENT);

                orderConditions.Add($"{item.Key} {value}");
                if (!columns.Contains(item.Key))
                    throw new ArgumentOutOfRangeException(paramName: $"{item.Key}", message: ErrorCodes.INVALID_COLUMN_NAME);
            }

            orderByClause = string.Join(",", orderConditions);
        }

        var hasWhereClause = !string.IsNullOrWhiteSpace(whereClause);
        var hasOrderByClause = !string.IsNullOrWhiteSpace(orderByClause);
        var query = string.Format(template, tableColumns, table);

        if (hasWhereClause && !hasOrderByClause)
            query += $" WHERE {whereClause}";

        if (!hasWhereClause && hasOrderByClause)
            query += $" ORDER BY {orderByClause}";

        if (hasWhereClause && hasOrderByClause)
            query += $" WHERE {whereClause} ORDER BY {orderByClause}";

        return query;
    }

    /// <inheritdoc/>
    public Tuple<string, object> GenerateInsertStatement<T>(T entity)
    {
        const string template = "INSERT INTO {0} ({1}) VALUES ({2})";

        var table = GetTableName<T>();
        var properties = typeof(T).GetProperties();

        var hasPrimaryKey = false;
        var columns = new List<string>();
        var values = new List<string>();
        var parameters = new Dictionary<string, object?>();

        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
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
    public Tuple<string, object> GenerateInsertStatement<T>(List<T> entities)
    {
        const string template = "INSERT INTO {0} VALUES {1}";

        var table = GetTableName<T>();
        var properties = typeof(T).GetProperties();

        var hasPrimaryKey = false;
        var parameterCounter = 1;
        var parameters = new Dictionary<string, object?>();

        var columns = new List<string>();
        foreach (var property in properties)
        {
            var isPrimaryKeyFound = HasPrimaryKey(property);
            if (!hasPrimaryKey && isPrimaryKeyFound)
                hasPrimaryKey = true;

            columns.Add(property.Name);
        }

        var stringColumns = string.Join(",", columns);
        var statement = $"{table} ({stringColumns})";

        var rows = new List<string>();
        foreach (var entity in entities)
        {
            var entityProperties = entity?.GetType().GetProperties();
            var values = new List<string>();
            foreach (var property in entityProperties!)
            {
                var value = property.GetValue(entity);
                values.Add($"@{property.Name}{parameterCounter}");
                parameters.Add($"{property.Name}{parameterCounter}", value);
            }

            rows.Add($"({string.Join(",", values)})");
            parameterCounter++;
        }

        var stringRows = string.Join(",", rows);
        var query = string.Format(template, statement, stringRows);
        var result = new Tuple<string, object>(query, parameters);

        return !hasPrimaryKey ? throw MissingPrimaryKey : result;
    }

    /// <inheritdoc/>
    public Tuple<string, object> GenerateUpdateStatement<T>(object updateBy, object filterBy)
    {
        const string template = "UPDATE {0} SET {1} WHERE {2}";

        var table = GetTableName<T>();
        var entityProperties = typeof(T).GetProperties();
        var columns = entityProperties.Select(property => property.Name).ToList();

        var update = new List<string>();
        var condition = new List<string>();
        var parameters = new Dictionary<string, object?>();

        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        var updateDict = GetDictionary(updateBy);
        foreach (var item in updateDict)
        {
            update.Add($"{item.Key}=@{item.Key}");
            parameters.Add(item.Key, item.Value);
            if (!columns.Contains(item.Key))
                throw new ArgumentOutOfRangeException(paramName: $"{item.Key}", message: ErrorCodes.INVALID_COLUMN_NAME);
        }

        var filterDict = GetDictionary(filterBy);
        foreach (var item in filterDict)
        {
            condition.Add($"{item.Key}=@{item.Key}");
            parameters.Add(item.Key, item.Value);
            if (!columns.Contains(item.Key))
                throw new ArgumentOutOfRangeException(paramName: $"{item.Key}", message: ErrorCodes.INVALID_COLUMN_NAME);
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
        var columns = entityProperties.Select(property => property.Name).ToList();

        var conditions = new List<string>();
        var parameters = new Dictionary<string, object?>();

        var isPrimaryKeyFound = entityProperties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        var dictionary = GetDictionary(deleteBy);
        foreach (var item in dictionary)
        {
            conditions.Add($"{item.Key}=@{item.Key}");
            parameters.Add(item.Key, item.Value);
            if (!columns.Contains(item.Key))
                throw new ArgumentOutOfRangeException(paramName: $"{item.Key}", message: ErrorCodes.INVALID_COLUMN_NAME);
        }

        if (conditions.Count == 0)
            throw MissingWhereClause;

        var stringConditions = string.Join(" AND ", conditions);
        var statement = string.Format(template, table, stringConditions);
        return new Tuple<string, object>(statement, parameters);
    }

    /// <inheritdoc/>
    public Tuple<string, object> GenerateDeleteStatement<T>(HashSet<object> ids)
    {
        const string template = "DELETE FROM {0} WHERE {1} IN ({2})";

        var tableName = GetTableName<T>();
        var keyName = string.Empty;

        var conditions = new List<string>();
        var parameterCounter = 1;
        var parameters = new Dictionary<string, object?>();
        var properties = typeof(T).GetProperties();

        var isPrimaryKeyFound = properties.Any(HasPrimaryKey);
        if (!isPrimaryKeyFound)
            throw MissingPrimaryKey;

        foreach (var property in properties)
        {
            var key = GetPrimaryKey(property);
            if (key != null)
                keyName = property.Name;
        }

        foreach (var item in ids)
        {
            var parametrizedKey = $"{keyName}{parameterCounter}";
            conditions.Add($"@{parametrizedKey}");
            parameters.Add($"{parametrizedKey}", item);
            parameterCounter++;
        }

        var stringConditions = string.Join(",", conditions);
        var statement = string.Format(template, tableName, keyName, stringConditions);
        return new Tuple<string, object>(statement, parameters);
    }
}