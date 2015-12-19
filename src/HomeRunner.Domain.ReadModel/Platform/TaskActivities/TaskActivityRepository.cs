
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Dapper;
using System.Collections.Generic;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities
{
    public class TaskActivityRepository :
        IQueryHandler<TaskActivityQuery, TaskActivity>,
        IQueryHandler<TaskActivityListQuery, IEnumerable<TaskActivity>>
    {
        private readonly IQueryProvider queryProvider;

        public TaskActivityRepository(IQueryProvider queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public TaskActivity Handle(TaskActivityQuery query)
        {
            TaskActivity instance =
                queryProvider.From<TaskActivity>()
                    .By(x => x.Id).EqualTo(query.TaskActivityId)
                    .SingleOrDefault();

            return instance;
        }

        public IEnumerable<TaskActivity> Handle(TaskActivityListQuery query)
        {
            IList<TaskActivity> list = this.queryProvider.From<TaskActivity>().ToList();

			return list;
        }
    }
}
