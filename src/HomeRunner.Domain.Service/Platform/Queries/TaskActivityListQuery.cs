
using HomeRunner.Foundation.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HomeRunner.Domain.Service.Platform.Queries
{
    public class TaskActivityListQuery
        : IQuery<IEnumerable<ITaskActivity>>, IWithFilterExpression<Data.Platform.TaskActivity>
    {
        public TaskActivityListQuery(params Guid[] ids)
        {
            this.Ids = ids;
        }

        public Guid[] Ids { get; set; }

        public Expression<Func<Data.Platform.TaskActivity, bool>> FilterExpression
        {
            get { return taskactivity => !this.Ids.Any() || this.Ids.Contains(taskactivity.Id); }
        }
    }
}
