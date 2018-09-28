using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BoilerPlateSPA.Authorization.Roles;
using BoilerPlateSPA.Authorization.Users;
using BoilerPlateSPA.MultiTenancy;

namespace BoilerPlateSPA.EntityFrameworkCore
{
    public class BoilerPlateSPADbContext : AbpZeroDbContext<Tenant, Role, User, BoilerPlateSPADbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public BoilerPlateSPADbContext(DbContextOptions<BoilerPlateSPADbContext> options)
            : base(options)
        {
        }
    }
}
