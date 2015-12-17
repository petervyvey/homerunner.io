
using System;
using HomeRunner.Foundation.Entity;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Entities
{
    public class TaskActivity
        : DataEntity<Guid>
    {
        public virtual string Description { get; set; }

        public virtual string AssignedTo { get; set; }

        public virtual bool IsClaimed { get; set; }

        public virtual bool IsCompleted { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
