using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BoilerPlateSPA.Configuration;
using BoilerPlateSPA.Web;

namespace BoilerPlateSPA.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BoilerPlateSPADbContextFactory : IDesignTimeDbContextFactory<BoilerPlateSPADbContext>
    {
        public BoilerPlateSPADbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BoilerPlateSPADbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            BoilerPlateSPADbContextConfigurer.Configure(builder, configuration.GetConnectionString(BoilerPlateSPAConsts.ConnectionStringName));

            return new BoilerPlateSPADbContext(builder.Options);
        }
    }
}
