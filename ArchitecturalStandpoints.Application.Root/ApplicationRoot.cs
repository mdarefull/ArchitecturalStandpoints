using System.Data;
using System.Data.SqlClient;

using ArchitecturalStandpoints.Customers;
using Commons.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitecturalStandpoints
{
    public static class ApplicationRoot
    {
        public static void ConfigureServices(IServiceCollection services)
            => services
               .AddScoped<IDbConnection, SqlConnection>(s => new SqlConnection("Server=.; Database=Northwind; Trusted_Connection=True;"))
               .AddScoped<IUnitOfWork, UnitOfWork>()
               .AddScoped(sl => sl.GetRequiredService<IUnitOfWork>() as UnitOfWork)
               .AddTransient<ICustomerRepository, CustomerRepository>();
    }
}
