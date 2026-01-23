using System.Reflection;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Persistence.DataAccess.Helpers;

public class SqlGenerator : ISqlGenerator
{
    public string GetTableName<T>()
    {
        var table = typeof(T).Name;

        var entityAttributes = (DatabaseTableAttribute[])typeof(T).GetCustomAttributes(typeof(DatabaseTableAttribute), true);
        if (entityAttributes.Length == 0)
            return table;

        var attributes = entityAttributes[0];
        if (string.IsNullOrWhiteSpace(attributes.TableName))
        {
            table = $"{attributes.Schema}.{table}";
        }
        else
        {
            table = $"{attributes.Schema}.{attributes.TableName}";
        }

        return table;
    }

    public string GenerateInsertStatement<T>(T entity)
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
                    values.Add(ProcessValue(value));
                    break;
            }

            columns.Add(property.Name);
        }

        return string.Format(template, table, string.Join(",", columns), string.Join(",", values));
    }

    public string GenerateUpdateStatement<T>(T entity)
    {
        const string template = "UPDATE {0} SET {1} WHERE {2}";

        var table = GetTableName<T>();
        var update = new List<string>();
        var condition = string.Empty;
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(entity)?.ToString();
            if (string.IsNullOrEmpty(value))
                continue;

            var hasPrimaryKey = HasPrimaryKey(property);
            if (hasPrimaryKey)
            {
                condition = $"{property.Name}='{value}'";
            }
            else
            {
                var inputValue = value switch
                {
                    null => "NULL",
                    "False" => "0",
                    "True" => "1",
                    _ => ProcessValue(value)
                };

                update.Add($"{property.Name}={inputValue}");
            }
        }

        if (string.IsNullOrWhiteSpace(condition))
            throw new GeneralException(nameof(ErrorCodes.MISSING_PRIMARYKEY), ErrorCodes.MISSING_PRIMARYKEY);

        return string.Format(template, table, string.Join(",", update), condition);
    }

    public string GenerateDeleteStatement<T>(T entity)
    {
        const string template = "DELETE FROM {0} WHERE {1}";

        var table = GetTableName<T>();
        var conditions = new List<string>();
        var columnWithKey = new List<string>();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(entity)?.ToString();
            if (string.IsNullOrEmpty(value))
                continue;

            var hasPrimaryKey = HasPrimaryKey(property);
            if (hasPrimaryKey)
                columnWithKey.Add(value);

            var inputValue = value switch
            {
                null => "NULL",
                "False" => "0",
                "True" => "1",
                _ => ProcessValue(value)
            };

            conditions.Add($"{property.Name}={inputValue}");
        }

        if (columnWithKey.Count != 1)
            throw new GeneralException(nameof(ErrorCodes.MISSING_PRIMARYKEY), ErrorCodes.MISSING_PRIMARYKEY);

        return string.Format(template, table, string.Join(" AND ", conditions));
    }

    private static bool HasPrimaryKey (PropertyInfo property) => Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute)) != null;

    private static string ProcessValue(string value)
    {
        var isInteger = int.TryParse(value, out var _);
        var isDouble = double.TryParse(value, out var _);
        var isFloat = float.TryParse(value, out var _);

        if (!isInteger || !isDouble || !isFloat)
            value = $"'{value}'";

        return value;
    }
}