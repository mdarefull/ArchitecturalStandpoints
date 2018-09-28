using BoilerPlate.Tasks;
using Shouldly;
using Xunit;

namespace BoilerPlate.Tests
{
    public class TaskAppService_Tests : BoilerPlateTestBase
    {
        protected virtual ITaskAppService TaskAppService { get; }
        public TaskAppService_Tests() => TaskAppService = Resolve<ITaskAppService>();

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_All_Tasks()
        {
            var output = await TaskAppService.GetAllAsync(new GetAllTasksInput());

            output.Items.Count.ShouldBe(2);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Filtered_Tasks()
        {
            // Act
            var output = await TaskAppService.GetAllAsync(new GetAllTasksInput
            {
                State = TaskState.Open
            });

            // Assert
            output.Items.ShouldAllBe(t => t.State == TaskState.Open);
        }
    }
}
