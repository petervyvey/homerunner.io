
using HomeRunner.Domain.Service.Platform.Commands;
using HomeRunner.Domain.Service.Platform.Events;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Entity;
using HomeRunner.Foundation.Extension;
using System;

namespace HomeRunner.Domain.Service.Platform
{
    public class TaskActivity
        : DomainEntity<Guid, Data.Platform.TaskActivity>, ITaskActivity
    {
        public TaskActivity()
            : base() { }

        public TaskActivity(Data.Platform.TaskActivity entity)
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
