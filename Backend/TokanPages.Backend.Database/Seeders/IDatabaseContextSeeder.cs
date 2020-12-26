using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Database.Seeders
{

    public interface IDatabaseContextSeeder
    {
        void Seed(ModelBuilder AModelBuilder);
    }

}
