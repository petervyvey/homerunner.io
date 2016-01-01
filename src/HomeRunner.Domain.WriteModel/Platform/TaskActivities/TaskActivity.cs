
using FluentValidation;
using FluentValidation.Results;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Events;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.Infrastructure.Extension;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities
{
	public sealed partial class TaskActivity
		: DomainEntity<Guid, Entities.TaskActivity>, IWithValidator<TaskActivity>
    {
		private readonly IValidator<TaskActivity> validator;

		public TaskActivity(IValidator<TaskActivity> validator)
            : base() 
		{
			Argument.InstanceIsRequired(validator, "validator");

			this.validator = validator;
		}

		public TaskActivity(Entities.TaskActivity entity, IValidator<TaskActivity> validator)
			: this(validator)
        {
            Argument.InstanceIsRequired(entity, "entity");

            this.entity = entity;
        }

        public string Description
        {
            get { return this.entity.Description; }
            private set { this.entity.Description = value; }
        }

        public string AssignedTo
        {
            get { return this.entity.AssignedTo; }
            private set { this.entity.AssignedTo = value; }
        }

        public bool IsClaimed
        {
            get { return this.entity.IsClaimed; }
            private set { this.entity.IsClaimed = value; }
        }

        public bool IsCompleted
        {
            get { return this.entity.IsCompleted; }
            private set { this.entity.IsCompleted = value; }
        }

        public DateTime UpdateTime
        {
            get { return this.entity.UpdateTime; }
            private set { this.entity.UpdateTime = value; }
        }

		public IDomainEvent Apply(CreateTaskActivityCommand command)
		{
			TaskActivityCreatedEvent domainEvent = new TaskActivityCreatedEvent(command);
			this.When(domainEvent);

			return domainEvent;
		}

        public IDomainEvent Apply(ClaimTaskActivityCommand command)
        {
            TaskActivityClaimedEvent domainEvent = new TaskActivityClaimedEvent(command);
            this.When(domainEvent);

            return domainEvent;
        }

        public IDomainEvent Apply(UnclaimTaskActivityCommand command)
        {
            TaskActivityUnclaimedEvent domainEvent = new TaskActivityUnclaimedEvent(command);
            this.When(domainEvent);

            return domainEvent;
        }

		public void When(TaskActivityCreatedEvent domainEvent)
		{
			this.Id = domainEvent.TaskId;
			this.Description = domainEvent.Description;

			this.DomainEvents.Add(domainEvent);
		}

        public void When(TaskActivityClaimedEvent domainEvent)
        {
            this.IsClaimed = true;

            this.DomainEvents.Add(domainEvent);
        }

        public void When(TaskActivityUnclaimedEvent domainEvent)
        {
            this.IsClaimed = false;

            this.DomainEvents.Add(domainEvent);
        }

		public ValidationResult Validate()
		{
			ValidationResult result = this.validator.Validate (this);

			return result;
		}
    }
}
