using ArchitecturalStandpoints.Sales;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace ArchitecturalStandpoints
{
    public static class ApplicationRoot
    {
        public static void ConfigureServices(IServiceCollection services)
            => services
               .AddTransient<IDbConnection, SqlConnection>(service => new SqlConnection("Server=.; Database=Northwind; Trusted_Connection=True;"))
               .AddTransient<ISalesService, SalesService>();
    }
}
