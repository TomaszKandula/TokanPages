using TokanPages.Backend.Domain.Attributes;

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

    public string GenerateDeleteStatement<T>(T entity)
    {
        const string template = "DELETE FROM {0} WHERE {1}";

        var table = GetTableName<T>();
        var conditions = new List<string>();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(entity)?.ToString();
            if (string.IsNullOrEmpty(value))
                continue;

            var inputValue = value switch
            {
                null => "NULL",
                "False" => "0",
                "True" => "1",
                _ => ProcessValue(value)
            };

            conditions.Add($"{property.Name}={inputValue}");
        }

        return string.Format(template, table, string.Join(" AND ", conditions));
    }

    private static string ProcessValue(string value)
    {
        var isInteger = int.TryParse(value, out var _);
        var isFloat = float.TryParse(value, out var _);

        if (!isInteger || !isFloat)
            value = $"'{value}'";

        return value;
    }
}