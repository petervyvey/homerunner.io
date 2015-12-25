
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Representations;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Dapper;
using System.Collections.Generic;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities
{
    public class TaskActivityService :
        IQueryHandler<TaskActivityQuery, TaskActivity>,
        IQueryHandler<TaskActivityListQuery, TaskActivityList>
    {
        private readonly IQueryProvider queryProvider;

        public TaskActivityService(IQueryProvider queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public TaskActivity Handle(TaskActivityQuery query)
        {
            TaskActivity instance =
                this.queryProvider.From<TaskActivity>()
                    .By(x => x.Id).EqualTo(query.TaskActivityId)
                    .SingleOrDefault();

            return instance;
        }

        public TaskActivityList Handle(TaskActivityListQuery query)
        {
            IList<TaskActivity> list = 
				this.queryProvider.From<TaskActivity>()
					.ToList();

			return new TaskActivityList(list);
        }
    }
}
