
using HomeRunner.Foundation.Entity;
using System;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities
{
    public sealed class TaskActivity
        : DataEntity<Guid>
    {
        public string Description { get; set; }

        public string AssignedTo { get; set; }

        public bool IsClaimed { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
