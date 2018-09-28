using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BoilerPlate.Authorization.Roles;
using BoilerPlate.Authorization.Users;
using BoilerPlate.MultiTenancy;

namespace BoilerPlate.EntityFrameworkCore
{
    public class BoilerPlateDbContext : AbpZeroDbContext<Tenant, Role, User, BoilerPlateDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public BoilerPlateDbContext(DbContextOptions<BoilerPlateDbContext> options)
            : base(options)
        {
        }
    }
}
