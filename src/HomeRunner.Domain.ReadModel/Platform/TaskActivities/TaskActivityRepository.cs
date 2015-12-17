
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Dapper;
using HomeRunner.Foundation.Dapper.Filter;
using System.Collections.Generic;

namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities
{
    public class TaskActivityRepository :
        IQueryHandler<TaskActivityQuery, TaskActivity>,
        IQueryHandler<TaskActivityListQuery, IEnumerable<TaskActivity>>
    {
        private readonly IEntityContext queryProvider;

        private readonly ICriteriaProvider criteria;

        public TaskActivityRepository(IEntityContext queryProvider, ICriteriaProvider criteria)
        {
            this.queryProvider = queryProvider;
            this.criteria = criteria;
        }

        public TaskActivity Handle(TaskActivityQuery query)
        {
            TaskActivity instance =
                this.queryProvider
                    .Get<TaskActivity>(criteria.CreateCriteria()
                        .Add<TaskActivity>(x => x.Id).Equal(query.TaskActivityId));

            return instance;
        }

        public IEnumerable<TaskActivity> Handle(TaskActivityListQuery query)
        {
            IList<TaskActivity> list =
                this.queryProvider
                    .GetList<TaskActivity>();

			return list;
        }
    }
}
