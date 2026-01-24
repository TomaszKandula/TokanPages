namespace TokanPages.Persistence.DataAccess.Helpers;

/// <summary>
/// 
/// </summary>
public interface ISqlGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GetTableName<T>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="filterBy"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GenerateQueryStatement<T>(T entity, IReadOnlyDictionary<string, object> filterBy);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GenerateInsertStatement<T>(T entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GenerateUpdateStatement<T>(T entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GenerateDeleteStatement<T>(T entity);
}