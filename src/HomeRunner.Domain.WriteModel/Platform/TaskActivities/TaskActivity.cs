
using HomeRunner.Domain.WriteModel.Commands;
using HomeRunner.Domain.WriteModel.Events;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.Extension;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities
{
    public class TaskActivity
        : DomainEntity<Guid, ReadModel.Platform.TaskActivities.Entities.TaskActivity>
    {
        public TaskActivity()
            : base() { }

        public TaskActivity(ReadModel.Platform.TaskActivities.Entities.TaskActivity entity)
            : this()
        {
            Argument.InstanceIsRequired(entity, "entity");
            this.entity = entity;
        }

        public string Description
        {
            get { return this.entity.Description; }
            set { this.entity.Description = value; }
        }

        public string AssignedTo
        {
            get { return this.entity.AssignedTo; }
            set { this.entity.AssignedTo = value; }
        }

        public bool IsClaimed
        {
            get { return this.entity.IsClaimed; }
            set { this.entity.IsClaimed = value; }
        }

        public bool IsCompleted
        {
            get { return this.entity.IsCompleted; }
            set { this.entity.IsCompleted = value; }
        }

        public DateTime UpdateTime
        {
            get { return this.entity.UpdateTime; }
            set { this.entity.UpdateTime = value; }
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
    }
}
