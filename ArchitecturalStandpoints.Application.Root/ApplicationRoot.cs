using ArchitecturalStandpoints.Application.Models;
using ArchitecturalStandpoints.Sales;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ArchitecturalStandpoints
{
    public static class ApplicationRoot
    {
        public static void ConfigureServices(IServiceCollection services)
            => services
               .AddDbContext<NorthwindContext>(options => options.UseSqlServer("Server=.; Database=Northwind; Trusted_Connection=True;"))
               .AddScoped<IDummyDisposable, DummyDisposable>()
               .AddTransient<IDbConnection, SqlConnection>(service => new SqlConnection("Server=.; Database=Northwind; Trusted_Connection=True;"))
               .AddTransient<ISalesService, SalesService>();
    }
}
