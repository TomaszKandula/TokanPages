namespace TokanPages.Backend.Database.Initializer
{
    public interface IDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// It will create the database if it does not already exist, and
        /// in-memory database is not in use.
        /// </summary>
        void StartMigration();

        /// <summary>
        /// Adds default values to the database if tables are empty (newly created).
        /// </summary>
        void SeedData();
    }
}