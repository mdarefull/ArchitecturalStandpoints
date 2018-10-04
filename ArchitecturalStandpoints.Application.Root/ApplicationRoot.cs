using ArchitecturalStandpoints.Sales;
using Commons.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace ArchitecturalStandpoints
{
    public static class ApplicationRoot
    {
        public static void ConfigureServices(IServiceCollection services)
            => services
               .AddScoped<IDbConnection, SqlConnection>(s => new SqlConnection("Server=.; Database=Northwind; Trusted_Connection=True;"))
               .AddScoped<IUnitOfWork, UnitOfWork>()
               .AddTransient<ISalesService, SalesService>();
    }
}
