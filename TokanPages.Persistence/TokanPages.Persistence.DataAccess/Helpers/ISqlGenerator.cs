namespace TokanPages.Persistence.DataAccess.Helpers;

public interface ISqlGenerator
{
    string GetTableName<T>();

    string GenerateInsertStatement<T>(T entity);

    string GenerateDeleteStatement<T>(T entity);
}