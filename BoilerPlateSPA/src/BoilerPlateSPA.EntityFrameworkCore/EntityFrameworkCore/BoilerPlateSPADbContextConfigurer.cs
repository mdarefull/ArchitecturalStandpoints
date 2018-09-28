using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlateSPA.EntityFrameworkCore
{
    public static class BoilerPlateSPADbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BoilerPlateSPADbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BoilerPlateSPADbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
