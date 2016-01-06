
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.ExceptionManagement;
using HomeRunner.Foundation.Extension;
using HomeRunner.Foundation.NHibernate;
using MediatR;
using System.Linq;
using NHibernate;
using HomeRunner.Foundation.Infrastructure.Extension;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Events;
using System;
using HomeRunner.Foundation.Infrastructure;
using HomeRunner.Foundation.Infrastructure.Logging;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities
{

	public class TaskActivityService :
		ICommandHandler<CreateTaskActivityCommand>,
        ICommandHandler<ClaimTaskActivityCommand>,
        ICommandHandler<UnclaimTaskActivityCommand>,

        INotificationHandler<DomainEventMessage<TaskActivityUnclaimedEvent>>
    {
        private readonly IMediator mediator;

		private readonly IQueryProvider<ISession> queryProvider;

		private readonly IDomainEntityValidatorProvider validator;

		public TaskActivityService(IMediator mediator, IQueryProvider<ISession> queryProvider, IDomainEntityValidatorProvider validator)
        {
            this.mediator = mediator;
			this.queryProvider = queryProvider;
			this.validator = validator;
        }

		public ICommandResult Handle(CreateTaskActivityCommand command)
			{
				TaskActivity entity = new TaskActivity (new Entities.TaskActivity(), validator.Create<TaskActivity>());
				IDomainEvent domainEvent = entity.Apply(command);

				entity
					.IfValid(e => this.queryProvider.Session.SaveOrUpdate(e.GetDataEnitity()))
					.ElseThrow();

				return new CommandResult (command.Id, domainEvent);
			}

        public void Handle(DomainEventMessage<TaskActivityUnclaimedEvent> notification)
        {
            Logger.Log.InfoFormat(Logger.CORRELATED_CONTENT, notification.DomainEvent.CorrelationId, "received task activity unclaimed", notification.DomainEvent.TaskId);
            var test = notification;
        }

        public ICommandResult Handle(ClaimTaskActivityCommand command)
        {
            var existing = this.queryProvider.CreateQuery<Entities.TaskActivity>().SingleOrDefault(x => x.Id == command.TaskId);
			if (existing.IsDefault()) throw new DataEntityNotFoundException ("");

			TaskActivity entity = new TaskActivity(existing, validator.Create<TaskActivity>());
            IDomainEvent domainEvent = entity.Apply(command);

			entity.MustBeValid();

            return new CommandResult(command.Id, domainEvent);
        }

        public ICommandResult Handle(UnclaimTaskActivityCommand command)
		{
            var existing = this.queryProvider.CreateQuery<Entities.TaskActivity>().SingleOrDefault(x => x.Id == command.TaskId);
			if (existing.IsDefault ())
				throw new DataEntityNotFoundException ("");

			TaskActivity entity = new TaskActivity (existing, validator.Create<TaskActivity> ());
			IDomainEvent domainEvent = entity.Apply (command);

            entity.MustBeValid();

			return new CommandResult (command.Id, domainEvent);
		}
	}
}