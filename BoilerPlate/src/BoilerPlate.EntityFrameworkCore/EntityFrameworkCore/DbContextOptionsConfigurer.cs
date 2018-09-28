using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<BoilerPlateDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for BoilerPlateDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
