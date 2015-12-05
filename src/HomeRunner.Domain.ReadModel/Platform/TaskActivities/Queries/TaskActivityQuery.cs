
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities;
using HomeRunner.Foundation.Cqrs;
using System;
using System.Linq.Expressions;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries
{
    public class TaskActivityQuery
        : IQuery<TaskActivity>, IWithFilterExpression<TaskActivity>
    {
        public TaskActivityQuery(Guid taskActivityId)
        {
            this.TaskActivityId = taskActivityId;
        }

        public Guid TaskActivityId { get; set; }

        public Expression<Func<TaskActivity, bool>> FilterExpression
        {
            get { return taskactivity => this.TaskActivityId == taskactivity.Id; }
        }
    }
}
