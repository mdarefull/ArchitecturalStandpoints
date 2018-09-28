using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Tasks
{
    [Table("AppTasks")]
    public class Task : Entity, IHasCreationTime
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }
        public DateTime CreationTime { get; set; } = Clock.Now;
        public TaskState State { get; set; } = TaskState.Open;
    }
}
