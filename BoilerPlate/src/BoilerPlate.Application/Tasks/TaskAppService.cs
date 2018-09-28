using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Tasks
{
    public class TaskAppService : BoilerPlateAppServiceBase, ITaskAppService
    {
        protected virtual IRepository<Task> TaskRepository { get; }
        public TaskAppService(IRepository<Task> taskRepository) => TaskRepository = taskRepository;

        public virtual async Task<ListResultDto<TaskListDto>> GetAllAsync(GetAllTasksInput input)
        {
            var tasks = await TaskRepository
                              .GetAll()
                              .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                              .OrderByDescending(t => t.CreationTime)
                              .ToListAsync();
            var tasksDtos = ObjectMapper.Map<List<TaskListDto>>(tasks);
            return new ListResultDto<TaskListDto>(tasksDtos);
        }
    }
}
