
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Infrastructure;
using NHibernate;
using System.Collections.Generic;
using System.Linq;


namespace HomeRunner.Domain.ReadModel.Platform.TaskActivities
{
    public class TaskActivityRepository :
        IQueryHandler<TaskActivityQuery, TaskActivity>,
        IQueryHandler<TaskActivityListQuery, IEnumerable<TaskActivity>>
    {
        private readonly IQueryProvider<ISession> queryProvider;

        public TaskActivityRepository(IQueryProvider<ISession> queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public TaskActivity Handle(TaskActivityQuery query)
        {
            var instance = this.queryProvider.SingleOrDefault<TaskActivityQuery, TaskActivity>(query);

            return instance;
        }

        public IEnumerable<TaskActivity> Handle(TaskActivityListQuery query)
        {
			var list = this.queryProvider.Find<TaskActivityListQuery, TaskActivity>(query).ToList();

			return list;
        }
    }
}
