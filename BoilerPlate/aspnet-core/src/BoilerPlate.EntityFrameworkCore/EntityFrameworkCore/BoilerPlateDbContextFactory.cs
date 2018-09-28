using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BoilerPlate.Configuration;
using BoilerPlate.Web;

namespace BoilerPlate.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BoilerPlateDbContextFactory : IDesignTimeDbContextFactory<BoilerPlateDbContext>
    {
        public BoilerPlateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BoilerPlateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            BoilerPlateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BoilerPlateConsts.ConnectionStringName));

            return new BoilerPlateDbContext(builder.Options);
        }
    }
}
