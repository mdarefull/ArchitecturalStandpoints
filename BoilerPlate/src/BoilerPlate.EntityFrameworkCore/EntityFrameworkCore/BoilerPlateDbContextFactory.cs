using BoilerPlate.Configuration;
using BoilerPlate.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BoilerPlate.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class BoilerPlateDbContextFactory : IDesignTimeDbContextFactory<BoilerPlateDbContext>
    {
        public BoilerPlateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BoilerPlateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(BoilerPlateConsts.ConnectionStringName)
            );

            return new BoilerPlateDbContext(builder.Options);
        }
    }
}