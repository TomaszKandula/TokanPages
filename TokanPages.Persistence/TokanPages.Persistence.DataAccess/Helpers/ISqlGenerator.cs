namespace TokanPages.Persistence.DataAccess.Helpers;

/// <summary>
/// Generates SQL for most common CRUD operations.
/// </summary>
public interface ISqlGenerator
{
    /// <summary>
    /// Allows to obtain table name from entity attribute (if provided).
    /// Otherwise, it returns entity class name.
    /// </summary>
    /// <typeparam name="T">Entity object type</typeparam>
    /// <returns>Table name.</returns>
    string GetTableName<T>();

    /// <summary>
    /// Returns an SQL query to retrieve data from the given table, filtered by one or more columns.
    /// Table name is derived from the provided class. Use 'DatabaseTable' attribute.
    /// </summary>
    /// <param name="entity">Entity object.</param>
    /// <param name="filterBy">List of filter conditions, key-value style.</param>
    /// <typeparam name="T">Given entity object type.</typeparam>
    /// <returns>SQL statement.</returns>
    string GenerateQueryStatement<T>(T entity, IReadOnlyDictionary<string, object> filterBy);

    /// <summary>
    /// Returns an SQL statement for an INSERT query for the given values from provided in the entity object.
    /// Table name is derived from the provided class. Use 'DatabaseTable' attribute.
    /// </summary>
    /// <remarks>
    /// Provided entity object should have a property marked with the 'PrimaryKey' attribute.
    /// </remarks>
    /// <param name="entity">Entity object.</param>
    /// <typeparam name="T">Given entity object type.</typeparam>
    /// <returns>SQL statement.</returns>
    string GenerateInsertStatement<T>(T entity);

    /// <summary>
    /// Returns an SQL statement for an UPDATE query for the given values provided in the entity object.
    /// Table name is derived from the provided class. Use 'DatabaseTable' attribute.
    /// </summary>
    /// <remarks>
    /// Provided entity object should have a property marked with the 'PrimaryKey' attribute.
    /// IMPORTANT: Primary key is used as a QUERY filter.
    /// </remarks>
    /// <param name="entity">Entity object.</param>
    /// <typeparam name="T">Given entity object type.</typeparam>
    /// <returns>SQL statement.</returns>
    string GenerateUpdateStatement<T>(T entity);

    /// <summary>
    /// Returns an SQL statement for a DELETE query for the given values provided in the entity object.
    /// Table name is derived from the provided class. Use 'DatabaseTable' attribute.
    /// </summary>
    /// <remarks>
    /// Provided entity object should have a property marked with the 'PrimaryKey' attribute.
    /// </remarks>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>SQL statement.</returns>
    string GenerateDeleteStatement<T>(T entity);
}