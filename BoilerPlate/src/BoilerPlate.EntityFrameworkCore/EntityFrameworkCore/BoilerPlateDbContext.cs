using Abp.EntityFrameworkCore;
using BoilerPlate.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.EntityFrameworkCore
{
    public class BoilerPlateDbContext : AbpDbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public BoilerPlateDbContext(DbContextOptions<BoilerPlateDbContext> options) : base(options) { }
    }
}
