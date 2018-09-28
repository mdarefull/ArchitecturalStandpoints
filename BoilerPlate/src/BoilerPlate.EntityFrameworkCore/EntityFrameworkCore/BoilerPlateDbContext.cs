using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.EntityFrameworkCore
{
    public class BoilerPlateDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public BoilerPlateDbContext(DbContextOptions<BoilerPlateDbContext> options) 
            : base(options)
        {

        }
    }
}
