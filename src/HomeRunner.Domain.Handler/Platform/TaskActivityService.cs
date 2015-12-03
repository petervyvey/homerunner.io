
using HomeRunner.Domain.Service.Platform.Commands;
using HomeRunner.Domain.Service.Platform.Queries;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.Infrastructure;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace HomeRunner.Domain.Service.Platform
{
    public class TaskActivityService :
            IQueryHandler<TaskActivityListQuery, IEnumerable<ITaskActivity>>,

            ICommandHandler<ClaimTaskActivityCommand>,
            ICommandHandler<UnclaimTaskActivityCommand>
    {
        private readonly Foundation.Infrastructure.IQueryProvider<ISession> queryProvider;

        public TaskActivityService(IQueryProvider<ISession> queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public IEnumerable<ITaskActivity> Handle(TaskActivityListQuery query)
        {
            var queryable =
                this.queryProvider.Find<TaskActivityListQuery, Data.Platform.TaskActivity>(query);

            return queryable.ToList().ToDomainEntities<Data.Platform.TaskActivity, TaskActivity>();
        }

        public ICommandResult Handle(ClaimTaskActivityCommand command)
        {
            var data =
                this.queryProvider
                    .CreateQuery<Data.Platform.TaskActivity>()
                    .Single(x => x.Id == command.TaskId);

            TaskActivity entity = new TaskActivity(data);
            IDomainEvent domainEvent = entity.Apply(command);

            return new CommandResult(command.Id, domainEvent);
        }

        public ICommandResult Handle(UnclaimTaskActivityCommand command)
        {
            var data =
                this.queryProvider
                    .CreateQuery<Data.Platform.TaskActivity>()
                    .Single(x => x.Id == command.TaskId);

            TaskActivity entity = new TaskActivity(data);
            IDomainEvent domainEvent = entity.Apply(command);

            return new CommandResult(command.Id, domainEvent);
        }
    }
}
