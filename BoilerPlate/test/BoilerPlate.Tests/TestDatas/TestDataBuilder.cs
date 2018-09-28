using BoilerPlate.EntityFrameworkCore;

namespace BoilerPlate.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly BoilerPlateDbContext _context;

        public TestDataBuilder(BoilerPlateDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}