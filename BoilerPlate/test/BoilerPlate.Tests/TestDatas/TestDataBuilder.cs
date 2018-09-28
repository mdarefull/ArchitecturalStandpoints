using BoilerPlate.EntityFrameworkCore;
using BoilerPlate.Tasks;

namespace BoilerPlate.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly BoilerPlateDbContext _context;

        public TestDataBuilder(BoilerPlateDbContext context) => _context = context;

        public void Build() => _context.Tasks.AddRange(
            new Task
            {
                Title = "Follow the white rabbit",
                Description = "Follow the white rabbit in order to know the reality.",
            },
            new Task
            {
                Title = "Clean your room",
                State = TaskState.Completed,
            }
        );
    }
}