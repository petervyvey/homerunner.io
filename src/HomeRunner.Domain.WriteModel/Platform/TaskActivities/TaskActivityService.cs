
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.ExceptionManagement;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.Infrastructure;
using MediatR;
using NHibernate;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities
{

	public class TaskActivityService :
		ICommandHandler<CreateTaskActivityCommand>,
        ICommandHandler<ClaimTaskActivityCommand>,
        ICommandHandler<UnclaimTaskActivityCommand>
    {
        private readonly IMediator mediator;

		private readonly IQueryProvider<ISession> queryProvider;

		private readonly IDomainEntityValidatorFactory validator;

		public TaskActivityService(IMediator mediator, IQueryProvider<ISession> queryProvider, IDomainEntityValidatorFactory validator)
        {
            this.mediator = mediator;
			this.queryProvider = queryProvider;
			this.validator = validator;
        }

		public ICommandResult Handle(CreateTaskActivityCommand command)
			{
				TaskActivity entity = new TaskActivity (new ReadModel.Platform.TaskActivities.Entities.TaskActivity (), validator.Create<TaskActivity>());
				IDomainEvent domainEvent = entity.Apply(command);

				entity
					.IfValid(e => this.queryProvider.Session.SaveOrUpdate(e.GetDataEnitity()))
					.ElseThrow();

				return new CommandResult (command.Id, domainEvent);
			}

        public ICommandResult Handle(ClaimTaskActivityCommand command)
        {
            var existing = this.mediator.Send(new TaskActivityQuery(command.TaskId));
			if (existing.IsDefault()) throw new DataEntityNotFoundException ("");

			TaskActivity entity = new TaskActivity(existing, validator.Create<TaskActivity>());
            IDomainEvent domainEvent = entity.Apply(command);

			entity.MustBeValid();

            return new CommandResult(command.Id, domainEvent);
        }

        public ICommandResult Handle(UnclaimTaskActivityCommand command)
		{
			var existing = this.mediator.Send (new TaskActivityQuery (command.TaskId));
			if (existing.IsDefault ())
				throw new DataEntityNotFoundException ("");

			TaskActivity entity = new TaskActivity (existing, validator.Create<TaskActivity> ());
			IDomainEvent domainEvent = entity.Apply (command);

			return new CommandResult (command.Id, domainEvent);
		}
	}
}