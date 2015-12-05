
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities;
using HomeRunner.Foundation.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries
{
    public class TaskActivityListQuery
        : IQuery<IEnumerable<TaskActivity>>, IWithFilterExpression<TaskActivity>
    {
        public TaskActivityListQuery(params Guid[] identifiers)
        {
            this.Identifiers = identifiers;
        }

        public Guid[] Identifiers { get; set; }

        public Expression<Func<TaskActivity, bool>> FilterExpression
        {
            get { return taskactivity => !this.Identifiers.Any() || this.Identifiers.Contains(taskactivity.Id); }
        }
    }
}
