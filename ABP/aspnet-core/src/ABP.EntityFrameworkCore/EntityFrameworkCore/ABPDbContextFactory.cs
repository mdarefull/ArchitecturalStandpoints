using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ABP.Configuration;
using ABP.Web;

namespace ABP.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ABPDbContextFactory : IDesignTimeDbContextFactory<ABPDbContext>
    {
        public ABPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ABPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ABPDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ABPConsts.ConnectionStringName));

            return new ABPDbContext(builder.Options);
        }
    }
}
